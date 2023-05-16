using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 1.0f;
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
        Debug.Log("Oui");
        Particle particle = collision.gameObject.GetComponent<Particle>();
        if (particle != null)
        {
            // Collision with other particle stuffs
        }
        else
        {
            Vector2 externalForce = new Vector2();
            Vector2 normal = collision.contacts[0].normal; 
            float mag = Mathf.Abs(Vector2.Dot(lastForce, normal));
            externalForce = normal.normalized * mag;
            force += externalForce;
        }
    }

    public void UpdatePosition(float deltaT)
    {
        Debug.Log(force);
        acceleration = force / mass;
        velocity += acceleration * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
    }

    private void LateUpdate()
    {
        lastForce = force;
        force = new Vector3(0.0f, 0.0f);
    }
}
