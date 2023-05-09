using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 1.0f;
    public float mass = 0.02f;
    public float pressure;

    public bool isActive = true;

    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);

    private void Awake()
    {
        force = new Vector2( 0.0f, -9.81f ) * mass / rho;
        velocity = new Vector3( 0.0f, 0.0f);
    }

    public void UpdatePosition()
    {
        velocity += Time.deltaTime * force / rho;
        transform.position += Time.deltaTime * new Vector3(velocity.x, velocity.y, 0);
    }

    private void LateUpdate()
    {
        force = new Vector2( 0.0f, -9.81f) * mass / rho;
    }
}
