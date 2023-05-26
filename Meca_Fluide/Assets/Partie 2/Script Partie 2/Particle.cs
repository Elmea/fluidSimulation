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
    private Vector2 colisionForce = new Vector3( 0.0f, 0.0f );
    private Vector2 lastVelocity = new Vector3( 0.0f, 0.0f);

    public Vector2 acceleration = new Vector3( 0.0f, 0.0f );
    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);

    private void Awake()
    {
        velocity = new Vector3( 0.0f, 0.0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
         if (collision.gameObject.layer == 3)
             return;
        
        Vector2 externalForce = new Vector2();
        Vector2 normal = collision.GetContact(0).normal;
        float vel = Mathf.Abs(Vector2.Dot(normal, velocity / ParticleManager.deltaT));
        Debug.Log(vel);
        
        float sign = 1;

        if (collision.gameObject.transform.rotation.eulerAngles.z >= 180)
            sign = -1;
        
        float angle = Vector2.Angle(lastForce, sign * normal);
        float angleVel = Vector2.Angle(velocity, sign * normal);

        externalForce.x = Mathf.Cos(Mathf.Deg2Rad * angle) * (lastForce.magnitude + colisionForce.magnitude) + Mathf.Cos(Mathf.Deg2Rad * angleVel) * vel ;
        externalForce.y = Mathf.Sin(Mathf.Deg2Rad * angle) * (lastForce.magnitude + colisionForce.magnitude) + Mathf.Sin(Mathf.Deg2Rad * angleVel) * vel ;

        colisionForce += externalForce;
        force += externalForce;
    }

    public void UpdatePosition(float deltaT)
    {
        acceleration = force * mass;
        lastVelocity = velocity;  
        velocity += acceleration * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
        lastForce = force;
        force = new Vector2(0, 0);
        colisionForce = new Vector2(0, 0);
    }
}
