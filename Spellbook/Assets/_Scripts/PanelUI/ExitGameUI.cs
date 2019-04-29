using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameUI : MonoBehaviour
{
    bool exitPanelOn;
    bool isInFirstScreen = true;  //Flag for being in the very first screen in the game.
    public GameObject ExitPanel;
    public Button ExitGameButton;
    public Button ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        exitPanelOn = false;
        ExitGameButton.onClick.AddListener(ExitGame);
        ContinueButton.onClick.AddListener(ContinuePlaying);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInFirstScreen)
            {
                Camera wc = gameObject.GetComponent<Canvas>().worldCamera;
                if(wc == null)
                {
                    gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                }
            }
            
            if (exitPanelOn)
            {
                //Do Nothing
            }
            else if (isInFirstScreen)
            {
                Application.Quit();
            }
            else
            {
                ExitPanel.SetActive(true);
                exitPanelOn = true;
            }
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void ContinuePlaying()
    {
        ExitPanel.SetActive(false);
        exitPanelOn = false;
    }

    public void OnFirstScreenLeave()
    {
        isInFirstScreen = false;
    }
}
