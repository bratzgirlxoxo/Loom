using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 endPos;
    public float riseTime;

    private bool rising;
    private bool up;

    public TestAnimation topBookshelf;
    public Loom3Lantern endLantern;

    private float t;
    private Rigidbody rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rising = false;
    }

    void FixedUpdate()
    {

        if (rising  && !up && t < 1f)
        {
            t += Time.fixedDeltaTime / riseTime;
            rbody.MovePosition(new Vector3(startPos.x, startPos.y + (endPos.y-startPos.y)*t, startPos.z));
        } 
        else if (t >= 1f)
        {
            rbody.velocity = Vector3.zero;
            rising = false;
            rbody.useGravity = false;
            rbody.isKinematic = false;
            endLantern.endRoom = true;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!rising && coll.CompareTag("Player"))
        {
            startPos = transform.position;
            Debug.Log("Rising!");
            rising = true;
            topBookshelf.startAnimation();
        }
    }
}
