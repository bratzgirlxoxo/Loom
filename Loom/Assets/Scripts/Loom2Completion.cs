using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom2Completion : MonoBehaviour
{

    public bool onRoof;
    public GameObject[] particleObjects;
    public float riseTime;
    public ParticleBells bells;

    public TutorialGhost2 lantern2;

    private Vector3 endPos;

    private bool risen;
    private bool faded;
    
    public AK.Wwise.Event Loom2RisEvent;
    
    void Update()
    {
        if (onRoof)
        {
            onRoof = false;
            bells.end = true;
            StartCoroutine(riseUP());
        }
        
        
        if (risen)
        {
            risen = false;
            StartCoroutine(fadeAway());
        }

        if (faded)
        {
            faded = false;
        }
    }

    IEnumerator riseUP()
    {
        Loom2RisEvent.Post(transform.gameObject);
        lantern2.transform.parent = bells.transform.parent;
        transform.position = particleObjects[0].transform.position;
        Vector3 startPosition = particleObjects[0].transform.position;
        Vector3 fireballStart = transform.position;
        endPos = new Vector3(startPosition.x, startPosition.y + 12f, startPosition.z);
        
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / riseTime;
            for (int i = 0; i < particleObjects.Length; i++)
            {
                particleObjects[i].transform.position = Vector3.Lerp(startPosition, endPos, t);
            }
            transform.position = Vector3.Lerp(fireballStart, endPos, t);
            yield return null;
        }

        risen = true;
    }

    IEnumerator fadeAway()
    {
        Vector3 startScale = particleObjects[0].transform.localScale;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / riseTime;
            for (int i = 0; i < particleObjects.Length; i++)
            {
                particleObjects[i].transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            }
            yield return null;
        }
        
        for (int i = 0; i < particleObjects.Length; i++)
        {
            Destroy(particleObjects[i]);
        }

        transform.localScale = Vector3.one;

        faded = true;
        StartCoroutine(lantern2.Birth(endPos));
    }
    
}
