using Bolt;
using Bolt.Samples.Photon.Lobby;
using UnityEngine;
using UnityEngine.UI;
/*
    Written by Moises Martinez moi.compsci@gmail.com
     */
namespace Photon.Lobby
{
    public class SpellCasterLobbyChoose : Bolt.EntityEventListener<ILobbySpellcasterSelectState>
    {
        // Bolt
        public BoltConnection connection;

        // Lobby
        public NetworkManager lobbyManager;

        // By default, no body has selected anything yet.
        public bool alchemistChosen = false;
        public bool arcanistChosen = false;
        public bool elementalistChosen = false;
        public bool chronomancerChosen = false;
        public bool illusionistChosen = false;
        public bool summonerChosen = false;
        public bool selectActivated = false;

        // To avoid filling the input queue, let's check if the player clicked on something first.
        private bool newClick = false;

        public Button alchemistButton;
        public Button arcanistButton;
        public Button elementalistButton;
        public Button chronomancerButton;
        public Button illusionistButton;  //aka tricksterButton
        public Button summonerButton;

        public GameObject alchemistSelection;
        public GameObject arcanistSelection;
        public GameObject elementalSelection;
        public GameObject chronomancerSelection;
        public GameObject illusionistSelection;
        public GameObject summonerSelection;
        public GameObject lastSelectedUI;
        public GameObject currentSelectedUI;



        public Button selectButton;
        public Button startGameButton;
        public Text text;
        //public Text text_numOfPlayers_join;
        public int numOfPlayers = 0;

        public Color alchemistColor = Color.green;
        public Color arcanistColor = Color.magenta;
        public Color elementalistColor = Color.red;
        public Color chronomancerColor = Color.yellow; //will change to brown later.
        public Color illusionistColor = Color.cyan;
        public Color summonerColor = Color.blue;

        public Color unselectedColor = Color.grey; // = Color.white;

        // Keep track of what the local player chooses.
        int previousSelected = -1;
        public int currentSelected = -1;

        // Handlers
        public override void Attached()
        {
            try
            {
                //Innefficient, for demoing purposes.
                lobbyManager = GameObject.Find("LobbyManager").GetComponent<NetworkManager>();
                alchemistButton = GameObject.Find("button_alchemist").GetComponent<Button>();
                arcanistButton = GameObject.Find("button_arcanist").GetComponent<Button>();
                elementalistButton = GameObject.Find("button_elementalist").GetComponent<Button>();
                chronomancerButton = GameObject.Find("button_chronomancer").GetComponent<Button>();
                illusionistButton = GameObject.Find("button_trickster").GetComponent<Button>();
                summonerButton = GameObject.Find("button_summoner").GetComponent<Button>();

                text = GameObject.Find("ChooseClass").GetComponent<Text>();

                alchemistSelection = GameObject.Find("Image_Alchemist");
                arcanistSelection = GameObject.Find("Image_Arcanist");
                elementalSelection = GameObject.Find("Image_Elementalist");
                chronomancerSelection = GameObject.Find("Image_Chronomancer");
                illusionistSelection = GameObject.Find("Image_Illusionist");
                summonerSelection = GameObject.Find("Image_Summoner");

                // A callback is basically another way of saying "getting an update from the network"
                state.AddCallback("AlchemistSelected", () =>
                {
                    if (state.AlchemistSelected)
                    {
                        alchemistButton.image.color = alchemistColor;
                        alchemistButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        alchemistButton.image.color = unselectedColor;
                        alchemistButton.interactable = true;
                    }
                });

                state.AddCallback("ArcanistSelected", () =>
                {
                    if (state.ArcanistSelected)
                    {
                        arcanistButton.image.color = arcanistColor;
                        arcanistButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        arcanistButton.image.color = unselectedColor;
                        arcanistButton.interactable = true;
                    }
                });

                state.AddCallback("ElementalistSelected", () =>
                {
                    if (state.ElementalistSelected)
                    {
                        elementalistButton.image.color = elementalistColor;
                        elementalistButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        elementalistButton.image.color = unselectedColor;
                        elementalistButton.interactable = true;
                    }
                });

                state.AddCallback("ChronomancerSelected", () =>
                {
                    if (state.ChronomancerSelected)
                    {
                        chronomancerButton.image.color = chronomancerColor;
                        chronomancerButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        chronomancerButton.image.color = unselectedColor;
                        chronomancerButton.interactable = true;
                    }
                });

                state.AddCallback("IllusionistSelected", () =>
                {
                    if (state.IllusionistSelected)
                    {
                        illusionistButton.image.color = illusionistColor;
                        illusionistButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        illusionistButton.image.color = unselectedColor;
                        illusionistButton.interactable = true;
                    }
                });

                state.AddCallback("SummonerSelected", () =>
                {
                    if (state.SummonerSelected)
                    {
                        summonerButton.image.color = summonerColor;
                        summonerButton.interactable = false;
                    }
                    else
                    {
                        unselectedColor.a = 1;
                        summonerButton.image.color = unselectedColor;
                        summonerButton.interactable = true;
                    }
                });
            }
            catch
            {
                //Loading previous game
            }
        }

        public override void ControlGained()
        {
            BoltConsole.Write("ControlGained", Color.blue);
            try
            {
                SetupCharacterSelectionUI();
            }
            catch
            {
                //loading previous game.
            }

        }

        public override void SimulateController()
        {
            if (newClick)
            {
                newClick = false;
                ISpellcasterSelectCommandInput input = SpellcasterSelectCommand.Create();
                input.alchemistChosen = alchemistChosen;
                input.arcanistChosen = arcanistChosen;
                input.elementalistChosen = elementalistChosen;
                input.chronomancerChosen = chronomancerChosen;
                input.illusionistChosen = illusionistChosen;
                input.summonerChosen = summonerChosen;
                entity.QueueInput(input);
            }
        }

        public override void ExecuteCommand(Command command, bool resetState)
        {
            // May have to delete this after testing.
            if (!entity.isOwner) { return; }

            if (!resetState && command.IsFirstExecution)
            {
                SpellcasterSelectCommand selectCommand = command as SpellcasterSelectCommand;
                state.AlchemistSelected = selectCommand.Input.alchemistChosen;
                state.ArcanistSelected = selectCommand.Input.arcanistChosen;
                state.ElementalistSelected = selectCommand.Input.elementalistChosen;
                state.ChronomancerSelected = selectCommand.Input.chronomancerChosen;
                state.IllusionistSelected = selectCommand.Input.illusionistChosen;
                state.SummonerSelected = selectCommand.Input.summonerChosen;
            }
        }

        public void SetupCharacterSelectionUI()
        {
            BoltConsole.Write("SetupPlayer", Color.green);
            Debug.Log("SetupPlayer");

            this.transform.SetParent(GameObject.Find("LobbyPanel").transform);

            //Hardcoded for prototyping and testing
            this.transform.localPosition = new Vector3(200f, -800f, 0f);
            this.transform.localScale = new Vector3(.6f, .6f, 1f);

            alchemistButton.onClick.RemoveAllListeners();
            alchemistButton.onClick.AddListener(OnAlchemistClicked);

            arcanistButton.onClick.RemoveAllListeners();
            arcanistButton.onClick.AddListener(OnArcanistClicked);

            elementalistButton.onClick.RemoveAllListeners();
            elementalistButton.onClick.AddListener(OnElementalistClicked);

            chronomancerButton.onClick.RemoveAllListeners();
            chronomancerButton.onClick.AddListener(OnChronomancerClicked);

            illusionistButton.onClick.RemoveAllListeners();
            illusionistButton.onClick.AddListener(OnIllusionistClicked);

            summonerButton.onClick.RemoveAllListeners();
            summonerButton.onClick.AddListener(OnSummonerClicked);

        }

        // UI
        public void UIupdate()
        {
            if (!selectActivated)
            {
                selectActivated = true;
                lobbyManager.activateSelectButton(true);
                selectButton = lobbyManager.selectButton;
                selectButton.onClick.RemoveAllListeners();
                selectButton.onClick.AddListener(onSelectClicked);
            }
            previousSelected = currentSelected;
            lastSelectedUI = currentSelectedUI;
            if(lastSelectedUI != null)
            {
                lastSelectedUI.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }
            openPreviousSpellcaster();
        }
        public void OnAlchemistClicked()
        {
            if (state.AlchemistSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 0;
            currentSelectedUI = alchemistSelection;
            alchemistChosen = true;
            text.text = "Alchemist";
            alchemistSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1); //Why 1.17? Because it looks good.
        }

        public void OnArcanistClicked()
        {
            if (state.ArcanistSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 1;
            currentSelectedUI = arcanistSelection;
            arcanistChosen = true;
            text.text = "Arcanist";
            arcanistSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1);
        }

        public void OnElementalistClicked()
        {
            if (state.ElementalistSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 2;
            currentSelectedUI = elementalSelection;
            elementalistChosen = true;
            text.text = "Elementalist";
            elementalSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1);
        }

        public void OnChronomancerClicked()
        {
            if (state.ChronomancerSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 3;
            currentSelectedUI = chronomancerSelection;
            chronomancerChosen = true;
            text.text = "Chronomancer";
            chronomancerSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1);
        }

        public void OnIllusionistClicked()
        {
            if (state.IllusionistSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 4;
            currentSelectedUI = illusionistSelection;
            illusionistChosen = true;
            text.text = "Illusionist";
            illusionistSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1);
        }

        public void OnSummonerClicked()
        {
            if (state.SummonerSelected)
            {
                text.text = "This spellcaster is taken.";
                return;
            }
            UIupdate();
            currentSelected = 5;
            currentSelectedUI = summonerSelection;
            summonerChosen = true;
            text.text = "Summoner";
            summonerSelection.GetComponent<RectTransform>().localScale = new Vector3(1.17f, 1.17f, 1);
        }


        /*Called when local player confirms their character*/
        public void onSelectClicked()
        {
            newClick = true;
            lobbyManager.notifySelectSpellcaster(currentSelected, previousSelected);
        }
        
        void openPreviousSpellcaster()
        {
            switch (previousSelected)
            {
                case 0:
                    alchemistChosen = false;
                    break;
                case 1:
                    arcanistChosen = false;
                    break;
                case 2:
                    elementalistChosen = false;
                    break;
                case 3:
                    chronomancerChosen = false;
                    break;
                case 4:
                    illusionistChosen = false;
                    break;
                case 5:
                    summonerChosen = false;
                    break;
            }
        }

    }

}