using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhost2 : MonoBehaviour
{

    private ParticleSystem myParticles;
    private Light myLight;
    private Rigidbody rBody;

    public Transform[] positionTransforms;

    public float[] lerpTimes;

    private Vector3[] positions;



    private bool moving;
    private int stageIdx = -1;
    
    void Start()
    {
        myParticles = GetComponent<ParticleSystem>();
        myLight = GetComponent<Light>();
        rBody = GetComponent<Rigidbody>();

        myLight.enabled = false;
    }

    public IEnumerator Birth()
    {
        myParticles.Play();
        myLight.enabled = true;
        yield return new WaitForSeconds(0.8f);
        rBody.useGravity = true;
        stageIdx++;
        
        positions = new Vector3[positionTransforms.Length + 1];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i+1] = positionTransforms[i].position;
        }
    }

    IEnumerator LinearLerp(Vector3 start, Vector3 finish, float moveTime)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, finish, t);
            yield return null;
        }
        moving = false;
        stageIdx++;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!moving && coll.CompareTag("Player") && stageIdx < positions.Length)
        {
            moving = true;
            if (stageIdx == 0)
            {
                positions[0] = transform.position;
            }
            StartCoroutine(LinearLerp(positions[stageIdx], positions[stageIdx + 1], lerpTimes[stageIdx]));
        }
    }
}
