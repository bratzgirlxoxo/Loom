﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3EntranceChecker : MonoBehaviour
{
    public Loom3Approach[] approachColls;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            for (int i = 0; i < approachColls.Length; i++)
            {
                if (!approachColls[i].hasHit)
                {
                    approachColls[i].Activate();
                }
            }
            Destroy(transform.gameObject);
        }
    }
}
