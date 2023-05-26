using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatFluide : MonoBehaviour
{
    [SerializeField] GameObject fluideParticle;
    [SerializeField] private ParticleManager particleManager;
    [SerializeField] FlexibleColorPicker color;
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
            instantiated.GetComponent<SpriteRenderer>().color = color.color;
            Particle newOne = instantiated.GetComponent<Particle>();
            newOne.mass = particleManager.MassSlider.value;
            newOne.referenceDensity = particleManager.DensiteSlider.value;
            newOne.dynamicViscosity = particleManager.ViscositySlider.value;
            particleManager.particles.Add(newOne);
        }
    }
}
