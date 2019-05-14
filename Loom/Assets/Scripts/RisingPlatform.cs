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
    public TestAnimation topHanger;
    public Loom3Lantern endLantern;
    public ReflectionProbe refProbe;
    public LightIntensityFlicker fireLightFlicker;
    public Light fireLight;
    public GameObject[] riseColliders;

    private float t;
    private Rigidbody rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rising = false;
        refProbe.enabled = false;
        
        for (int i = 0; i < riseColliders.Length; i++)
        {
            riseColliders[i].SetActive(false);
        }
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
            rbody.useGravity = false;
            endLantern.endRoom = true;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!rising && coll.CompareTag("Player"))
        { 
            for (int i = 0; i < riseColliders.Length; i++)
            {
                riseColliders[i].SetActive(true);
            }
            
            startPos = transform.position;
            Debug.Log("Rising!");
            rising = true;
            topBookshelf.startAnimation();
            topHanger.startAnimation();
            refProbe.enabled = true;
            fireLightFlicker.isLit = true;
            fireLight.enabled = true;
        }
    }
}
