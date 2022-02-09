using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelection;
    [SerializeField] GameObject optionMenu;
    [SerializeField] private Text MusicVolumeDisplay;
    [SerializeField] private Text EffectsVolumeDisplay;
    [SerializeField] private AudioMaster am;

    private AudioSource[] allAudioSources;

    public SoundValueHolder holder;
    private int MusicVolume = 10;
    private int EffectsVolume = 10;

    void Awake()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    void Start()
    {
        holder = GameObject.Find("SoundValueHolder").GetComponent<SoundValueHolder>();
        MusicVolume = (int)(PlayerPrefs.GetFloat("MusicVolumeScale", 1) * 10);
        EffectsVolume = (int)(PlayerPrefs.GetFloat("EffectsVolumeScale", 1) * 10);
        MusicVolumeDisplay.text = "" + MusicVolume;
        EffectsVolumeDisplay.text = "" + EffectsVolume;
        StopAllAudio();
        am.PlayBackgroundTrack();
    }

    void StopAllAudio()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public void OnStart()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void OnBackOption()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnBackLS()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnOptions()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void OnLevel1()
    {
        SceneManager.LoadScene("DialogueScene");
        mainMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnLevel2()
    {
        SceneManager.LoadScene("DialogueScene2");
        mainMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void MusicButtonClick(bool right)
    {
        if (right && MusicVolume < 10)
        {
            MusicVolume++;

        }
        else if (!right && MusicVolume > 0)
        {
            MusicVolume--;
        }

        MusicVolumeDisplay.text = "" + MusicVolume;
        am.SetMusicVolumeScale(MusicVolume);
        PlayerPrefs.SetFloat("MusicVolumeScale", MusicVolume / 10f);

    }

    public void EffectsButtonClick(bool right)
    {
        Debug.Log("effect button was clicked: " + right);
        if (right && EffectsVolume < 10)
        {
            EffectsVolume++;

        }
        else if (!right && EffectsVolume > 0)
        {
            EffectsVolume--;
        }


        EffectsVolumeDisplay.text = "" + EffectsVolume;
        am.SetEffectsVolumeScale(EffectsVolume);
        PlayerPrefs.SetFloat("EffectsVolumeScale", EffectsVolume / 10f);
    }
}
