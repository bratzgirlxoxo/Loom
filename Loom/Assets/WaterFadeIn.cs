using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFadeIn : MonoBehaviour
{

    public float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        fadeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime < 5)
        {
            fadeTime += Time.deltaTime;
            AkSoundEngine.SetRTPCValue("FadeTime", fadeTime);
        }
        
    }
}
