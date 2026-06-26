using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 7.0f;
    [SerializeField] private float _speedIncrement = 1.1f;

    private float _steepnessThresholdY = 1.0f;
    
    private Rigidbody2D _rb;
    [SerializeField, Range(0.0f, 1.0f)] private float _paddleInfluence = 0.4f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        ResetBall();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameBehavior.Instance.Score(
            Mathf.Sign(transform.position.x) > 0 ? 1 : 2
        );
        
        Destroy(gameObject);
    }

    private void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        
        transform.position = Vector3.zero;
        // transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
        
        // Make sure that direction has a vector length of 1
        Vector2 direction = Random.insideUnitCircle.normalized;

        if (Mathf.Abs(direction.y) < _steepnessThresholdY)
        {
            direction.y += _steepnessThresholdY * Mathf.Sign(direction.y);
        }
        
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
