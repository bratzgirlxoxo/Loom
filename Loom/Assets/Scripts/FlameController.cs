using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FlameController : MonoBehaviour
{
    public FirstPersonController fpsController;
    
    private Camera activeCam;


    void Start()
    {
        activeCam = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {  
        transform.LookAt(activeCam.transform.position);
    }
}
