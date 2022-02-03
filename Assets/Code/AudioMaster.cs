using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{   
    //this static singleton variable has the goal to make audioClips accesible form anywhere, and the audioClips savable in this space
    public static AudioMaster AM;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private  AudioClip enemyDeathSound;
    [SerializeField] private AudioClip shotSoundEffect;
    [SerializeField] private AudioClip turretPlacedEffect;
    [SerializeField] private AudioClip turretClickEffect;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAudioClip()
    {
        Debug.LogError("FUCK");
    }
    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSound, 1f);
    }
    public void PlayTurretClickSound()
    {
        audioSource.PlayOneShot(turretClickEffect, 1f);
    }
}
