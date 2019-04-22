using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPillar : MonoBehaviour
{

    public GameObject nextPillar;
    public bool loomPillar;

    public GameObject[] fireflies;
   
    private Vector3 startPos;
    private Vector3 endPos;
    private float t;


    public float emergeTime;
    private bool emerging;
    public bool readyToEmerge;

    public ParticleSystem emergingParticles;

    public AK.Wwise.Event PillarAudioEvent;
    
    void Start()
    {
        startPos = nextPillar.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);

        if (loomPillar)
        {
            endPos = new Vector3(startPos.x, startPos.y + Mathf.Abs(startPos.y) + 0.75f, startPos.z);
            PillarAudioEvent.Post(gameObject);
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
            PillarAudioEvent.Post(GameObject.Find("Loom2RiseSound"));

            if (loomPillar)
            {
                emergingParticles.Play();
            }
        }
    }
}
