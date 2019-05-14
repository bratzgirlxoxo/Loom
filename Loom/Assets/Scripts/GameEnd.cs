using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public Material endMat;
    
    public Transform frame;
    private MeshRenderer frameRend;
    
    public float fadeTime;
    public AnimationCurve fadeCurve;

    private bool hit;

    void Awake()
    {
        frameRend = frame.GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!hit && coll.CompareTag("Player"))
        {
            hit = true;
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        float t = 0;
        
        frame.localScale = Vector3.one * 10f;
        frameRend.material = endMat;

        float tempA;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeTime;
            tempA = Mathf.Lerp(0f, 1f, fadeCurve.Evaluate(t));
            endMat.SetFloat("_Opacity", tempA);
            yield return null;
        }
        
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
