using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float bounceForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");   // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");     // W/S or Up/Down

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveX * moveSpeed;
        velocity.z = moveZ * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && rb.linearVelocity.y <= 0f)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = bounceForce;
            rb.linearVelocity = velocity;
        }
    }
}
