using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBells : MonoBehaviour
{
    private ParticleSystem.Particle[] allParts;

    public float switchFrequencyMin;
    public float switchFrequencyMax;

    private float switchFrequency;

    private ParticleSystem myPartSystem;

    private float t;

    private Vector2 particlePosition;

    public Transform player;

    public float distanceThreshold;

    public AK.Wwise.Event PlayBellEvent;

    private bool playing;

    private GameObject soundObject;
    
    // Start is called before the first frame update
    void Start()
    {
        switchFrequency = Random.Range(switchFrequencyMin, switchFrequencyMax);
        myPartSystem = GetComponent<ParticleSystem>();
        allParts = new ParticleSystem.Particle[myPartSystem.main.maxParticles];
        myPartSystem.GetParticles(allParts);
        ParticleSystem.Particle chosenParticle = allParts[Random.Range(0, allParts.Length)];
        particlePosition = new Vector2(chosenParticle.position.x, chosenParticle.position.z);
        Debug.Log("New Particle Position: " + particlePosition);
        soundObject = new GameObject("BellSoundObject");
        soundObject.transform.parent = transform;
        soundObject.transform.localPosition = particlePosition;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!playing)
        {
            StartCoroutine(PlaySound());
        }

    }

    IEnumerator PlaySound()
    {
        playing = true;
        switchFrequency = Random.Range(switchFrequencyMin, switchFrequencyMax);
        myPartSystem.GetParticles(allParts);
        ParticleSystem.Particle chosenParticle = allParts[Random.Range(0, allParts.Length)];
        particlePosition = new Vector2(chosenParticle.position.x, chosenParticle.position.z);
        Debug.Log("New Particle Position: " + particlePosition);
        soundObject.transform.localPosition = particlePosition;
        PlayBellEvent.Post(soundObject);
        yield return new WaitForSeconds(switchFrequency);
        playing = false;
    }
    
    
}
