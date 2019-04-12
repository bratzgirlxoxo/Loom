using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Loom1Completion : MonoBehaviour
{
    public bool ready;

    public FirstPersonController playerController;

    public Transform[] cutPositions;
    public float[] cutWaitTimes;

    public TutorialGhost ghostFriend;
    
    public GameObject playerCam;
    private Camera thisCam;

    void Start()
    {
        thisCam = GetComponent<Camera>();
        thisCam.enabled = false;
    }
    
    void Update()
    {
        if (ready)
        {
            StartCoroutine(CameraCutscene());
            ready = false;
        }
    }

    IEnumerator CameraCutscene()
    {
        yield return new WaitForSeconds(3.75f);
        thisCam.enabled = true;
        playerController.canWalk = false;
        playerCam.SetActive(false);


        for (int i = 0; i < cutPositions.Length; i++)
        {
            CameraCut(cutPositions[i]);
            if (i == cutPositions.Length - 1)
            {
                ghostFriend.GetComponent<ParticleSystem>().Play();
                ghostFriend.StartCoroutine(ghostFriend.JumpTutorial());
            }
            yield return new WaitForSeconds(cutWaitTimes[i]);
        }
        

        playerCam.SetActive(true);
        playerController.canWalk = true;
    }

    // for directly cutting from one camera position to another
    void CameraCut(Transform newPosition)
    {
        thisCam.transform.position = newPosition.position;
        thisCam.transform.forward = newPosition.forward;
    }

}
