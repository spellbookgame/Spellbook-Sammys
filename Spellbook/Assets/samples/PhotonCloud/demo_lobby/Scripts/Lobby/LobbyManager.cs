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

namespace Bolt.Samples.Photon.Lobby
{
    public class LobbyManager : Bolt.GlobalEventListener
    {
        static public LobbyManager s_Singleton;
        BoltEntity characterSelection;
        BoltEntity playerEntity;
        BoltEntity gameStateEntity;
        [Header("Lobby Configuration")]
        public SceneField lobbyScene;
        public SceneField gameScene;
        public int minPlayers = 2;
        public int localPlayerSpellcasterID = -1;

        [Header("UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        [Space]
        [Header("UI Reference")]
        public LobbyTopPanel topPanel;

        public RectTransform mainMenuPanel;
        public RectTransform lobbyPanel;

        public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
        public GameObject addPlayerButton;

        protected RectTransform currentPanel;

        public Button backButton;
        public GameObject startGameButton;
        public GameObject playerController;
        public Text statusInfo;
        public Text hostInfo;
        public Text numPlayersInfo;
        protected bool _isCountdown = false;
        protected string _matchName;

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

                    // Spawn Player
                    SpawnGamePlayer();
                    MainPageHandler.instance.setupMainPage();
                }

            } catch (Exception e)
            {
                BoltConsole.Write(e.Message, Color.red);
                BoltConsole.Write(e.Source, Color.red);
                BoltConsole.Write(e.StackTrace, Color.red);
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
            infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });
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
                ChangeTo(lobbyPanel);

                backDelegate = Stop;
                SetServerInfo("Host", "");

                SoundManager.instance.musicSource.Play();

                // Build Server Entity
                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.CharacterSelectionEntity);
                entity.TakeControl();

                gameStateEntity = BoltNetwork.Instantiate(BoltPrefabs.GameState);
                gameStateEntity.TakeControl();
                
                numPlayersInfo.text = gameStateEntity.GetComponent<NetworkGameState>().onPlayerJoined()+ "";
                startGameButton.SetActive(true);

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

        public void onGameStart()
        {
            _isCountdown = true;
            StartCoroutine(ServerCountdownCoroutine());
        }

        public IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            LobbyCountdown countdown;

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
            countdownPanel.UIText.text = "Match Starting in " + evnt.Time;
            countdownPanel.gameObject.SetActive(evnt.Time != 0);
        }

        public override void OnEvent(PlayerJoinedEvent evnt)
        {
            numPlayersInfo.text = evnt.numOfPlayers + "";
        }

        /*Only the server recieves this event.*/
        public override void OnEvent(SelectSpellcaster evnt)
        {
            BoltConsole.Write("SERVER: Recieved a new character selection event");
            gameStateEntity.GetComponent<NetworkGameState>()
                .onSpellcasterSelected(evnt.spellcasterID, evnt.previousID);
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

        public override void EntityReceived(BoltEntity entity)
        {
            BoltConsole.Write("EntityReceived");
            /*
            if (BoltNetwork.IsServer)
            {
                
                if (entity.StateIs(typeof(ISpellcasterState))){
                    int numPlayers = gameStateEntity.GetComponent<NetworkGameState>().onPlayerJoined();
                    var playerTurnEvnt = PlayerJoinedEvent.Create(Bolt.GlobalTargets.Everyone);
                    playerTurnEvnt.numOfPlayers = numPlayers;
                    playerTurnEvnt.Send();
                }
            }
            */
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

        public override void Connected(BoltConnection connection)
        {
            if (BoltNetwork.IsClient)
            {
                BoltConsole.Write("Connected Client: " + connection, Color.blue);
                infoPanel.gameObject.SetActive(false);
                ChangeTo(lobbyPanel);

                //Spawn local player here? or Below?
                //BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.CharacterSelectionEntity);
                //entity.AssignControl(connection);
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

                /*
                BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.PlayerInfo);

                LobbyPhotonPlayer lobbyPlayer = entity.GetComponent<LobbyPhotonPlayer>();
                lobbyPlayer.connection = connection;

                connection.UserData = lobbyPlayer;
                connection.SetStreamBandwidth(1024 * 1024);

                entity.AssignControl(connection);
                */
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
        }

        public void notifySelectSpellcaster(int spellcasterID, int previous)
        {
            localPlayerSpellcasterID = spellcasterID;
            var selected = SelectSpellcaster.Create(Bolt.GlobalTargets.OnlyServer);
            selected.spellcasterID = spellcasterID;
            selected.previousID = previous;
            selected.Send();
        }

        public void updateNumOfPlayers(int numOfPlayrs)
        {
            BoltConsole.Write("About to update UI but not implemented");
            //TODO: Call a method in SpellCasterLobbyChoose to update the UI to the # of players.
        }

        public void updateNumOfSpellcasters(int numSpellcasters)
        {
            BoltConsole.Write("About to update UI but not implemented");
            //TODO: Call a method in SpellCasterLobbyChoose to update the UI to the # of spellcasters.
        }

    }
}
