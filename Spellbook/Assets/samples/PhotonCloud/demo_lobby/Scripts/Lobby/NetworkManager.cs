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
        protected bool _isCountdown = false;
        protected string _matchName;


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
                    ChangeTo(null);

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

            } catch (Exception e)
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
            BoltNetwork.RegisterTokenClass<RoomProtocolToken>();
            BoltNetwork.RegisterTokenClass<ServerAcceptToken>();
            BoltNetwork.RegisterTokenClass<ServerConnectToken>();
        }

        public override void BoltStartDone()
        {
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
                BoltNetwork.EnableLanBroadcast();
                // Setup Host
                infoPanel.gameObject.SetActive(false);
                //PanelHolder.instance.hideConnectingPanel();
                ChangeTo(lobbyPanel);

                backDelegate = Stop;
                SetServerInfo("Host", "");

                //SoundManager.instance.musicSource.Play();

                // Build Server Entity
                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.CharacterSelectionEntity);
                entity.TakeControl();

                gameStateEntity = BoltNetwork.Instantiate(BoltPrefabs.GameState);
                gameStateEntity.TakeControl();
                gameStateEntity.GetComponent<NetworkGameState>().onCreateRoom(_matchName);
                
                numPlayersInfo.text = gameStateEntity.GetComponent<NetworkGameState>().onPlayerJoined()+ "";


            } else if (BoltNetwork.IsClient)
            {
                backDelegate = Stop;
                SetServerInfo("Client", "");
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            _matchName = "";

            if (BoltNetwork.IsServer)
            {
                BoltNetwork.LoadScene(lobbyScene.SimpleSceneName);
            }
            else if (BoltNetwork.IsClient)
            {
                SceneManager.LoadScene(lobbyScene.SimpleSceneName);
            }

            registerDoneCallback(() => {
                Debug.Log("Shutdown Done");
                ChangeTo(mainMenuPanel);
            });
        }

        // --- Countdown management
        /*
        void VerifyReady()
        {
            if (!LobbyPlayerList.Ready) { return; }

            bool allReady = true;
            int readyCount = 0;

            foreach (LobbyPhotonPlayer player in LobbyPlayerList._instance.AllPlayers)
            {
                allReady = allReady && player.IsReady;

                if (!allReady) { break; }

                readyCount++;
            }

            if (allReady && readyCount >= minPlayers)
            {
                _isCountdown = true;
                StartCoroutine(ServerCountdownCoroutine());
            }
        }*/


        /*Called from host Start button*/
        public void onGameStart()
        {
            if (!gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
                return;
            }
            //NetworkGameState.instance.determineTurnOrder();
            gameStateEntity.GetComponent<NetworkGameState>().globalEvents.determineGlobalEvents();
            //lobbyPanel.gameObject.SetActive(false);
            
            _isCountdown = true;
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

            ChangeTo(countdownPanel.GetComponent<RectTransform>()) ;
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

        // ----------------- Client callbacks ------------------
        public override void OnEvent(NextPlayerTurnEvent evnt)
        {
            Debug.Log("Recieved Event!!!!!!!!!!!!!!!!!");
            BoltConsole.Write("Recieved Event!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            playerEntity.GetComponent<Player>().nextTurnEvent(evnt.NextSpellcaster);
        }

        public override void OnEvent(LobbyCountdown evnt)
        {
            if(_isCountdown == false)
            {
                //lobbyPanel.gameObject.SetActive(false);
            }
            ChangeTo(countdownPanel.GetComponent<RectTransform>()) ;
            _isCountdown = true;
            Text text = countdownPanel.UIText;
            text.fontSize = 40;
            text.text = "Match Starting in " + evnt.Time;
            countdownPanel.gameObject.SetActive(evnt.Time != 0);
        }

        public override void OnEvent(NewUpcomingEvent evnt)
        {
            Debug.Log("New Upcoming event : " + evnt.Name);
            BoltConsole.Write("New Upcoming event : " + evnt.Name + " cool");
            //PanelHolder.instance.displayNotify(evnt.Name, evnt.Description);
            PanelHolder.instance.displayNotify("Global Event Coming Soon", NetworkGameState.instance.getEventInfo(), "OK");
        }

        // Player recieves this event from the network, the event contains
        // the spellcasterID  of the spellcaster that can counter the event
        // if this client's spellcasterId matches that ID then check if they 
        // can counter it.
        public override void OnEvent(CheckIfCanCounterEvent evnt)
        {
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            Debug.Log("Check Counter Event");
            if (playerSpellcaster.spellcasterID == evnt.requiredSpellcaster)
            {
                var counterEvent = CounterGlobalEvent.Create(GlobalTargets.OnlyServer);
                counterEvent.EventID = evnt.eventID;
                foreach (Spell spell in playerSpellcaster.chapter.spellsCollected)
                {
                    if(spell.iTier >= evnt.requiredSpellTier)
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

        public override void OnEvent(LetEveryoneKnowAboutCounter evnt)
        {

            if(playerSpellcaster.classType == evnt.Savior)
            {
                BoltConsole.Write("You saved the world with your spell!");
                Debug.Log("You saved the world with your spell!");
                PanelHolder.instance.displayNotify("Congratulations", "You saved the world with your spell!", "OK");
            }
            else
            {
                BoltConsole.Write(evnt.Savior+" saved the world!");
                Debug.Log(evnt.Savior + " saved the world!");
                PanelHolder.instance.displayNotify("Congratulations", evnt.Savior + " saved the world!", "OK");
            }
        }

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

        public override void OnEvent(FinalBoss evnt)
        {
            BoltConsole.Write("Final Boss battle (not yet implemented so everyone dies)");
            Debug.Log("Final Boss battle (not yet implemented so everyone dies)");
            PanelHolder.instance.displayNotify("Final Boss Battle", "(not yet implemented so everyone dies)", "OK");
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            playerSpellcaster.TakeDamage((int)(playerSpellcaster.fCurrentHealth));
            SpellCaster.savePlayerData(playerSpellcaster);
        }

        public override void OnEvent(DealPercentDmgEvent evnt)
        {
            playerSpellcaster = playerEntity.GetComponent<Player>().spellcaster;
            if (playerSpellcaster.spellcasterID == evnt.SpellcasterID)
            {
                PanelHolder.instance.displayNotify(evnt.EventName, "Lose " +((int) (evnt.PercentDmgDecimal * 100)) +"% health", "OK");
                playerSpellcaster.TakeDamage((int) (playerSpellcaster.fCurrentHealth * evnt.PercentDmgDecimal));
                SpellCaster.savePlayerData(playerSpellcaster);
                try
                {
                    GameObject health = GameObject.Find("text_healthvalue");
                    if(health != null)
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

        /*Only the server recieves this event.*/
        public override void OnEvent(SelectSpellcaster evnt)
        {
            BoltConsole.Write("SERVER: Recieved a new character selection event");
            Debug.Log("New selection event");
            gameStateEntity.GetComponent<NetworkGameState>()
                .onSpellcasterSelected(evnt.spellcasterID, evnt.previousID);
            if (gameStateEntity.GetComponent<NetworkGameState>().allPlayersSelected())
            {
            Debug.Log("start button setactive");
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

        public override void EntityReceived(BoltEntity entity)
        {
            BoltConsole.Write("EntityReceived");
        }

        public override void EntityAttached(BoltEntity entity)
        {
            BoltConsole.Write("EntityAttached");

            if (!entity.isControlled)
            {
                LobbyPhotonPlayer photonPlayer =  entity.gameObject.GetComponent<LobbyPhotonPlayer>();

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
            LobbyPhotonPlayer player = connection.GetLobbyPlayer();
            if (player != null)
            {
                BoltLog.Info("Disconnected");

                player.RemovePlayer();
            }
        }

        public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
        {
        }

        // Spawner
        private void SpawnGamePlayer()
        {
            playerEntity = BoltNetwork.Instantiate(BoltPrefabs.LocalPlayer); //, pos, Quaternion.identity);
            playerEntity.TakeControl();
            playerEntity.GetComponent<Player>().setup(localPlayerSpellcasterID);
            PanelHolder.instance.displayNotify("Global Event Coming Soon", NetworkGameState.instance.getEventInfo(), "OK");
        }

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
    }
}
