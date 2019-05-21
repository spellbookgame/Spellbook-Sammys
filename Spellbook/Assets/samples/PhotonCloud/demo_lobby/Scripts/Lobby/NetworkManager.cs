using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bolt;
using UdpKit;
using System;
using UnityEngine.SceneManagement;
using Bolt.Samples.Photon.Lobby.Utilities;
using Bolt.Samples.Photon.Simple;
using Photon.Lobby;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Bolt.Samples.Photon.Lobby
{
    /*On every player's device not just the host's.*/
    public class NetworkManager : Bolt.GlobalEventListener
    {
        static public NetworkManager s_Singleton;
        BoltEntity characterSelection;
        BoltEntity playerEntity;
        SpellCaster playerSpellcaster;
        BoltEntity gameStateEntity;
        [Header("Lobby Configuration")]
        public SceneField lobbyScene;
        public SceneField gameScene;
        public int minPlayers = 2;
        public int localPlayerSpellcasterID = -1;

        [Header("UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 3.0f;

        [Space]
        [Header("UI Reference")]
        public LobbyTopPanel topPanel;

        public RectTransform mainMenuPanel;
        public RectTransform lobbyPanel;
        public GameObject serverListPanel;

        public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
        public GameObject addPlayerButton;

        protected RectTransform currentPanel;
        public Button selectButton;

        public Button backButton;
        public GameObject startGameButton;
        public GameObject playerController;
        public GameObject bookShelf;
        public Text statusInfo;
        public Text hostInfo;
        public Text numPlayersInfo;
        protected string _matchName;

        //For server.
        //Key is connection.ToString()
        //Value is SpellcasterId (0-5)
        private Dictionary<string, int> connection_spellcaster;


        //For returning client only, does not support a returning host (yet).
        protected bool spawned = false;


        public string matchName;

        public string matchHost
        {
            get
            {
                if (BoltNetwork.IsRunning)
                {
                    if (BoltNetwork.IsServer)
                    {
                        return "<server>";
                    }

                    if (BoltNetwork.IsClient)
                    {
                        return "<client>";
                    }
                }
                return "";
            }
        }

        void Awake()
        {
            BoltLauncher.SetUdpPlatform(new PhotonPlatform());
        }

        void Start()
        {
            s_Singleton = this;
            currentPanel = mainMenuPanel;

            backButton.gameObject.SetActive(false);
            GetComponent<Canvas>().enabled = true;



            DontDestroyOnLoad(gameObject);

            SetServerInfo("Offline", "None");

            Debug.Log("Lobby Scene: " + lobbyScene.SimpleSceneName);
            Debug.Log("Game Scene: " + gameScene.SimpleSceneName);

            SoundManager.instance.musicSource.Play();
        }

        void FixedUpdate()
        {
            /*
            if (BoltNetwork.IsServer && !_isCountdown)
            {
                VerifyReady();
            }*/
        }

        public override void SceneLoadLocalDone(string scene)
        {
            BoltConsole.Write("New scene: " + scene, Color.yellow);
            BoltConsole.Write("Spawned : " + spawned, Color.yellow);
            try
            {
                if (lobbyScene.SimpleSceneName == scene)
                {
                    ChangeTo(mainMenuPanel);
                    topPanel.isInGame = false;
                }
                else if (gameScene.SimpleSceneName == scene)
                {
                    if (BoltNetwork.IsServer)
                    {
                        //SpellCasterLobbyChoose sl = GameObject.find
                        //BoltNetwork.Detach(characterSelection);
                    }
                    //ChangeTo(null);

                    backDelegate = Stop;
                    topPanel.isInGame = true;
                    topPanel.ToggleVisibility(false);
                    bookShelf.SetActive(false);
                    // Spawn Player
                    if (!spawned)
                    {
                        spawned = true;
                        SpawnGamePlayer();
                        MainPageHandler.instance.setupMainPage();
                    }

                    SoundManager.instance.PlayGameBCM();
                }

            }
            catch (Exception e)
            {
                //BoltConsole.Write(e.Message, Color.red);
                //BoltConsole.Write(e.Source, Color.red);
                //BoltConsole.Write(e.StackTrace, Color.red);
            }
        }

        public void ChangeTo(RectTransform newPanel)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }

            currentPanel = newPanel;

            if (currentPanel != mainMenuPanel)
            {
                backButton.gameObject.SetActive(true);
            }
            else
            {
                backButton.gameObject.SetActive(false);
                SetServerInfo("Offline", "None");
            }
        }

        public void DisplayIsConnecting()
        {
            var _this = this;
            //infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });
            infoPanel.Display();
            //PanelHolder.instance.displayConnectingToGame();
            serverListPanel.SetActive(false);
        }

        public void SetServerInfo(string status, string host)
        {
            statusInfo.text = status;
            hostInfo.text = host;
        }

        public delegate void BackButtonDelegate();
        public BackButtonDelegate backDelegate;
        public void GoBackButton()
        {
            backDelegate();
            topPanel.isInGame = false;
        }

        // ----------------- Server management

        private void StartServer()
        {
            BoltLauncher.StartServer();
            BoltConsole.Write("StartServer breh");
            Debug.Log("StartServer breh");
        }

        public void StartClient()
        {
            BoltLauncher.StartClient();
        }

        public void Stop()
        {
            BoltLauncher.Shutdown();
        }

        public void CreateMatch(string matchName, bool dedicated = false)
        {
            StartServer();
            _matchName = matchName;
        }

        public void SimpleBackClbk()
        {
            ChangeTo(mainMenuPanel);
        }

        // ----------------- Server callbacks ------------------

        public override void BoltStartBegin()
        {
            BoltConsole.Write("BoltStartBegin breh");
            BoltNetwork.RegisterTokenClass<RoomProtocolToken>();
            BoltNetwork.RegisterTokenClass<ServerAcceptToken>();
            BoltNetwork.RegisterTokenClass<ServerConnectToken>();
        }

        public override void BoltStartDone()
        {
            BoltConsole.Write("BoltStartDone breh");
            if (!BoltNetwork.IsRunning) { return; }

            if (BoltNetwork.IsServer)
            {
                RoomProtocolToken token = new RoomProtocolToken()
                {
                    ArbitraryData = "My DATA",
                };

                BoltLog.Info("Starting Server");
                // Start Photon Room
                BoltNetwork.SetServerInfo(_matchName, token);
                //BoltNetwork.EnableLanBroadcast();
                // Setup Host
                infoPanel.gameObject.SetActive(false);
                //PanelHolder.instance.hideConnectingPanel();
                ChangeTo(lobbyPanel);

                backDelegate = Stop;
                SetServerInfo("Host", "");
                connection_spellcaster = new Dictionary<string, int>();
                //SoundManager.instance.musicSource.Play();

                // Build Server Entity

                characterSelection = BoltNetwork.Instantiate(BoltPrefabs.CharacterSelectionEntity);
                characterSelection.TakeControl();

                gameStateEntity = BoltNetwork.Instantiate(BoltPrefabs.GameState);
                gameStateEntity.TakeControl();
                gameStateEntity.GetComponent<NetworkGameState>().onCreateRoom(_matchName);

                numPlayersInfo.text = gameStateEntity.GetComponent<NetworkGameState>().onPlayerJoined() + "";


            }
            else if (BoltNetwork.IsClient)
            {
                backDelegate = Stop;
                SetServerInfo("Client", "");
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            BoltConsole.Write("BoltShutdownBegin");
            _matchName = "";

            if (BoltNetwork.IsServer)
            {
                BoltNetwork.LoadScene(lobbyScene.SimpleSceneName);
            }
            else if (BoltNetwork.IsClient)
            {
                SceneManager.LoadScene(lobbyScene.SimpleSceneName);
            }

            registerDoneCallback(() =>
            {
                Debug.Log("Shutdown Done");
                ChangeTo(mainMenuPanel);
            });
        }

        /*Called from host Start button*/
        public void onGameStart()
        {
            if (!gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
                return;
            }
            gameStateEntity.GetComponent<NetworkGameState>().globalEvents.determineGlobalEvents();
            StartCoroutine(ServerCountdownCoroutine());
        }

        /*Called when player clicks on a spellcaster in the character selection panel.
         The select button is enabled so the player can confirm their character selection.
             */
        public void activateSelectButton(bool isActive)
        {
            selectButton.gameObject.SetActive(isActive);
        }


        public IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = 4f;//prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdown;

            ChangeTo(countdownPanel.GetComponent<RectTransform>());
            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {
                    floorTime = newFloorTime;

                    countdown = LobbyCountdown.Create(GlobalTargets.Everyone);
                    countdown.Time = floorTime;
                    countdown.Send();
                }
            }

            countdown = LobbyCountdown.Create(GlobalTargets.Everyone);
            countdown.Time = 0;
            countdown.Send();

            BoltNetwork.LoadScene(gameScene.SimpleSceneName);
        }

        public override void EntityReceived(BoltEntity entity)
        {
            BoltConsole.Write("EntityReceived");
        }

        public override void EntityAttached(BoltEntity entity)
        {
            BoltConsole.Write("EntityAttached");

            if (!entity.isControlled)
            {
                LobbyPhotonPlayer photonPlayer = entity.gameObject.GetComponent<LobbyPhotonPlayer>();

                if (photonPlayer != null)
                {
                    photonPlayer.SetupOtherPlayer();
                }
            }
        }

        public static string PreviousMatchName()
        {
            if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                file.Close();
                return data.matchname;
            }
            return "";
        }

        public override void Connected(BoltConnection connection)
        {
            if (BoltNetwork.IsClient)
            {
                BoltConsole.Write("Connected Client: " + connection, Color.blue);
                infoPanel.gameObject.SetActive(false);
                string prevMatch = PreviousMatchName();
                BoltConsole.Write("Previous match if any: " + prevMatch);

                if (prevMatch != matchName)
                {
                    BoltConsole.Write("New Game");
                    ChangeTo(lobbyPanel);
                }
                else
                {
                    BoltConsole.Write("Joining previous match");
                    var returnPlayerEvnt = ReturnPlayerEvent.Create(Bolt.GlobalTargets.OnlyServer);
                    returnPlayerEvnt.Send();
                }

            }
            else if (BoltNetwork.IsServer)
            {
                BoltConsole.Write("Connected Server: " + connection, Color.blue);
                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.CharacterSelectionEntity);
                entity.AssignControl(connection);
                int numPlayers = gameStateEntity.GetComponent<NetworkGameState>().onPlayerJoined();
                var playerTurnEvnt = PlayerJoinedEvent.Create(Bolt.GlobalTargets.Everyone);
                playerTurnEvnt.numOfPlayers = numPlayers;
                playerTurnEvnt.Send();
            }
        }

        public override void Disconnected(BoltConnection connection)
        {
            int id = connection_spellcaster[connection.ToString()];
            gameStateEntity.GetComponent<NetworkGameState>()
            .onRemovePlayer(id);
            //BoltNetwork.Destroy(entity);///////////////////////
        }

        public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
        {
        }

        // Spawner
        private void SpawnGamePlayer()
        {
            playerEntity = BoltNetwork.Instantiate(BoltPrefabs.LocalPlayer);
            playerEntity.TakeControl();
            playerEntity.GetComponent<Player>().setup(localPlayerSpellcasterID);
            PanelHolder.instance.displayNotify("Global Event Coming Soon", NetworkGameState.instance.getEventInfo(), "OK");
        }
        #region CLIENT_CALLBACKS
        // ----------------- Client callbacks -----------------------------------------------------------
        // NOTE: the host also recieves these callbacks


        public override void OnEvent(NextPlayerTurnEvent evnt)
        {
            //BoltConsole.Write("Recieved NextPlayerTurnEvent");
            playerEntity.GetComponent<Player>().nextTurnEvent(evnt.NextSpellcaster);
        }

        public override void OnEvent(LobbyCountdown evnt)
        {
            ChangeTo(countdownPanel.GetComponent<RectTransform>());
            Text text = countdownPanel.UIText;
            text.fontSize = 40;
            text.text = "Match Starting in " + evnt.Time;
            countdownPanel.gameObject.SetActive(evnt.Time != 0);
        }

        //TODO: Double check if this includes other crisis events
        public override void OnEvent(NewUpcomingEvent evnt)
        {
            BoltConsole.Write("New Upcoming event : " + evnt.Name + " cool");
            PanelHolder.instance.displayNotify("Global Event Coming Soon", NetworkGameState.instance.getEventInfo(), "OK");
        }

        // Player recieves this event from the network, the event contains
        // the spellcasterID  of the spellcaster that can counter the event
        // if this client's spellcasterId matches that ID then check if they 
        // can counter it.
        public override void OnEvent(CheckIfCanCounterEvent evnt)
        {
            Debug.Log("Check Counter Event");
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            if (playerSpellcaster.spellcasterID == evnt.requiredSpellcaster)
            {
                var counterEvent = CounterGlobalEvent.Create(GlobalTargets.OnlyServer);
                counterEvent.EventID = evnt.eventID;
                foreach (Spell spell in playerSpellcaster.chapter.spellsCollected)
                {
                    if (spell.iTier >= evnt.requiredSpellTier)
                    {
                        counterEvent.IsCountered = true;
                        counterEvent.Send();
                        return;
                    }
                }
                counterEvent.IsCountered = false;
                counterEvent.Send();
            }
        }

        // Let's players know who countered the crisis
        public override void OnEvent(LetEveryoneKnowAboutCounter evnt)
        {
            if (playerSpellcaster.classType == evnt.Savior)
            {
                BoltConsole.Write("You saved the world with your spell!");
                Debug.Log("You saved the world with your spell!");
                PanelHolder.instance.displayNotify("Congratulations", "You saved the world with your spell!", "OK");
            }
            else
            {
                BoltConsole.Write(evnt.Savior + " saved the world!");
                Debug.Log(evnt.Savior + " saved the world!");
                PanelHolder.instance.displayNotify("Congratulations", evnt.Savior + " saved the world!", "OK");
            }
        }

        // For lobby, updates the Number of players and displays it in character selection panel
        public override void OnEvent(PlayerJoinedEvent evnt)
        {
            numPlayersInfo.text = evnt.numOfPlayers + "";
            if (BoltNetwork.IsServer && gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
                startGameButton.SetActive(true);
            }
            else
            {
                startGameButton.SetActive(false);
            }
        }

        //TODO: start combat here.
        public override void OnEvent(FinalBoss evnt)
        {
            BoltConsole.Write("Final Boss battle (not yet implemented so everyone dies)");
            Debug.Log("Final Boss battle (not yet implemented so everyone dies)");
            PanelHolder.instance.displayNotify("Final Boss Battle", "(not yet implemented so everyone dies)", "OK");
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            playerSpellcaster.TakeDamage((int)(playerSpellcaster.fCurrentHealth));
            SpellCaster.savePlayerData(playerSpellcaster);
        }

        // Deals a percent amount of damage to a target spellcaster, and display a pop-up message.
        // Also tries to update the healthvalue UI-component in the mainplayerscene.
        public override void OnEvent(DealPercentDmgEvent evnt)
        {
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            if (playerSpellcaster.spellcasterID == evnt.SpellcasterID)
            {
                PanelHolder.instance.displayNotify(evnt.EventName, "Lose " + ((int)(evnt.PercentDmgDecimal * 100)) + "% health", "OK");
                playerSpellcaster.TakeDamage((int)(playerSpellcaster.fCurrentHealth * evnt.PercentDmgDecimal));
                SpellCaster.savePlayerData(playerSpellcaster);
                try
                {
                    GameObject health = GameObject.Find("text_healthvalue");
                    if (health != null)
                    {
                        health.GetComponent<Text>().text = playerSpellcaster.fCurrentHealth + " / 20";
                    }

                }
                catch
                {
                    // Not in home page
                }
            }
        }


        //The ally recieves this event.
        public override void OnEvent(CastOnAllyEvent evnt)
        {
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            // 8 stands for all spellcasters, the spell is targeting everyone
            if (playerSpellcaster.spellcasterID == evnt.ToSpellcaster || evnt.ToSpellcaster == 8)
            {
                //PanelHolder.instance.displayNotify(evnt.EventName, "Lose " + ((int)(evnt.PercentDmgDecimal * 100)) + "% health", "OK");
                IAllyCastable spell = (IAllyCastable) AllSpellsDict.AllSpells[evnt.Spellname];
                spell.RecieveCastFromAlly(playerSpellcaster);
            }
        
        }       
        
        #endregion
        #region HOST_CALLBACKS
        /*Only the server recieves this event.*/
        public override void OnEvent(SelectSpellcaster evnt)
        {
            BoltConsole.Write("SERVER: Recieved a new character selection event");
            if (evnt.RaisedBy != null)
            {
                //BoltConsole.Write("Sent by: " + evnt.RaisedBy.ToString());
                //Get the connection as a string and update the dictionary using that as the key. 
                string con = evnt.RaisedBy.ToString();
                if (connection_spellcaster.ContainsKey(con))
                {
                    connection_spellcaster[evnt.RaisedBy.ToString()] = evnt.spellcasterID;
                }
                else
                {
                    connection_spellcaster.Add(con, evnt.spellcasterID);
                }
            }

            // Let the gamestate know about the new selected spellcaster and the previous selected one (if any).
            gameStateEntity.GetComponent<NetworkGameState>()
                .onSpellcasterSelected(evnt.spellcasterID, evnt.previousID);

            //Show the start button to the host if all player's have selected their spellcaster
            if (gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
                startGameButton.SetActive(true);
            }
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(CancelSpellcaster evnt)
        {
            gameStateEntity.GetComponent<NetworkGameState>()
                .onSpellcasterCanceled(evnt.spellcasterID);
            if (gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
                startGameButton.SetActive(true);
            }
            else
            {
                startGameButton.SetActive(false);
            }
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(CollectSpellEvent evnt)
        {
            BoltConsole.Write("SERVER: Recieved a new spell collected event");
            gameStateEntity.GetComponent<NetworkGameState>()
                .onCollectedSpell(evnt.SpellcasterID, evnt.SpellName);
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(NextTurnEvent evnt)
        {
            BoltConsole.Write("SERVER: Recieved a new end turn event");
            int nextSpellcaster = gameStateEntity.GetComponent<NetworkGameState>().startNewTurn();
            var nextTurnEvnt = NextPlayerTurnEvent.Create(Bolt.GlobalTargets.Everyone);
            nextTurnEvnt.NextSpellcaster = nextSpellcaster;
            nextTurnEvnt.Send();
        }

        /*Only the server recieves this event.
         Similar to NextTurnEvent, but doesn't update the turn.
         Used for network disconnection.
         Sends the current spellcasterId. 
             */
        public override void OnEvent(NotifyTurnEvent evnt)
        {
            BoltConsole.Write("SERVER: Recieved a Notify turn event");
            int currentSpellcaster = gameStateEntity.GetComponent<NetworkGameState>().getCurrentTurn();
            var nextTurnEvnt = NextPlayerTurnEvent.Create(Bolt.GlobalTargets.Everyone);
            nextTurnEvnt.NextSpellcaster = currentSpellcaster;
            nextTurnEvnt.Send();
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(ReturnPlayerEvent evnt)
        {
            NetworkGameState.instance.actuallyAReturningPlayer();
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(CounterGlobalEvent evnt)
        {
            gameStateEntity.GetComponent<GlobalEvents>()
                .GlobalEventCounter(evnt.EventID, evnt.IsCountered);
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(ItemPickUp evnt)
        {
            gameStateEntity.GetComponent<NetworkGameState>().ItemPickUp();
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(ItemDropOff evnt)
        {
            gameStateEntity.GetComponent<NetworkGameState>().ItemDropOff(evnt.ItemName);
        }


        #endregion
        #region EVENT_CALLS
        /**
         Events, any script can call these.
            Initiates and sends a message across the network, either to the server or everyone.
             */
        public void notifySelectSpellcaster(int spellcasterID, int previous)
        {
            localPlayerSpellcasterID = spellcasterID;
            var selected = SelectSpellcaster.Create(Bolt.GlobalTargets.OnlyServer);
            selected.spellcasterID = spellcasterID;
            selected.previousID = previous;
            selected.Send();
        }

        public void notifyCancelSpellcaster(int spellcasterID)
        {
            var selected = CancelSpellcaster.Create(Bolt.GlobalTargets.OnlyServer);
            selected.spellcasterID = spellcasterID;
            selected.Send();
        }
        public void notifyHostAboutCollectedSpell(int sID, string spellName)
        {
            var spellCollectedEvnt = CollectSpellEvent.Create(Bolt.GlobalTargets.OnlyServer);
            spellCollectedEvnt.SpellcasterID = sID;
            spellCollectedEvnt.SpellName = spellName;
            spellCollectedEvnt.Send();
        }

        public void DealPercentDamage(int spellcasterID, float percentDamage, string evntName)
        {
            var evnt = DealPercentDmgEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.SpellcasterID = spellcasterID;
            evnt.PercentDmgDecimal = percentDamage;
            evnt.EventName = evntName;
            evnt.Send();
        }

        public void CheckIfCanCounter(int spellcasterID, int requiredSpellTier, int evntID)
        {
            var evnt = CheckIfCanCounterEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.requiredSpellcaster = spellcasterID;
            evnt.requiredSpellTier = requiredSpellTier;
            evnt.eventID = evntID;
            evnt.Send();
        }

        public void LetEveryoneKnowCountered(string theSavior)
        {
            var evnt = LetEveryoneKnowAboutCounter.Create(Bolt.GlobalTargets.Everyone);
            evnt.Savior = theSavior;
            evnt.Send();
        }

        public void startFinalBossBattle()
        {
            var evnt = FinalBoss.Create(Bolt.GlobalTargets.Everyone);
            evnt.Send();
        }

        public void notifyAboutNewUpcomingEvent(string eName, string eDesc)
        {
            var evnt = NewUpcomingEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.Name = eName;
            evnt.Description = eDesc;
            evnt.Send();
        }

        //Cast the spell on the ally. Initiates and send a network event.
        //from = ID of this spellcaster
        //to   = ID of ally spellcaster
        public void CastOnAlly(int from, int to, string spellName)
        {
            var evnt = CastOnAllyEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.FromSpellcaster = from;
            evnt.ToSpellcaster = to;
            evnt.Spellname = spellName;
            evnt.Send();
        }

        //For the Item-Scan space
        public void PickUpItem()
        {
            var evnt = ItemPickUp.Create(Bolt.GlobalTargets.OnlyServer);
            evnt.Send();
        }

        //For the Item-Scan space
        public void DropItem(string itemName)
        {
            var evnt = ItemDropOff.Create(Bolt.GlobalTargets.OnlyServer);
            evnt.ItemName = itemName;
            evnt.Send();
        }
        #endregion
    }
}
