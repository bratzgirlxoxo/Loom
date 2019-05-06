using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiCandle : MonoBehaviour
{

    public Transform candle;

    public float yOffset;

    public Transform mainCam;

    public float bounceDistance;
    public float bounceSpeed;
    public float rotationSpeed;

    private Vector3 startPos;
    private bool inRange;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, candle.localPosition.y + yOffset, transform.localPosition.z);
        GetComponent<MeshRenderer>().enabled = false;
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = startPos;
        newPos.y += bounceDistance * Mathf.Sin(Time.time * bounceSpeed);
        transform.localPosition = newPos;

        transform.forward = -mainCam.forward;
        //Vector3 currRot = transform.eulerAngles;
        //currRot.y = 0f;
        //transform.eulerAngles = currRot;
        
        
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            Destroy(transform.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = true;
            inRange = true;
        }
    }
}
