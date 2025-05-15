using UnityEngine;

public class NameTag : MonoBehaviour
{
    public Camera mainCamera;
    public float scaleFactor = 0.002f;

    void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // Always face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        // Maintain same screen size
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        transform.localScale = Vector3.one * distance * scaleFactor;
    }
}
