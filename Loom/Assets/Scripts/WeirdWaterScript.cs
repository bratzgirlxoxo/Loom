using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdWaterScript : MonoBehaviour
{

    private Material waterMat;

    public Texture[] waterNormals;

    // Start is called before the first frame update
    void Start()
    {
        waterMat = GetComponent<MeshRenderer>().material;
    }

    private int counter = 0;
    private int imgIdx = 0;
    
    // Update is called once per frame
    void Update()
    {
        counter++;

        if (counter % 2 == 0)
        {
            imgIdx++;
            if (imgIdx == waterNormals.Length)
                imgIdx = 0;
            waterMat.SetTexture("_BumpMap", waterNormals[imgIdx]);
        }
    }
}
