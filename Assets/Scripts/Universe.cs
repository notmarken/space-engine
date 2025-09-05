using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    private List<CelestialBody> bodies = new List<CelestialBody>();
    private float gravitationalConstant = 1f;

    private void Awake()
    {
        gravitationalConstant = GetComponent<OrbitPredictor>().gravitationalConstant;

        foreach (var body in GetComponent<OrbitPredictor>().bodies)
        {
            bodies.Add(body.body);
        }
    }

    void FixedUpdate()
    {
        foreach (var body in bodies)
        {
            Vector3 acceleration = Vector3.zero;
            foreach (var other in bodies)
            {
                if (body == other) continue;
                Vector3 direction = other.transform.position - body.transform.position;
                float distanceSqr = direction.sqrMagnitude;
                acceleration += gravitationalConstant * other.mass * direction.normalized / distanceSqr;
            }
            body.velocity += acceleration * Time.fixedDeltaTime;
        }

        foreach (var body in bodies)
        {
            body.transform.position += body.velocity * Time.fixedDeltaTime;
        }
    }
}
