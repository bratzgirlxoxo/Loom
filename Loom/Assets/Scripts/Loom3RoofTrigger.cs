using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3RoofTrigger : MonoBehaviour
{
    public Loom2Completion loom2End;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && !loom2End.onRoof)
        {
            loom2End.onRoof = true;
        }
    }
}
