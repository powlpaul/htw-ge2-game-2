using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    //this static singleton variable has the goal to make audioClips accesible form anywhere, and the audioClips savable in this space
    public static AudioMaster AM;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip shotSoundEffect;
    [SerializeField] private AudioClip turretPlacedEffect;
    [SerializeField] private AudioClip turretClickEffect;
    [SerializeField] private AudioClip backGroundTrack;
    [SerializeField] private AudioClip boomerangSoundEffect;
    [SerializeField] private AudioClip wizardSoundEffect;

    private AudioSource musicAudioSource;
    private AudioSource effectAudioSource;
    private SoundValueHolder holder;
    private float effectsVolumeScale = 1f;
    private float musicVolumeScale = 1f;
    // Start is called before the first frame update

    void Awake()
    {
        if (AM != null)
            GameObject.Destroy(AM);
        else
            AM = this;

        DontDestroyOnLoad(this);
    }
    void Start()
    {
        AudioSource[] soundSources = GetComponents<AudioSource>();
        musicAudioSource = soundSources[0];
        effectAudioSource = soundSources[1];
        musicVolumeScale = PlayerPrefs.GetFloat("MusicVolumeScale", 1);
        effectsVolumeScale = PlayerPrefs.GetFloat("EffectsVolumeScale", 1);
    }

    // Update is called once per frame
    void Update()
    {
        musicAudioSource.volume = musicVolumeScale;
        effectAudioSource.volume = effectsVolumeScale;
    }

    public void SetEffectsVolumeScale(int value)
    {
        effectsVolumeScale = value / 10f;
    }

    public void SetEffectsVolumeScale(float scale)
    {
        effectsVolumeScale = scale;
    }

    public float GetEffectsVolumeScale()
    {
        return effectsVolumeScale;
    }
    public void SetMusicVolumeScale(int value)
    {
        musicVolumeScale = value / 10f;
        Debug.Log(musicVolumeScale);
    }

    public void SetMusicVolumeScale(float scale)
    {
        musicVolumeScale = scale;
    }

    public float GetMusicVolumeScale()
    {
        return musicVolumeScale;
    }
    public void PlayAudioClip()
    {
        Debug.LogError("FUCK");
    }

    public void PlayShotSoundEffect()
    {
        audioSource.PlayOneShot(shotSoundEffect, 0.5f);
    }
    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSound, 0.5f);
    }

    public void PlayTurretClickSound()
    {
        audioSource.PlayOneShot(turretClickEffect, 1f);
    }

    public void PlayBackgroundTrack(){
        audioSource.loop = true;
        audioSource.clip = backGroundTrack;
        audioSource.Play();
    }

    public void PlayBoomerangTrack()
    {
        audioSource.PlayOneShot(boomerangSoundEffect, 0.5f);
    }

    public void PlayWizardTrack()
    {
        audioSource.PlayOneShot(wizardSoundEffect, 0.5f);
    }
}
