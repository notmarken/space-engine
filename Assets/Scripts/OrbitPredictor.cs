#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class OrbitPredictor : MonoBehaviour
{
    [System.Serializable]
    public class PredictableBody
    {
        public CelestialBody body;
        public CelestialBody relativeTo; // Optional: show orbit relative to this body (e.g., planet)
    }

    public PredictableBody[] bodies;
    public float gravitationalConstant = 1f;
    public int steps = 1000;
    public float timeStep = 0.1f;

    void OnDrawGizmos()
    {
        if(Application.isPlaying) return; // Skip if playing

        if (bodies == null || bodies.Length == 0) return;

        int count = bodies.Length;
        Vector3[] positions = new Vector3[count];
        Vector3[] velocities = new Vector3[count];
        float[] masses = new float[count];

        // Initialize from current transform and initialVelocity
        for (int i = 0; i < count; i++)
        {
            if (bodies[i].body == null) continue;
            positions[i] = bodies[i].body.transform.position;
            velocities[i] = bodies[i].body.initialVelocity;
            masses[i] = bodies[i].body.mass;
        }

        for (int step = 0; step < steps; step++)
        {
            Vector3[] accelerations = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                if (bodies[i].body == null) continue;

                for (int j = 0; j < count; j++)
                {
                    if (i == j || bodies[j].body == null) continue;

                    Vector3 dir = positions[j] - positions[i];
                    float distSqr = dir.sqrMagnitude + 0.01f; // Prevent division by zero
                    accelerations[i] += gravitationalConstant * masses[j] * dir.normalized / distSqr;
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (bodies[i].body == null) continue;

                velocities[i] += accelerations[i] * timeStep;
                Vector3 newPos = positions[i] + velocities[i] * timeStep;

                Vector3 start = positions[i];
                Vector3 end = newPos;

                if (bodies[i].relativeTo != null)
                {
                    int parentIndex = System.Array.FindIndex(bodies, b => b.body == bodies[i].relativeTo);
                    if (parentIndex >= 0)
                    {
                        start -= positions[parentIndex]; // parent position at current step
                        end -= positions[parentIndex];   // subtract same parent position for relative path
                    }
                }


                Gizmos.color = Color.Lerp(Color.white, Color.cyan, i / (float)count);
                Gizmos.DrawLine(start, end);

                positions[i] = newPos;
            }
        }
    }
}
#endif
