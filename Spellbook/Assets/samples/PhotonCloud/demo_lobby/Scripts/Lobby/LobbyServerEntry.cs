﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using UdpKit;

namespace Bolt.Samples.Photon.Lobby
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;

		public void Populate(UdpSession match, NetworkManager lobbyManager, Color c)
		{
            serverInfoText.text = match.HostName;

            slotInfo.text = match.ConnectionsCurrent.ToString() + "/" + match.ConnectionsMax.ToString(); ;

            joinButton.onClick.RemoveAllListeners();
            joinButton.onClick.AddListener(() => { JoinMatch(match, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        void JoinMatch(UdpSession match, NetworkManager lobbyManager)
        {
            BoltNetwork.Connect(match);

            lobbyManager.backDelegate = lobbyManager.Stop;
            lobbyManager.DisplayIsConnecting();
            lobbyManager.matchName = match.HostName;
            joinButton.enabled = false;

            //lobbyManager.infoPanel.Display();
        }
    }
}