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
    public GameObject fireBall;

    private ParticleSystem fireballParticles;

    public float distanceThreshold;

    public AK.Wwise.Event PlayBellEvent;

    private bool playing;

    private GameObject soundObject;

    public bool end;
    
    
    
    void Start()
    {
        switchFrequency = Random.Range(switchFrequencyMin, switchFrequencyMax);
        myPartSystem = GetComponent<ParticleSystem>();
        allParts = new ParticleSystem.Particle[myPartSystem.main.maxParticles];
        myPartSystem.GetParticles(allParts);
        ParticleSystem.Particle chosenParticle = allParts[Random.Range(0, allParts.Length)];
        
        particlePosition = chosenParticle.position;
        soundObject = new GameObject("BellSoundObject");
        soundObject.transform.parent = transform;
        soundObject.transform.localPosition = particlePosition;

        fireBall.transform.localPosition = particlePosition;
        fireballParticles = fireBall.GetComponent<ParticleSystem>();
        fireBall.transform.localScale *= 3.5f;
    }

    void Update()
    {
        if (!playing && !end)
        {
            StartCoroutine(PlaySound());
        }

    }

    IEnumerator PlaySound()
    {   
        playing = true;
        switchFrequency = Random.Range(switchFrequencyMin, switchFrequencyMax);
        
        fireballParticles.Play();        
        myPartSystem.GetParticles(allParts);
        ParticleSystem.Particle chosenParticle = allParts[Random.Range(0, allParts.Length)];
        
        Color32 startCol = chosenParticle.GetCurrentColor(myPartSystem);

        particlePosition = chosenParticle.position;
        Debug.Log("New Particle Position: " + particlePosition);
        soundObject.transform.localPosition = particlePosition;
        fireBall.transform.localPosition = particlePosition;
        PlayBellEvent.Post(soundObject);
        yield return new WaitForSeconds(switchFrequency);

        
        fireballParticles.Stop();
        playing = false;
    }
}
