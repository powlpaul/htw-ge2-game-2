using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundValueHolder : MonoBehaviour
{
    private float effectsVolumeScale = 1f;
    private float musicVolumeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //two different functions. the one using value is mainly important for the menu controller 
    //as this objects deals with an absoloute value instead of a scale
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
}
