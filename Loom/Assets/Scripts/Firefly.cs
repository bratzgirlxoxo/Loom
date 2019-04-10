using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Firefly : MonoBehaviour
{

    public Vector3 axisSpeeds;
    public Vector3 axisScales;
    public float growUpSpeed;
    public float flickerSpeed;

    private Material fireflyMat;

    private float t;
    private bool grownUp;

    public float mainScale;

    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        fireflyMat = GetComponent<MeshRenderer>().material;
        startPos = transform.position;
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (grownUp)
        {
            Fly(axisSpeeds, axisScales);
            Flicker();
        }
    }

    IEnumerator GrowUp()
    {
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / growUpSpeed;
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(mainScale, mainScale, mainScale), t);
            yield return null;
        }

        startPos = transform.position;
        
        grownUp = true;
        t = 0;
    }

    void Fly(Vector3 speeds, Vector3 scales)
    {
        float xSpeed = speeds.x, ySpeed = speeds.y, zSpeed = speeds.z;
        float xScale = scales.x, yScale = scales.y, zScale = speeds.z;
        Vector3 newPos = startPos;
        newPos.x = startPos.x + xScale * Mathf.PerlinNoise(Time.time * xSpeed, startPos.x) - (xScale/2f);
        newPos.y = startPos.y + yScale * Mathf.PerlinNoise(Time.time * ySpeed, startPos.y) - (yScale/2f);
        newPos.z = startPos.z + zScale * Mathf.PerlinNoise(Time.time * zSpeed, startPos.z) - (zScale/2f);
        transform.position = newPos;

    }

    void Flicker()
    {
        float visibility = 5 + 20 * Mathf.PerlinNoise(Time.time * flickerSpeed, transform.position.y);
        fireflyMat.SetFloat("_Intensity", visibility);
    }
}
