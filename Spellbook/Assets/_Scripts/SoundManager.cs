using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    // static clip so any script can access it
    [SerializeField] AudioClip[] soundeffects = new AudioClip[19];

    #region static_clips
    public static AudioClip buttonconfirm;
    public static AudioClip characterswipe;
    public static AudioClip closeinventory;
    public static AudioClip diceroll;
    public static AudioClip dicetrayopen;
    public static AudioClip glyphfound;
    public static AudioClip grabspellpiece;
    public static AudioClip itemfound;
    public static AudioClip manafound;
    public static AudioClip openinventory;
    public static AudioClip pageturn;
    public static AudioClip placespellpiece;
    public static AudioClip questaccept;
    public static AudioClip questfailed;
    public static AudioClip questsuccess;
    public static AudioClip spellbookopen;
    public static AudioClip spellcast;
    public static AudioClip spellcreate;
    public static AudioClip yourturn;
    #endregion

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        // assigning sounds to static audio clips
        buttonconfirm = soundeffects[0];
        characterswipe = soundeffects[1];
        closeinventory = soundeffects[2];
        diceroll = soundeffects[3];
        dicetrayopen = soundeffects[4];
        glyphfound = soundeffects[5];
        grabspellpiece = soundeffects[6];
        itemfound = soundeffects[7];
        manafound = soundeffects[8];
        openinventory = soundeffects[9];
        pageturn = soundeffects[10];
        placespellpiece = soundeffects[11];
        questaccept = soundeffects[12];
        questfailed = soundeffects[13];
        questsuccess = soundeffects[14];
        spellbookopen = soundeffects[15];
        spellcast = soundeffects[16];
        spellcreate = soundeffects[17];
        yourturn = soundeffects[18];

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }
}