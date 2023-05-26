using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] public Slider MassSlider;
    [SerializeField] public Slider DensiteSlider;
    [SerializeField] public Slider ViscositySlider;
    [SerializeField] public Slider RigiditySlider;
    [SerializeField] TextMeshProUGUI MassText;
    [SerializeField] TextMeshProUGUI DensiteText;
    [SerializeField] TextMeshProUGUI ViscosityText;
    [SerializeField] TextMeshProUGUI RigidityText;
    [SerializeField] TextMeshProUGUI NbParticles;

    

    public List<Particle> particles;

    public static float kernelRadius = 1.5f;
    
    private static float poly6;
    private static float spikyGradConst;
    private static float viscLaplacienConst;
    
    private Vector2 g = new Vector2(0.0f, -9.81f); 

    private void Start()
    {
        MassSlider.value = 0.1f;
        DensiteSlider.value = 35.0f;
        ViscositySlider.value = 500.0f;
        RigiditySlider.value = 10000.0f;

        RecalcConstants();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
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
            
            float lenght = (other.transform.position - me.transform.position).magnitude;

            if (lenght < kernelRadius)
            {
                sigmaW += poly6 * Mathf.Pow(kernelRadius * kernelRadius - lenght * lenght, 6);
            }
        }

        me.rho = me.referenceDensity + sigmaW * me.mass;
        me.pressure = me.stiffness * (me.rho - me.referenceDensity);
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

            if (distVec.magnitude < kernelRadius && distVec.magnitude > 0.02f)
            {
                sigmaPress += distVec.normalized * ((me.pressure + other.pressure) / (2 * other.rho) * 
                                                    spikyGradConst * MathF.Pow(kernelRadius - distVec.magnitude, 2));

                sigmaVisc +=  (other.velocity - me.velocity) / other.rho * (viscLaplacienConst * (kernelRadius - distVec.magnitude));
            }
        }

        me.force += me.mass * sigmaPress;
        me.force += me.dynamicViscosity * me.mass * sigmaVisc;
        me.force += me.rho * g;
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach (Particle p in particles)
            CalcDensityAndPressure(p);

        foreach (Particle p in particles)
            CalcForces(p);
        
        foreach (Particle p in particles)
        {
            p.UpdatePosition(Time.deltaTime);
        }

        MassText.text = "Mass : " + MassSlider.value;
        DensiteText.text = "Densite : " + DensiteSlider.value;
        ViscosityText.text = "Viscosity : " + ViscositySlider.value;
        RigidityText.text = "Rigidity : " + RigiditySlider.value;
        NbParticles.text = "NbParticles : " + particles.Count;

    }
}
