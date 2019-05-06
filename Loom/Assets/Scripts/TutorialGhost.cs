using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhost : MonoBehaviour
{

    public Transform player;
    public Light myLight;
    
    public float jumpStrength;
    public float runTime = 3;
    public AnimationCurve runAnimationCurve;

    public Transform startPosTrans;
    public Transform divingBoardTrans;
    public Transform stage2PosTrans;
    public Transform stage3PosTrans;

    private Vector3 startPos;
    private Vector3 jumpPos;
    private Vector3 stage2Pos;
    private Vector3 stage3Pos;
    private Rigidbody rBody;

    public ParticleSystem burstParticles;
    public MeshRenderer lantern;

    private Collider coll;

    public AK.Wwise.Event Loom1LanternJumpEvent;
    
    private bool moving;

    
    // Start is called before the first frame update
    void Start()
    {
        startPos = startPosTrans.position;
        jumpPos = divingBoardTrans.position;
        stage2Pos = stage2PosTrans.position;
        stage3Pos = stage3PosTrans.position;
        
        rBody = GetComponent<Rigidbody>();
        rBody.useGravity = false;
        myLight.enabled = false;

        lantern.enabled = false;

        coll = GetComponent<Collider>();
        //coll.enabled = false;
    }

    void Update()
    {

        if (!moving)
        {
            if (stageIdx == 1 && Vector3.Distance(transform.position, player.position) < 6f)
            {
                rBody.useGravity = true;
                coll.enabled = true;
                StartCoroutine(JumpTutorial1());
                Debug.Log("jumping");
            }

            if (stageIdx == 2 && Vector3.Distance(transform.position, player.position) < 25f)
            {
                rBody.useGravity = false;
                coll.enabled = false;
                startPos = transform.position;
                StartCoroutine(JumpTutorial2());
            }

            if (stageIdx == 3 && Vector3.Distance(transform.position, player.position) < 3f)
            {
                startPos = transform.position;
                StartCoroutine(JumpTutorial3());
            }

            if (stageIdx == 5 && Vector3.Distance(transform.position, player.position) < 6f)
            {
                StartCoroutine(JumpTutorial4());
            }
        }
        
    }

    private float t;
    private int stageIdx = 0;

    public IEnumerator JumpTutorial()
    {
        moving = true;
        myLight.enabled = true;
        burstParticles.Play();

        lantern.enabled = true;
        myLight.transform.GetComponent<LightIntensityFlicker>().flame.SetActive(true);

        
        ParticleSystem.EmissionModule emiss = burstParticles.emission;
        emiss.enabled = true;
        StartCoroutine(LinearLerp(startPos, jumpPos, runTime));

        stageIdx++;
        
        emiss.enabled = false;

        yield return null;
    }

    IEnumerator JumpTutorial1()
    {
        moving = true;
        rBody.AddForce((transform.up - transform.forward)*jumpStrength, ForceMode.Impulse);
        Loom1LanternJumpEvent.Post(transform.gameObject);
        yield return new WaitForSeconds(4f);
        stageIdx++;
        moving = false;
    }

    IEnumerator JumpTutorial2()
    {
        moving = true;
        StartCoroutine(LinearLerp(startPos, stage2Pos, runTime));
        stageIdx++;
        yield return null;
    }
    
    IEnumerator JumpTutorial3()
    {
        moving = true;
        
        t = 0;
        StartCoroutine(LinearLerp(startPos, stage3Pos, runTime));

        yield return new WaitForSeconds(1f);
        
        stageIdx++;
    }
    
    IEnumerator JumpTutorial4()
    {
        t = 0;
        Vector3 startScale = transform.localScale;
        while (t <= 1f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }
        myLight.enabled = false;
        Destroy(transform.gameObject);
    }
    
    IEnumerator LinearLerp(Vector3 start, Vector3 finish, float moveTime)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / moveTime;
            Vector3 nextLerpPos = Vector3.Lerp(start, finish, runAnimationCurve.Evaluate(t));

            transform.position = nextLerpPos;
            yield return null;
        }
        moving = false;
    }
    
}
