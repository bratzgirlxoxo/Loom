using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomStart : MonoBehaviour
{
    public float startYPos;
    public Transform skyDome;
    
    void Awake()
    {
        StartCoroutine(BakeAndDestroy());
    }

    IEnumerator BakeAndDestroy()
    {
        yield return new WaitForSeconds(3f);
        skyDome.localScale = Vector3.one;
        Vector3 newPos = transform.position;
        newPos.y = startYPos;
        transform.position = newPos;
        gameObject.SetActive(false);
    }
}
