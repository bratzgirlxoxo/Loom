using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{

    public Animator cabinetAnimator;
    public float waitTime;
    public bool loom3;


    void Start()
    {
        if (!loom3)
            cabinetAnimator.gameObject.SetActive(false);
        else
            cabinetAnimator.SetBool("constructing", true);
    }

    public IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(waitTime);
        cabinetAnimator.gameObject.SetActive(true);
        cabinetAnimator.SetBool("constructing", true);
        Debug.Log("Objects Constructing");
    }
}
