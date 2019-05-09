using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCandle : MonoBehaviour
{
    private GameObject organEmitter;
    private float distance;
    public AK.Wwise.Event CandlePlayEvent;
    public AK.Wwise.Event CandleStrikeEvent;
    public AK.Wwise.Event CandleStopEvent;
    public AK.Wwise.Event MenuStopAll;

    public Vector3 mousePos;
    private Vector3 candlePos;

    public GameObject L;
    public GameObject O;
    public GameObject O2;
    public GameObject M;

    private bool lit;
    

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

    private void OnMouseDown()
    {
        if (!lit)
        {
            CandleStrikeEvent.Post(gameObject);
            lit = true;
        }
        
        MenuStopAll.Post(L);
        MenuStopAll.Post(O);
        MenuStopAll.Post(O2);
        MenuStopAll.Post(M);
        MenuStopAll.Post(organEmitter);
        CandleStopEvent.Post(gameObject);
        organEmitter.SetActive(false);
    }

}
