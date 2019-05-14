using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MenuCandle : MonoBehaviour
{
    private Scene scene;
    
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
    public GameObject flame;

    public bool lit;
    
    public Animator anim;
    public Image black;

    private void Awake()
    {        
        DontDestroyOnLoad(gameObject);
        scene = SceneManager.GetActiveScene();
    }


    private void Start()
    {
        organEmitter = GameObject.Find("OrganEmitter");
        if (scene.buildIndex == 0)
        {
            CandlePlayEvent.Post(gameObject);
        }
    }

    private void Update()
    {
        if (scene.buildIndex == 0)
        {
            mousePos = organEmitter.transform.position;
            candlePos = transform.position;

            distance = Vector3.Distance(mousePos, candlePos);
            AkSoundEngine.SetRTPCValue("MouseDistance", distance);
        }
    }

    private void OnMouseDown()
    {
        if (!lit)
        {
            CandleStrikeEvent.Post(gameObject);
            lit = true;
            flame.SetActive(true);

            StartCoroutine(SceneSwap("Playtest"));
        }

        if (scene.buildIndex == 0)
        {
            MenuStopAll.Post(L);
            MenuStopAll.Post(O);
            MenuStopAll.Post(O2);
            MenuStopAll.Post(M);
            MenuStopAll.Post(organEmitter);
            CandleStopEvent.Post(gameObject);
            organEmitter.SetActive(false);
        }
        
    }
    
    IEnumerator SceneSwap(string name)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a==1);
        SceneManager.LoadScene(name);
    }
}
    