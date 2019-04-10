using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FlameController : MonoBehaviour
{

    public Camera cutCam;
    public FirstPersonController fpsController;
    
    private Camera activeCam;

    private Vector3 startPos;

    void Start()
    {
        activeCam = Camera.main;
        startPos = transform.forward;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (fpsController.canWalk == false)
        {
            activeCam = cutCam;
        }
        else
        {
            activeCam = Camera.main;
        }
        
        
        transform.LookAt(activeCam.transform.position);
    }
}
