using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class TextHover : MonoBehaviour
 {
     private Vector2 startScale;
     private Vector2 targetScale;

     private float TimeScale = 0.5f;
     private Coroutine scaleRoutine = null;

     private void Start()
     {
         //defining our start scale
         startScale = new Vector2(gameObject.transform.localScale.x, 
             gameObject.transform.localScale.y);
     }

     private void OnMouseOver()
     { 
         targetScale = new Vector2(1.5f, 1.5f);
         scaleRoutine = StartCoroutine(LerpUp());
     }
     
     private void OnMouseExit()
     {
         StopCoroutine(scaleRoutine);
     }
     
     //this coroutine scale the object up at a certain time scale
     IEnumerator LerpUp(){
         float progress = 0;
     
         while(progress <= 1){
             transform.localScale = Vector2.Lerp(startScale, targetScale, progress);
             progress += Time.deltaTime * TimeScale;
             yield return null;
         }
         transform.localScale = targetScale;
     
     } 
 }