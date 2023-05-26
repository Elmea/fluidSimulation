using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatFluide : MonoBehaviour
{
    [SerializeField] GameObject fluideParticle;
    [SerializeField] private ParticleManager particleManager;
    public bool open = false;
    
    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            if (particleManager.particles.Count > 260)
            {
                open = false;
                return;
            }

            GameObject instantiated = Instantiate(fluideParticle, transform.position, transform.rotation);
            particleManager.particles.Add(instantiated.GetComponent<Particle>());
        }
    }
}
