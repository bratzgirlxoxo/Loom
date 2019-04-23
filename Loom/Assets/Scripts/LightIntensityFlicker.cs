using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityFlicker : MonoBehaviour
{
    public float timeScale;

    public bool isLit;
    public Color offColor;
    public Color onColor;

    private Light myLight;
    private float t = 0f;

    public GameObject flame;
    
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        myLight.color = offColor;
        flame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (isLit)
        {
            t += Time.deltaTime * timeScale;

            myLight.intensity = Mathf.PerlinNoise(t, transform.position.y);
        } 
        
    }
}
