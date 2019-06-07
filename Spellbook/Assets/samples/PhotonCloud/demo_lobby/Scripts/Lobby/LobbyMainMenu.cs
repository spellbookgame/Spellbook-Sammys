using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bolt.Samples.Photon.Lobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public NetworkManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField matchNameInput;
        public Button CreateButton;
        public GameObject gameTitle;

        public void OnEnable()
        {
            lobbyManager.topPanel.ToggleVisibility(true);

            matchNameInput.onValueChanged.RemoveAllListeners();
            matchNameInput.onValueChanged.AddListener(OnValueGameNameChanged);

            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(OnEndEditGameName);
        }

        public void OnClickHost()
        {
            //lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
            //lobbyManager.ChangeTo(lobbyPanel);

            //lobbyManager.networkAddress = ipInput.text;
            //lobbyManager.StartClient();


            //lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            //lobbyManager.DisplayIsConnecting();


            //lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        public void OnClickDedicated()
        {
            //lobbyManager.ChangeTo(null);
            //lobbyManager.StartServer();


            //lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            //lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        }

        public void OnClickCreateMatchmakingGame()
        {
            lobbyManager.CreateMatch(matchNameInput.text);

            lobbyManager.backDelegate = NetworkManager.s_Singleton.Stop;
            lobbyManager.DisplayIsConnecting();


            lobbyManager.SetServerInfo("Matchmaker Host", NetworkManager.s_Singleton.matchHost);

            gameTitle.SetActive(false);
        }

        public void OnClickOpenServerList()
        {
            lobbyManager.StartClient();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);

            gameTitle.SetActive(false);
        }

        public void OnClickJoinRandom()
        {
            //lobbyManager.StartClient();
            //lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
        }

        void OnEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return) && text.Length != 0)
            {
                OnClickCreateMatchmakingGame();
            }
        }


        void OnValueGameNameChanged(string text)
        {
            CreateButton.interactable = text.Length != 0;
        }

    }
}
