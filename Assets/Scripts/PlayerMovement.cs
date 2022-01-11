using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] float _speed = 5f;
    
    void Update()
    {
        // Reading the Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Moving
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        if(movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}