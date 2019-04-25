using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostFireballSound : MonoBehaviour
{


    public AK.Wwise.Event fireBallSound;

    void Awake()
    {
        fireBallSound.Post(gameObject);
    }
}
