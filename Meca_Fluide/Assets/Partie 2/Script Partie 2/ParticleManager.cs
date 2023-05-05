using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<Particle> particles;

    public static float kernelRadius = 5.0f;
    public static float dynamicViscosity = 0.1f;

    private static float poly6;
    private static float spikyGradConst;
    private static float viscLaplacienConst;

    public float stiffness = 3.0f;
    public float referenceDensity = 1000.0f;

    private void Start()
    {
        RecalcConstants();
    }

    // This function should be call when the kernel radius is modified.
    public void RecalcConstants()
    {
        poly6 = 315.0f/(64.0f * Mathf.PI * Mathf.Pow(kernelRadius, 9));
        spikyGradConst = -45.0f / (Mathf.PI * Mathf.Pow(kernelRadius, 5.0f));
        viscLaplacienConst = 45/(Mathf.PI * Mathf.Pow(kernelRadius, 6));
    }
    
    private void CalcDensityAndPressure(Particle me)
    {
        me.rho = 0.0f;

        float sigmaW = 0.0f;
        foreach (Particle other in particles)
        {
            if (me == other)
                continue;
            Vector2 distVec = other.transform.position - me.transform.position;
            float lenght = distVec.magnitude;

            if (lenght < kernelRadius)
            {
                sigmaW += poly6 * Mathf.Pow(kernelRadius * kernelRadius - lenght * lenght, 3);
            }
        }

        me.rho = sigmaW * me.mass;
        me.pressure = stiffness * (me.rho - referenceDensity);
    }

    private void CalcForces(Particle me)
    {   
        Vector2 sigmaPress = new Vector3( 0.0f, 0.0f );
        Vector2 sigmaVisc = new Vector3( 0.0f, 0.0f );
        foreach (Particle other in particles)
        {
            if (me == other)
                continue;
            Vector2 distVec = other.transform.position - me.transform.position;
            float lenght = distVec.magnitude;

            if (lenght < kernelRadius && lenght > 0)
            {
                sigmaPress += (me.pressure + other.pressure) / (2 * other.rho) * 
                             spikyGradConst * distVec.normalized
                             * MathF.Pow(kernelRadius - lenght, 2);

                sigmaVisc +=  (other.velocity - me.velocity) / other.rho 
                            * viscLaplacienConst * (kernelRadius - lenght);
            }
        }

        me.force += -me.mass * sigmaPress;
        me.force += dynamicViscosity * me.mass * sigmaVisc;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (particles.Count > 1)
        {
            foreach (Particle p in particles)
            {
                CalcDensityAndPressure(p);
                CalcForces(p);
            }
        }
        
        foreach (Particle p in particles)
        {
            p.UpdatePosition();
        }
    }
}
