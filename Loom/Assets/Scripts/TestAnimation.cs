using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{

    public Animator cabinetAnimator;
    public float waitTime;


    void Start()
    {
        cabinetAnimator.gameObject.SetActive(false);
    }

    public IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(waitTime);
        cabinetAnimator.gameObject.SetActive(true);
        cabinetAnimator.SetBool("constructing", true);
        Debug.Log("Objects Constructing");
    }
}
