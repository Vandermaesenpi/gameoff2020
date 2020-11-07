using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{
    public bool compute = false;
    public bool computeStar = false;
    public bool computeConnections = false;
    public bool computeConnectionsBuilding = false;
    
    public float radius;
    public List<BuildingSpot> buildingSpots;
    
    public Transform starParent;
    public GameObject starPrefab;
    public int starNumber;
    public float startDistance;
    public Vector2 sizeRandom;
    public Gradient starColor;

    public List<Connection> connections;
    public float detectionRadius;
    public float offsetRadius;
    public float offsetZ;

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

        if(computeConnections){
            int connectionMade = 0;
            foreach (BuildingSpot spotStart in buildingSpots)
            {
                foreach (BuildingSpot spotEnd in buildingSpots)
                {
                    if(Vector3.Distance(spotStart.transform.position, spotEnd.transform.position) < detectionRadius && spotEnd != spotStart){
                        bool newConnection = true;
                        foreach (Connection connection in connections)
                        {
                            if(connection.spotEnd == spotEnd && connection.spotStart == spotStart || connection.spotStart == spotEnd && connection.spotEnd == spotStart){
                                newConnection = false;
                            }
                        }
                        if(connectionMade < connections.Count && newConnection){
                            connections[connectionMade].spotEnd = spotEnd;
                            connections[connectionMade].spotStart = spotStart;
                            Vector3 endPos = spotEnd.transform.position;
                            Vector3 startPos = spotStart.transform.position;
                            connections[connectionMade].transform.position = endPos + (startPos - endPos)/2f;
                            connections[connectionMade].transform.forward = connections[connectionMade].transform.position - endPos;
                            connections[connectionMade].transform.position += connections[connectionMade].transform.position.normalized * offsetZ;
                            connectionMade ++;
                        }
                    }
                    
                }
            }
            computeConnections = false;
        }

        if(computeConnectionsBuilding){
            foreach (BuildingSpot spot in buildingSpots)
            {
                spot.connections.Clear();
            }
            foreach (Connection item in connections)
            {
                item.spotStart.connections.Add(item);
                item.spotEnd.connections.Add(item);
            }
            computeConnectionsBuilding = false;
        }
    }
}
