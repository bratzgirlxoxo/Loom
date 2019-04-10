using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintGenerator : MonoBehaviour
{

    private ParticleSystem footstepSystem;
    
    void Start()
    {
        footstepSystem = GetComponent<ParticleSystem>();

    }

    void Update()
    {
        ParticleSystem.CustomDataModule customData = footstepSystem.customData;
        customData.enabled = true;
    }
    
    
}
