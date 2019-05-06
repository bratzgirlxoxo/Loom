using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomTrigger2 : MonoBehaviour
{
    
    public GameObject pillar1;
    public GameObject nextLoom;

    public PathPillar[] path;
    public bool[] lights;
    public int numLights;
    private bool fullyLit;
    
    public float emergeTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;
    
    void Start()
    {
        startPos = pillar1.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);
        pillar1.SetActive(false);
        nextLoom.SetActive(false);
        lights = new bool[numLights];
    }

    void Update()
    {
        fullyLit = true;
        for (int i = 0; i < numLights; i++)
        {
            if (!lights[i])
            {
                fullyLit = false;
                break;
            }
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (fullyLit && coll.CompareTag("Player"))
        {
            for (int i = 0; i < path.Length; i++)
            {
                path[i].readyToEmerge = true;
                
            }
            pillar1.SetActive(true);
            nextLoom.SetActive(true);

            StartCoroutine(pillar1.GetComponent<PathPillar>().PillarEmerge(pillar1.transform, startPos, endPos));
        }
    }
}
