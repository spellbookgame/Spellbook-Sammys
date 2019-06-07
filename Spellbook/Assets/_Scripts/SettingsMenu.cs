using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Start()
    {
        musicSource = SoundManager.instance.musicSource;
        sfxSource = SoundManager.instance.efxSource;

        // set the slider values to current volume
        musicSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;

        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
            UICanvasHandler.instance.ActivateSpellbookButtons(true);
        });
    }

    public void SetMusicVolume(float volume) 
    {
        musicSource.volume = volume;
    }

    public void SetSoundVolume(float volume) 
    {
        sfxSource.volume = volume;
    }

    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
