using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class TextHover : MonoBehaviour
 {
     private Vector2 startScale;
     private Vector2 targetScale;

     private Coroutine scaleRoutine = null;

     public AK.Wwise.Event Melody1Start;
     public AK.Wwise.Event Melody1Stop;
     

     public float InflateScale;
     public float DeflateScale;

     private void Start()
     {
         //defining our start scale
         startScale = new Vector2(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y);
     }

     private void OnMouseEnter()
     { 
         StopAllCoroutines();
         Vector2 currentScale = new Vector2(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y);
         targetScale = new Vector2(1.5f, 1.5f);
         StartCoroutine(TextScaleLerp(currentScale, targetScale, InflateScale));

         Melody1Start.Post(gameObject);
     }
     
     private void OnMouseExit()
     {
         StopAllCoroutines();
         Vector2 currentScale = new Vector2(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y);
         StartCoroutine(TextScaleLerp(currentScale, startScale, DeflateScale));
         
         Melody1Stop.Post(gameObject);

     }
     
     //this coroutine scale the object up at a certain time scale
     IEnumerator TextScaleLerp(Vector2 start, Vector2 goal, float TimeScale){
         
         float progress = 0;
         while(progress <= 1){
             transform.localScale = Vector2.Lerp(start, goal, progress);
             progress += Time.deltaTime * TimeScale;
             yield return null;
         }     
     } 
 }