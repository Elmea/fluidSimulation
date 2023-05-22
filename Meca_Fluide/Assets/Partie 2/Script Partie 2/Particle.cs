using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 0.1f;
    public float mass = 0.02f;
    public float pressure;

    private Vector2 lastForce = new Vector3( 0.0f, 0.0f );

    public Vector2 acceleration = new Vector3( 0.0f, 0.0f );
    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);

    private void Awake()
    {
        velocity = new Vector3( 0.0f, 0.0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Particle particle = collision.gameObject.GetComponent<Particle>();
        if (particle != null)
        {
            // Collision with other particle stuffs
        }
        else
        {
            float sign = 1;

            if (collision.gameObject.transform.rotation.eulerAngles.z > 180)
                sign = -1;
            
            Vector2 externalForce = new Vector2();
            Vector2 normal = collision.GetContact(0).normal;

            float angle = Vector2.Angle(sign * normal, velocity);
            float magX = Mathf.Cos(Mathf.Deg2Rad * angle) * lastForce.magnitude;
            float magY = Mathf.Sin(Mathf.Deg2Rad * angle) * lastForce.magnitude;

            externalForce.x = magX / rho;
            externalForce.y = magY / rho;

            force += externalForce;
            Debug.Log(sign);
        }
    }

    public void UpdatePosition(float deltaT)
    {
        acceleration = force * mass;
        velocity += acceleration * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
        lastForce = force;
        Debug.Log(force);
        force = new Vector2(0, 0);
    }
}
