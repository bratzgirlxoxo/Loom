using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraDepthTextureMode : MonoBehaviour
{

    [SerializeField] private DepthTextureMode depthTextureMode;

    void OnValidate()
    {
        SetCamDepthTextureMode();
    }

    void Awake()
    {
        SetCamDepthTextureMode();
    }

    void SetCamDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = depthTextureMode;
    }
}
