using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHandler : MonoBehaviour
{
    public static LoadHandler instance = null;

    public int sceneBuildIndex;
    public int loadSceneIndex;
    public int mainSceneIndex;

    public bool setupComplete;

    #region singleton
    private void Awake()
    {
        //Check if there is already an instance of PanelHolder
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of PanelHolder.
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion
}
