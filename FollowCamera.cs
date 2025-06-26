using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float followHeight = 4f;           // Camera Y position
    public float cameraDistance = 10f;         // Distance behind the player
    public float smoothSpeed = 5f;             // Follow smoothing
    public float angle = 30f;                  // Downward tilt

    private Vector3 currentVelocity;

    void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }


    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("FollowCamera: No target assigned. Returning...");
            return;
        }

        Debug.Log("FollowCamera updating position...");

        // Track only X and Z from the player
        Vector3 targetXZ = new Vector3(target.position.x, 0f, target.position.z);

        // Camera offset: back and up at a fixed angle
        Quaternion tilt = Quaternion.Euler(angle, 0f, 0f);
        Vector3 offsetDir = tilt * Vector3.back;

        Vector3 desiredPosition = targetXZ + offsetDir * cameraDistance;
        desiredPosition.y = followHeight; // Lock Y height

        // Smoothly move to position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at ground point under the player
        transform.LookAt(targetXZ);
    }
}
