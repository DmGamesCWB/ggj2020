using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public enum SoundTypes
    {
        MainTheme,
        CreditsTheme,
        CarHonkSad,
        CarHonkAngry,
        BikeHorn,
        TrafficJam,
        Cursing,
        CursingKlingon,
        HammeringShort,
        HammeringLong,
        RepairingShort,
        RepairingLong,
        Boo,
        Applause,
        Poop
    }

    public SoundTypes name;
    public AudioClip clip;
    public AudioMixerGroup group;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
