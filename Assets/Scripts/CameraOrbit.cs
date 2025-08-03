using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float minDistance = 1.0f; // Minimum allowed distance to avoid clipping too close
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public LayerMask collisionMask; // Set to Environment layer in inspector

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 desiredCameraPos = target.position - rotation * Vector3.forward * distance;

        // Add a small offset to ray origin to avoid hitting player
        Vector3 rayOrigin = target.position + Vector3.up * 1.5f;

        // Check for obstacles
        if (Physics.Raycast(rayOrigin, desiredCameraPos - rayOrigin, out RaycastHit hit, distance, collisionMask))
        {
            float adjustedDistance = Mathf.Clamp(hit.distance, minDistance, distance);
            desiredCameraPos = rayOrigin + (desiredCameraPos - rayOrigin).normalized * adjustedDistance;
        }

        transform.rotation = rotation;
        transform.position = desiredCameraPos;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

}