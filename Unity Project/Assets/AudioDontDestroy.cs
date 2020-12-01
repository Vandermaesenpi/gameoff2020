using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDontDestroy : MonoBehaviour
{
    public AudioSource source;
    public static AudioDontDestroy _instance;
    public static AudioDontDestroy I
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioDontDestroy>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("AudioDontDestroy");
                    _instance = container.AddComponent<AudioDontDestroy>();
                }
            }
            return _instance;
        }
    }

    private void Awake(){
        if(I != null && I != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Restart(){
        source.volume = 1;
        source.Play();
    }
}
