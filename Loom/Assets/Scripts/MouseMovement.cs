using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public AK.Wwise.Event OrganPlayEvent;
    public AK.Wwise.Event OrganStopEvent;

    [Header("Called on Mouse Movement")]
    public AK.Wwise.Event Melody2StartEvent;
    public AK.Wwise.Event Melody2StopEvent;

    public Vector2 melody2EmitterPos;
    private Vector3 mousePos;

    public bool hasPlayed;

    private Vector3 lastPos;

    public float speed;

    public float decay;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
        OrganPlayEvent.Post(gameObject);
        mousePos = new Vector3();
        lastPos = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = Input.mousePosition;
        Vector3 worldSpaceMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        worldSpaceMousePos.z = 0f;
        gameObject.transform.position = worldSpaceMousePos;
        
        melody2EmitterPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        AkSoundEngine.SetRTPCValue("MousePos", melody2EmitterPos.x);

        speed = Vector3.Magnitude(mousePos - lastPos);

        if (speed > 1)
        {
            if (!hasPlayed)
            {
                Melody2StartEvent.Post(gameObject);
                hasPlayed = true;
            }

        }

        if (speed < 1)
        {
            Melody2StopEvent.Post(gameObject);
            hasPlayed = false;
        }

        lastPos = mousePos;

    }
}
