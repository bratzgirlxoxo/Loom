﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3EndCollider : MonoBehaviour
{

    public Material lanternBase;
    public Material lanternGlass;
    public Material[] replacementMats;
    public float fadeTime;
    public Loom3Lantern endLantern;

    private bool hasHit;

    public MeshRenderer lanternRend;
    public AK.Wwise.Event lanternSound;

    void Start()
    {   
        lanternBase.SetFloat("_Opacity", 0f);
        lanternGlass.SetFloat("_Opacity", 0f);
    }
    
    
    void OnTriggerEnter(Collider coll)
    {
        if (!hasHit && coll.CompareTag("Player"))
        {
            StartCoroutine(LanternFadeIn());
            lanternSound.Post(lanternRend.gameObject);
        }
    }
    
    IEnumerator LanternFadeIn()
    {
        hasHit = true;
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime / fadeTime;
            float lerpVal = Mathf.Lerp(0f, 1f, timer);
            lanternBase.SetFloat("_Opacity", lerpVal);
            lanternGlass.SetFloat("_Opacity", lerpVal/2f);
            yield return null;
        }

        lanternRend.materials = replacementMats;
        
        endLantern.Birth();
    }
}
