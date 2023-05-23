using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatFluide : MonoBehaviour
{
    [SerializeField] GameObject fluideParticle;
    [SerializeField] private ParticleManager particleManager;
    public bool open = false;
    private int remains = 0;
    private bool shouldInstaciate;
    
    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            if (!shouldInstaciate)
            {
                shouldInstaciate = true;
                return;
            }
            
            GameObject instantiated = Instantiate(fluideParticle, transform.position, transform.rotation);
            particleManager.particles.Add(instantiated.GetComponent<Particle>());
            shouldInstaciate = false;
        }
    }
}
