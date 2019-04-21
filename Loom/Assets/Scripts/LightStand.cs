using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStand : MonoBehaviour
{

    public bool isLit;
    public bool canLight;
    

    // specify which loom it is
    public bool loom1;
    public bool loom2;
    public bool loom3;

    public GameObject myLight;
    public LoomTrigger nextLoomTrigger;
    public LoomTrigger2 nextLoomTrigger2;
    public TestAnimation objAnim;

    public int idx;

    public AK.Wwise.Event Candle_Strike_Event;

    private Vector3 startPos;
    private Vector3 endPos;

    public Vector3 endPosPub;
    
    public bool willMove;

    public float oscillateScale;
    public float oscillateSpeed;

    public GameObject door;


    void Start()
    {
        startPos = transform.localPosition;
        if (willMove)
        {
            endPos = endPosPub;
        }
        else
        {
            endPos = startPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLit && canLight && Input.GetKeyDown(KeyCode.E))
        {
            if (loom1)
            {
                myLight.SetActive(true);
                Candle_Strike_Event.Post(gameObject);
                isLit = true;
                nextLoomTrigger.lights[idx] = true;
                StartCoroutine(objAnim.startAnimation());
            } else if (loom2)
            {
                myLight.SetActive(true);
                Candle_Strike_Event.Post(gameObject);
                isLit = true;
                nextLoomTrigger2.lights[idx] = true;
                StartCoroutine(objAnim.startAnimation());
            } else if (loom3)
            {
                myLight.SetActive(true);
                StartCoroutine(door.GetComponent<Loom3Door>().OpenDoor());
            }
            
            StartCoroutine(SettleDown(transform.localPosition));
            
        }

        if (!isLit)
        {
            transform.localPosition = startPos + new Vector3(0f, oscillateScale * Mathf.Sin(Time.time * oscillateSpeed), 0f);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            canLight = true;
            
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            canLight = false;
        }
    }

    IEnumerator SettleDown(Vector3 startingPos)
    {
        yield return new WaitForSeconds(1.5f);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / 3f;
            transform.localPosition = Vector3.Lerp(startingPos, endPos, t);
            yield return null;
        }
    }
}
