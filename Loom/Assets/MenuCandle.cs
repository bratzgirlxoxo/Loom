using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCandle : MonoBehaviour
{
    public GameObject organEmitter;
    public float distance;
    public AK.Wwise.Event CandlePlayEvent;

    public Vector3 mousePos;
    private Vector3 candlePos;

    private void Start()
    {
        organEmitter = GameObject.Find("OrganEmitter");
        CandlePlayEvent.Post(gameObject);
    }

    private void Update()
    {
        mousePos = organEmitter.transform.position;
        candlePos = transform.position;

        distance = Vector3.Distance(mousePos, candlePos);

        AkSoundEngine.SetRTPCValue("MouseDistance", distance);
    }
}
