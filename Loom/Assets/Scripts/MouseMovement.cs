using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public AK.Wwise.Event OrganPlayEvent;
    public AK.Wwise.Event OrganStopEvent;

    public AK.Wwise.Event Melody2StartEvent;
    public AK.Wwise.Event Melody2StopEvent;

    public Vector2 melody2EmitterPos;

    public bool hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
        OrganPlayEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(Input.GetAxis("Mouse X"), 
            Input.GetAxis("Mouse Y"));
        
        melody2EmitterPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (melody2EmitterPos != Vector2.zero)
        {
            if (!hasPlayed)
            {
                Melody2StartEvent.Post(gameObject);
                hasPlayed = true;
            }

        }

        if (melody2EmitterPos == Vector2.zero)
        {
            Melody2StopEvent.Post(gameObject);
            hasPlayed = false;
        }
    }
}
