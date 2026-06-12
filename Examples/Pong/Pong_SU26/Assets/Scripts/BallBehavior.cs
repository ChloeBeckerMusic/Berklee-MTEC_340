using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 7.0f;
    [SerializeField] private float _speedIncrement = 1.1f;
    
    private Rigidbody2D _rb;
    [SerializeField, Range(0.0f, 1.0f)] private float _paddleInfluence = 0.4f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // Make sure that direction has a vector length of 1
        Vector2 direction = Random.insideUnitCircle.normalized;

        if (Mathf.Abs(direction.y) < 0.4f)
        {
            direction.y += 0.4f * Mathf.Sign(direction.y);
        }
        
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            // Check if the paddle is moving
            if (!Mathf.Approximately(other.rigidbody.linearVelocity.y, 0.0f))
            {
                // Weighted sum using one-minus to calculate weights
                Vector2 direction = _rb.linearVelocity * (1.0f - _paddleInfluence)
                                    + other.rigidbody.linearVelocity * _paddleInfluence;
                
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
            }
        }
    }
}
