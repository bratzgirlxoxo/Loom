using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCSend : MonoBehaviour
{

    private Transform playerTransform;
    public float playerHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponentInParent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = playerTransform.position.y;
        AkSoundEngine.SetRTPCValue("PlayerHeight", playerHeight);
    }
}
