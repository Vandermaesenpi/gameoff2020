using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource source;
    public AudioSource sourceFaithfull;
    public List<AudioClip> clips;
    public void Play(SFX sfx){
        Play(sfx, 1f);
    }
    public void Play(SFX sfx, float pitch){
        source.pitch = pitch;
        source.PlayOneShot(clips[(int)sfx]);
    }

    public void Play(AudioClip clip){
        source.pitch = 1;
        source.PlayOneShot(clip);
    }

    public void PlayFaithfull(SFX sfx){
        sourceFaithfull.PlayOneShot(clips[(int)sfx]);
    }

    public void SetVolume(float amount){
        source.volume = amount;
        sourceFaithfull.volume = amount;
        if(!source.isPlaying)
        Play(SFX.Talk);
    }
}

public enum SFX{
    Alert,
    Pause,
    Play,
    PlayFast,
    Bip,
    Bop,
    Tic,
    Tuc,
    Build,
    Talk,
    ShieldUp,
    SuperNova
}
