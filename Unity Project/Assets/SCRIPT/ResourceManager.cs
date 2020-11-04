using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    
}
[System.Serializable]
public class Resource{
    public float[] resource = new float[6];
}

public enum R{
    Energy,
    Water,
    Material,
    Population,
    Mood
}
