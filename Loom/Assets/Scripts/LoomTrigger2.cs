using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomTrigger2 : MonoBehaviour
{
    
    public GameObject pillar1;
    public GameObject nextLoom;

    public GameObject[] fireflies;
    public PathPillar[] path;
    public bool[] lights;
    public int numLights;
    private bool fullyLit;
    
    public float emergeTime;
    private bool emerging;
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = pillar1.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);
        pillar1.SetActive(false);
        nextLoom.SetActive(false);
        lights = new bool[numLights];
    }

    // Update is called once per frame
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
        
        if (emerging)
        {
            t += Time.deltaTime / emergeTime;

            pillar1.transform.position = Vector3.Lerp(startPos, endPos, t);
        }

        if (Vector3.Distance(pillar1.transform.position, endPos) < 0.1f)
        {
            Destroy(transform.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (fullyLit && coll.CompareTag("Player"))
        {

            emerging = true;
            for (int i = 0; i < path.Length; i++)
            {
                path[i].readyToEmerge = true;
                
            }
            pillar1.SetActive(true);
            nextLoom.SetActive(true);

            for (int i = 0; i < fireflies.Length; i++)
            {
                fireflies[i].SetActive(true);
            }
        }

    }
}
