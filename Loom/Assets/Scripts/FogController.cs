using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FogController : MonoBehaviour
{
    public Transform playerView;
    public Transform Water;

    private float startDensity;

    void Awake()
    {
        startDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        float upAmount = Vector3.Dot(playerView.forward, Water.up);
        RenderSettings.fogDensity = startDensity * (1-upAmount);
        if (RenderSettings.fogDensity < 0.005f)
        {
            RenderSettings.fogDensity = 0.005f;
        }
    }
}
