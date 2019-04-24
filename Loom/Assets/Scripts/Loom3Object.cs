using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom3Object : MonoBehaviour
{

    public TestAnimation myAnim;
    
    void Start()
    {
        //myAnim = GetComponent<TestAnimation>();
        myAnim.StartCoroutine(myAnim.startAnimation());
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            myAnim.StartCoroutine(myAnim.startAnimation());
        }
    }
    

    
}
