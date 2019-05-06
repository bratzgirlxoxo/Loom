using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPillar : MonoBehaviour
{

    public GameObject nextPillar;
    public bool loomPillar;

    public GameObject[] fireflies;
    public Texture[] blueprints;
    public GameObject riseObject;
   
    private Vector3 startPos;
    private Vector3 endPos;

    private bool imageFlashed;

    public float emergeTime;
    private bool emerging;
    public bool readyToEmerge;

    public ParticleSystem emergingParticles;

    public AK.Wwise.Event PillarAudioEvent;
    public AK.Wwise.Event Loom2RiseEvent;

    public Transform imageObj;
    public Material uiImage;
    public float imgFreq;
    private bool fading;
    
    void Start()
    {
        startPos = nextPillar.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 6.25f, startPos.z);

        if (loomPillar)
        {
            endPos = new Vector3(startPos.x, startPos.y + Mathf.Abs(startPos.y) + 0.75f, startPos.z);
        }
    }

    
    
    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.CompareTag("Player") && readyToEmerge)
        {
            if (loomPillar)
            {
                emergingParticles.Play();
                Loom2RiseEvent.Post(riseObject);
            }
            else
            {
                PillarAudioEvent.Post(gameObject);
            }

            StartCoroutine(PillarEmerge(nextPillar.transform, startPos, endPos));

            StartCoroutine(Fade(0, 0.8f));
            StartCoroutine(FlashImages());
            imageFlashed = true;
            
        }
    }

    public IEnumerator PillarEmerge(Transform pillar, Vector3 start, Vector3 end)
    {
        float t = 0f;
        
        for (int i = 0; i < fireflies.Length; i++)
        {
            fireflies[i].SetActive(true);
        }

        while (t < 1f)
        {
            t += Time.deltaTime / emergeTime;
            pillar.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }  
        
        if (loomPillar)
        {
            emergingParticles.Stop();
        }
    }

    IEnumerator FlashImages()
    {
        for (int i = 0; i < blueprints.Length; i++)
        {
            uiImage.SetTexture("_MainTex", blueprints[i]);

            float newX = Random.Range(-1f, 1f);
            float newY = Random.Range(-0.3f, 0.3f);
            imageObj.localPosition = new Vector3(newX, newY, 1f);
            
            yield return new WaitForSeconds(imgFreq);
        }

        StartCoroutine(Fade(0.8f, 0f));
    }
    
    IEnumerator Fade(float start, float end)
    {
        fading = true;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.5f;
            float newOpacity = Mathf.Lerp(start, end, t);
            uiImage.SetFloat("_Opacity", newOpacity);
            yield return null;
        }

        fading = false;
    }
}
