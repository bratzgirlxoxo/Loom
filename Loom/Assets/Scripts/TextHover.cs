﻿using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.Serialization;

public class TextHover : MonoBehaviour
 {
     private Vector3 startScale;
     private Vector3 targetScale;

     private Coroutine scaleRoutine = null;

     public AK.Wwise.Event Melody1Start;
     public AK.Wwise.Event Melody1Stop;
     

     public float InflateTime;
     public float DeflateTime;

     public float jitterScale;

     public AnimationCurve textTweenCurve;

     private void Start()
     {
         //defining our start scale
         startScale = new Vector3(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y,1);
         StartCoroutine(Jitter());
     }

     private void OnMouseEnter()
     { 
         StopAllCoroutines();
         Vector3 currentScale = new Vector3(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y, 1);
         targetScale = new Vector3(1.5f, 1.5f, 1);
         StopAllCoroutines();
         StartCoroutine(TextScaleLerp(currentScale, targetScale, InflateTime));

         Melody1Start.Post(gameObject);
     }
     
     private void OnMouseExit()
     {
         StopAllCoroutines();
         Vector2 currentScale = new Vector3(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y, 1);
         StartCoroutine(TextScaleLerp(currentScale, startScale, DeflateTime));

         StartCoroutine(WaitBeforeStop());

     }
     
     //this coroutine scale the object up at a certain time scale
     IEnumerator TextScaleLerp(Vector3 start, Vector3 goal, float TimeScale){
         
         float progress = 0;
         while(progress <= 1){
             transform.localScale = Vector3.Lerp(start, goal, textTweenCurve.Evaluate(progress));
             progress += Time.deltaTime / TimeScale;
             yield return null;
         }

         if (start.x > goal.x)
         {
             StartCoroutine(Jitter());
         }
     }

     IEnumerator WaitBeforeStop()
     {
         yield return new WaitForSeconds(0.5f);
         Melody1Stop.Post(gameObject);
     }

     IEnumerator Jitter()
     {
         Vector3 startPos = transform.position;
         while (true)
         {
             Vector3 pos = transform.position;
             pos.x = startPos.x + (Mathf.PerlinNoise(Time.time, startPos.x) * jitterScale - jitterScale/2);
             pos.y = startPos.y + (Mathf.PerlinNoise(Time.time * 0.85f, startPos.y) * jitterScale - jitterScale/2);

             yield return null;
         }
     }
 }