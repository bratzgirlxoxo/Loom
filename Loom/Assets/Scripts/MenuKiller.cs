using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuKiller : MonoBehaviour
{
    public GameObject menuCandle;
    public GameObject menuCanvas;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Destroy(menuCandle);
            Destroy(menuCanvas);
            Destroy(transform.gameObject);
        }
    }
}
