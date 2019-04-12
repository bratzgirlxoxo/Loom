using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{

    public Transform player;

    private float t;
    
    // Particle Stuff
    private ParticleSystem myPartSystem;
    private ParticleSystem.Particle[] myParts;
    //private int[] specialIndices;

    public float flickerSpeed;


    void Start()
    {
        myPartSystem = GetComponent<ParticleSystem>();
        myParts = new ParticleSystem.Particle[myPartSystem.main.maxParticles];
        //specialIndices = new int[myParts.Length / 10];
    }
    
    void Update()
    {
        transform.position = new Vector3(player.position.x, 0f, player.position.z);




        if (t >= 0f)
            t += Time.deltaTime;
        else
        {
            myPartSystem.GetParticles(myParts);
            for (int i = 0; i < myParts.Length; i += 10)
            {
                myParts[i].startSize = Mathf.PerlinNoise(myParts[i].position.x, Time.time * flickerSpeed)*2.5f;
            }
            myPartSystem.SetParticles(myParts);
        }
        
        // this is only gonna happen once
        if (t >= 7.5f)
        {
            t = -1;
            myPartSystem.GetParticles(myParts);
        }
        
        
        
        
        
    }
}
