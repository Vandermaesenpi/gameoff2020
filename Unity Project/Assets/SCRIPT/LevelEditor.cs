using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{
    public bool compute = false;
    public bool computeStar = false;
    public float radius;
    public List<GameObject> buildingSpots;
    public Transform starParent;
    public GameObject starPrefab;
    public int starNumber;
    public float startDistance;
    public Vector2 sizeRandom;
    public Gradient starColor;

    // Update is called once per frame
    void Update()
    {
        // Place building spots
        if(compute){
            float phi = Mathf.PI * (3f - Mathf.Sqrt(5f));

            for (int i = 0; i < buildingSpots.Count; i++)
            {
                Vector3 newPos = Vector3.zero;
                newPos.y = 1f - ((float)i/(float)(buildingSpots.Count -1)) * 2f;
                float myRadius = Mathf.Sqrt(1f - newPos.y * newPos.y);
                float theta = phi * (float)i;

                newPos.x = Mathf.Cos(theta) * myRadius;
                newPos.z = Mathf.Sin(theta) * myRadius;

                buildingSpots[i].transform.localPosition = newPos * radius;
                buildingSpots[i].transform.up = buildingSpots[i].transform.localPosition - Vector3.zero;
            }
        }

        if(computeStar){
            foreach (Transform child in starParent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < starNumber; i++)
            {
                GameObject newStar = Instantiate(starPrefab, starParent);
                newStar.transform.position = Random.onUnitSphere * startDistance;
                newStar.transform.localScale = Vector3.one * Random.Range(sizeRandom.x, sizeRandom.y);
                newStar.GetComponent<Renderer>().material.color = starColor.Evaluate(Random.value);

            }
            computeStar = false;
        }
    }
}
