using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource resources;
}
[System.Serializable]
public class Resource{
    public float[] resource = new float[3];
}

public enum R{
    Energy,
    Water,
    Material,
}
