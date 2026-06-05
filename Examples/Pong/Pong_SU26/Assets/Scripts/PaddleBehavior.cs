using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;

    public KeyCode UpDirection = KeyCode.UpArrow;
    public KeyCode DownDirection = KeyCode.DownArrow;
    
    void Update()
    {
        // Create a movement variable
        Vector3 movement = Vector3.zero;
        
        // Update variable based on player's input
        if (Input.GetKey(UpDirection))
        {
            movement.y += Speed;
        }
        
        if (Input.GetKey(DownDirection))
        {
            movement.y -= Speed;
        }
        
        // Consider frame rate to make game platform agnostic
        movement *= Time.deltaTime;
        
        // Apply movement to the current position
        transform.position += movement;
    }
}
