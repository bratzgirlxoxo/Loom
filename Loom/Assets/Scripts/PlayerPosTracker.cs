using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosTracker : MonoBehaviour
{

    public Material waterMat;
    public Transform player;
    
    
    void Update()
    {
        Vector3 pPos = player.position;
        waterMat.SetVector("_PlayerPos", new Vector4(pPos.x, pPos.y, pPos.z, 1));
    }
}
