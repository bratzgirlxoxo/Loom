﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhost2 : MonoBehaviour
{

    public Light myLight;
    private Rigidbody rBody;

    public Transform[] positionTransforms;
    public float[] lerpTimes;
    private Vector3[] positions;
    
    public AnimationCurve runAnimationCurve;

    public MeshRenderer mRend;

    private bool moving;
    private int stageIdx = -1;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

        myLight.enabled = false;
        mRend.enabled = false;
        rBody.useGravity = false;
    }

    public IEnumerator Birth(Vector3 startPos)
    {
        transform.position = startPos;
        myLight.enabled = true;
        mRend.enabled = true;
        myLight.transform.GetComponent<LightIntensityFlicker>().flame.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        rBody.useGravity = true;
        stageIdx++;
        
        positions = new Vector3[positionTransforms.Length + 1];
        for (int i = 0; i < positionTransforms.Length; i++)
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
            Vector3 nextLerpPos = Vector3.Lerp(start, finish, runAnimationCurve.Evaluate(t));

            transform.position = nextLerpPos;
            yield return null;
        }
        moving = false;
        stageIdx++;
    }
    
    IEnumerator Death()
    {
        moving = true;
        myLight.enabled = false;
        myLight.transform.GetComponent<LightIntensityFlicker>().flame.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        Vector3 startPos = transform.position;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(LinearLerp(startPos, startPos + new Vector3(0f, -80f, 0f), 10f));
        yield return new WaitForSeconds(6f + 1f);
        Destroy(transform.gameObject);
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
            if (stageIdx < 2)
                StartCoroutine(LinearLerp(positions[stageIdx], positions[stageIdx + 1], lerpTimes[stageIdx]));
            else
            {
                StartCoroutine(Death());
            }
        }
    }
}
