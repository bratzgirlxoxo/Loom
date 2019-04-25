using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomTrigger : MonoBehaviour
{

    public GameObject pillar1;
    public GameObject nextLoom;

    public GameObject[] fireflies;

    private Vector3 startPos;

    private Vector3 endPos;

    private float t;

    public float emergeTime;

    private bool emerging;

    private bool fullyLit;
    private bool cutSceneReady;
    public int numLights;

    public bool[] lights;
    public PathPillar[] path;

    public Loom1Completion cutScene;

    public AK.Wwise.Event OceanSplashEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = pillar1.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);
        lights = new bool[numLights];
        cutSceneReady = true;
        pillar1.SetActive(false);
        //loom.SetActive(false);
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

        /*        
        if (fullyLit && cutSceneReady)
        {
            cutScene.ready = true;
            cutSceneReady = false;
        }
        */
        

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
        if (coll.CompareTag("Player") && fullyLit)
        {
            OceanSplashEvent.Post(gameObject);
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
