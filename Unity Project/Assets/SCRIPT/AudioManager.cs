using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> songs;
    int index;

    private void Update() {
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(songs[index]);
            index ++;
            if(index == songs.Count){
                index = 0;
            }
        }
    }
}
