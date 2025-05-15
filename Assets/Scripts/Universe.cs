using UnityEngine;

public class Universe : MonoBehaviour
{
    public CelestialBody[] bodies;
    public float gravitationalConstant = 1f;

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
