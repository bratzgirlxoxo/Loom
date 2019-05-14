using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomTrigger2 : MonoBehaviour
{
    
    public GameObject pillar1;
    public GameObject nextLoom;
    public ParticleSystem emergingParticles;

    public PathPillar[] path;
    public GameObject[] fireflies;
    public bool[] lights;
    public int numLights;
    private bool fullyLit;
    
    public float emergeTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;

    private bool emerged;
    
    void Start()
    {
        startPos = pillar1.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 4f, startPos.z);
        pillar1.SetActive(false);
        lights = new bool[numLights];

        for (int i = 0; i < path.Length; i++)
        {
            path[i].gameObject.SetActive(false);
        }
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

        if (emerged)
        {
            t += Time.deltaTime;
            if (t > pillar1.GetComponent<PathPillar>().emergeTime)
            {
                emergingParticles.Stop();
                Destroy(transform.gameObject);
            }
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (!emerged && fullyLit && coll.CompareTag("Player"))
        {
            emerged = true;
            for (int i = 0; i < path.Length; i++)
            {
                path[i].readyToEmerge = true;
                
            }
            pillar1.SetActive(true);
            nextLoom.SetActive(true);

            emergingParticles.Play();

            for (int i = 0; i < fireflies.Length; i++)
            {
                fireflies[i].SetActive(true);
            }
            
            StartCoroutine(pillar1.GetComponent<PathPillar>().PillarEmerge(pillar1.transform, startPos, endPos));
        }
    }
}
