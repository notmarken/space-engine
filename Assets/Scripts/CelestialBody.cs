using UnityEngine;
using System.Collections.Generic;

public class CelestialBody : MonoBehaviour
{
    public Vector3 initialVelocity;
    [HideInInspector] public Vector3 velocity;
    public float mass;

    void Awake()
    {
        velocity = initialVelocity;
    }
}
