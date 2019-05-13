using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxVelocity = 10f;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Limit our velocity
        if (_rigidbody.velocity.magnitude > maxVelocity)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
        }

        // Prevent us from  passing thru the ground
        if (transform.position.y < 0f)
        {
            _rigidbody.MovePosition(new Vector3(transform.position.x, 0f, transform.position.z));
        }
    }
}