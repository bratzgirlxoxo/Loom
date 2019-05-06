
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loom3Entrance : MonoBehaviour
{

    public Transform imageObj;
    
    public Material uiImage;
    public Texture[] artworks;

    private int lastArtWork = -1;


    public float maxOpacityVal;
    public float fadeTime;
    public float imgFreq;

    private bool movingFwd;

    private bool fading;

    private bool inEntrance;

    void Start()
    {
        uiImage.SetFloat("_Opacity", 0f);
    }
    
    
    void Update()
    {
        if (inEntrance)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
            
                StopAllCoroutines();


                float currentA = uiImage.GetFloat("_Opacity");
                StartCoroutine(Fade(currentA, maxOpacityVal));
                movingFwd = true;

                StartCoroutine(ImageScroll());
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
            
                StopAllCoroutines();
            
                float currentA = uiImage.GetFloat("_Opacity");
                StartCoroutine(Fade(currentA, 0f));
                movingFwd = false;
            }
        }
        
    }

    IEnumerator Fade(float start, float end)
    {
        fading = true;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            float newOpacity = Mathf.Lerp(start, end, t);
            uiImage.SetFloat("_Opacity", newOpacity);
            yield return null;
        }

        fading = false;

        if (start < end)
        {
            while (true)
            {
                float nextOpacity = end + (Mathf.PerlinNoise(Time.time, imageObj.position.x) * 0.5f - 0.25f);
                uiImage.SetFloat("_Opacity", nextOpacity);
                yield return null;
            }
        }
    }

    IEnumerator ImageScroll()
    {
        int nextIdx = -2;
        while (true)
        {
            
            do
            {
                nextIdx = Random.Range(0, artworks.Length);
            } while (nextIdx == lastArtWork);
            lastArtWork = nextIdx;

            uiImage.SetTexture("_MainTex", artworks[nextIdx]);

            float newX = Random.Range(-1f, 1f);
            float newY = Random.Range(-0.3f, 0.3f);
            imageObj.localPosition = new Vector3(newX, newY, 1f);
            
            yield return new WaitForSeconds(imgFreq);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            inEntrance = true;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                StopAllCoroutines();


                float currentA = uiImage.GetFloat("_Opacity");
                StartCoroutine(Fade(currentA, maxOpacityVal));
                movingFwd = true;

                StartCoroutine(ImageScroll());
            }
        }
    }
    
    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            inEntrance = false;
            StopAllCoroutines();
            float currentA = uiImage.GetFloat("_Opacity");
            StartCoroutine(Fade(currentA, 0));
        }
    }
}
