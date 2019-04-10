using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PostWwise : MonoBehaviour
{
    public AK.Wwise.Event Jump_Up_Event;
    public AK.Wwise.Event Jump_Land_Event;

    private bool hasJustLanded;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if we're on the ground and press space, post jump up event

        if (controller.isGrounded)
        {
            if (!hasJustLanded)
            {
                hasJustLanded = true;
                Jump_Land_Event.Post(gameObject);
            }
        }
        else
        {
            if (hasJustLanded)
            {
                hasJustLanded = false;
                Jump_Up_Event.Post(gameObject);
            }
            
        }
        
    }
}
