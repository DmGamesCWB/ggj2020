using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume(float volume)
    {
        //audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        //audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetFxVolume(float volume)
    {
        //audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("FxVolume", volume);
    }

}
