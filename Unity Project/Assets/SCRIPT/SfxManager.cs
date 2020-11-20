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

    public void PlayFaithfull(SFX sfx){
        sourceFaithfull.PlayOneShot(clips[(int)sfx]);
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
