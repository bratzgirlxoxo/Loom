using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public AK.Wwise.Event OrganPlayEvent;
    public AK.Wwise.Event DrumsPlayEvent;
    public AK.Wwise.Event BassPlayEvent;

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
        DrumsPlayEvent.Post(gameObject);
        BassPlayEvent.Post(gameObject);
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

        AkSoundEngine.SetRTPCValue("MousePosX", melody2EmitterPos.x);
        AkSoundEngine.SetRTPCValue("MousePosY", melody2EmitterPos.y);

        speed = Vector3.Magnitude(mousePos - lastPos);
        AkSoundEngine.SetRTPCValue("MouseSpeed", speed);

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
            StartCoroutine(WaitBeforeStop());
        }

        lastPos = mousePos;

    }

    IEnumerator WaitBeforeStop()
    {
        yield return new WaitForSeconds(decay);
        Melody2StopEvent.Post(gameObject);
        hasPlayed = false;
    }
}
