using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM _instance;
    public static GM I
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GM>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("GM");
                    _instance = container.AddComponent<GM>();
                }
            }
            return _instance;
        }
    }
    public MoonRotator moonRotator;
    public CityManager city;
    public UIManager ui;
    public ArtReferences art;
    public GameplayManager gameplay;
    public PopulationManager people;
    public ResourceManager resource;
    public ProjectManager project;
    public EventManager eventManager;
    public SfxManager sfx;
    public AudioManager audioManager;
    public TooltipManager tooltip;
    public IntroManager introManager;
}
