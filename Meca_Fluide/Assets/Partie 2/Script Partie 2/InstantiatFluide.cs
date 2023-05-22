using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatFluide : MonoBehaviour
{
    [SerializeField] GameObject fluideParticle;
    [SerializeField] private ParticleManager particleManager;
    public bool open = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            GameObject instantiated = Instantiate(fluideParticle, transform.position, transform.rotation);
            particleManager.particles.Add(instantiated.GetComponent<Particle>()); 
            open = false;
        }

    }
}
