using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3Door : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 startScale;

    public float openTime;
    
    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    public IEnumerator OpenDoor()
    {
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 1000f, startPos.z);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / openTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            transform.localScale = Vector3.Lerp(startScale, startScale * 0.95f, t);
            yield return null;
        }
    }
}
