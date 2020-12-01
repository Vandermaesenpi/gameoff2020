using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioBeacon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioDontDestroy.I.Restart();
    }

}
