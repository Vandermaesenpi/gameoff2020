using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> songs;
    int index;
    public bool canPlay;

    private void Update() {
        if(!audioSource.isPlaying && canPlay){
            audioSource.PlayOneShot(songs[index]);
            index ++;
            if(index == songs.Count){
                index = 0;
            }
        }
    }
}
