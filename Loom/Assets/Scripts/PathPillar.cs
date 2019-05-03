using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPillar : MonoBehaviour
{

    public GameObject nextPillar;
    public bool loomPillar;

    public GameObject[] fireflies;
    public GameObject riseObject;
   
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;

    private bool imageFlashed;
    
    

    public float emergeTime;
    private bool emerging;
    public bool readyToEmerge;

    public ParticleSystem emergingParticles;

    public AK.Wwise.Event PillarAudioEvent;
    public AK.Wwise.Event Loom2RiseEvent;
    
    void Start()
    {
        startPos = nextPillar.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);

        if (loomPillar)
        {
            endPos = new Vector3(startPos.x, startPos.y + Mathf.Abs(startPos.y) + 0.75f, startPos.z);
        }
    }

    void Update()
    {
        
        if (emerging)
        {
            t += Time.deltaTime / emergeTime;

            nextPillar.transform.position = Vector3.Lerp(startPos, endPos, t);
        }
        
        if (Vector3.Distance(nextPillar.transform.position, endPos) < 0.1f)
        {
            emerging = false;
            for (int i = 0; i < fireflies.Length; i++)
            {
                fireflies[i].SetActive(true);
            }

            if (loomPillar)
            {
                emergingParticles.Stop();
            }
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && readyToEmerge)
        {
            emerging = true;


            if (loomPillar)
            {
                emergingParticles.Play();
                Loom2RiseEvent.Post(riseObject);
            }
            else
            {
                PillarAudioEvent.Post(gameObject);
            }
        }
    }
}
