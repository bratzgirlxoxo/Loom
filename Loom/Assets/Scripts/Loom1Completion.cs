using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Loom1Completion : MonoBehaviour
{
    public bool ready;


    public TutorialGhost ghostFriend;

    public AK.Wwise.Event fireballPlay;

    private float t;
    
    void Update()
    {
        if (ready)
        {
            t += Time.deltaTime;
            if (t >= 3.25f)
            {
                fireballPlay.Post(ghostFriend.gameObject);
                //ghostFriend.GetComponent<ParticleSystem>().Play();
                ghostFriend.StartCoroutine(ghostFriend.JumpTutorial());
                ready = false;
            }
        }
    }

}
