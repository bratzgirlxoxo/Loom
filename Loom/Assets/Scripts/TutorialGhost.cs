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
        
        if (stageIdx == 1 && Vector3.Distance(transform.position, player.position) < 10f)
        {
            rBody.useGravity = true;
            coll.enabled = true;
            rBody.AddForce((transform.up - transform.forward)*jumpStrength, ForceMode.Impulse);
            
            stageIdx++;
        }

        if (stageIdx == 2 && Vector3.Distance(transform.position, player.position) < 2.5f)
        {
            transform.position = stage2Pos;
            stageIdx++;
            rBody.useGravity = false;
            coll.enabled = false;
        }

        if (stageIdx == 3 && Vector3.Distance(transform.position, player.position) < 3f)
        {
            stageIdx++;
            startPos = transform.position;
            StartCoroutine(JumpTutorial3());
        }

        if (stageIdx == 5 && Vector3.Distance(transform.position, player.position) < 6f)
        {
            StartCoroutine(JumpTutorial4());
        }
    }

    private float t;
    private int stageIdx = 0;

    public IEnumerator JumpTutorial()
    {
        myLight.enabled = true;
        burstParticles.Play();

        lantern.enabled = true;
        myLight.transform.GetComponent<LightIntensityFlicker>().flame.SetActive(true);

        
        ParticleSystem.EmissionModule emiss = burstParticles.emission;
        emiss.enabled = true;
        while (t <= 1f)
        {
            t += Time.deltaTime / runTime;
            Vector3 nextLerpPos = Vector3.Lerp(startPos, jumpPos, runAnimationCurve.Evaluate(t));

            transform.position = nextLerpPos;
            yield return null;
        }

        stageIdx++;
        
        emiss.enabled = false;

        moving = false;
    }
    
    IEnumerator JumpTutorial3()
    {
        moving = true;
        
        t = 0;
        while (t <= 1f)
        {
            t += Time.deltaTime / runTime;
            Vector3 nextLerpPos = Vector3.Lerp(startPos, stage3Pos, runAnimationCurve.Evaluate(t));
            
            transform.position = nextLerpPos;
            yield return null;
        }

        yield return new WaitForSeconds(4f);
        
        stageIdx++;

        moving = false;
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
    
}
