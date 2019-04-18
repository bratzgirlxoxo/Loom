using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3Animation : MonoBehaviour
{

    public TestAnimation objectAnim;

    public ParticleSystem[] fireballs;

    private bool isSpawned;


    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && !isSpawned)
        {
            isSpawned = true;
            StartCoroutine(objectAnim.startAnimation());

            for (int i = 0; i < fireballs.Length; i++)
            {
                fireballs[i].Play();
            }

            StartCoroutine(spawnFireballs());
        }
    }

    IEnumerator spawnFireballs()
    {
        yield return new WaitForSeconds(3f);
        
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].Play();
            fireballs[i].transform.localScale = new Vector3(3f, 3f, 3f);
        }
    }
}
