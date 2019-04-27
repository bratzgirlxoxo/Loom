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

    public AnimationCurve birthCurve;
    
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        if (transform.parent.GetComponent<LightStand>() != null)
        {
            if (transform.parent.GetComponent<LightStand>().loom3)
                myLight.color = onColor;
            else
            {
                flame.SetActive(false);
                myLight.color = offColor;
            }
        }
        else
        {
            myLight.color = onColor;
            flame.SetActive(false);
        }
       

        birthCurve.AddKey(0.2f, 4f);
        if (birthCurve.length > 1) 
            birthCurve.keys[1].value = 1;

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

    public IEnumerator DieDown()
    {
        if (myLight == null)
            myLight = GetComponent<Light>();
        myLight.color = onColor;
        flame.SetActive(true);
        float t = 0;
        float startIntensity = myLight.intensity;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            myLight.intensity = Mathf.LerpUnclamped(1f, birthCurve.Evaluate(t), t);
            yield return null;
        }
        isLit = true;
    }
}
