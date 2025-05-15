using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float sensitivity = 5f;
    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -60f, 60f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
