using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3Lantern : MonoBehaviour
{

    public Light lanternLight;
    public GameObject flame;

    [HideInInspector] public bool endRoom;
    private bool final;

    private Animator walkingAnim;

    public Transform endPosTrans;
    private Vector3 endPos;

    public AnimationCurve floatCurve;
    public float floatTime;
    
    void Start()
    {
        walkingAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (endRoom && !final)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            final = true;
            StartCoroutine(endLerp());
        }
    }

    public void Birth()
    {
        lanternLight.enabled = true;
        lanternLight.gameObject.GetComponent<LightIntensityFlicker>().isLit = true;
        flame.SetActive(true);
    }

    IEnumerator endLerp()
    {
        endPos = endPosTrans.position;
        Vector3 startPos = transform.position;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.5f);
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / floatTime;
            transform.position = Vector3.Lerp(startPos, endPos, floatCurve.Evaluate(t));
            yield return null;
        }
        yield return new WaitForSeconds(1.25f);
        walkingAnim.SetBool("stop", true);
    }
}
