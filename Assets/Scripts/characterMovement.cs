using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{

    public CharacterController characterController;
    public float speed;
    private Vector3 camRotation;
    private Transform cam;
    private Vector3 moveDirection;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    private Rigidbody rb;

    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;

    private void Start(){
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f,2.0f,0.0f);
    }

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(horizontalMove, 0, verticalMove);
            moveDirection = transform.TransformDirection(moveDirection);
        }

        moveDirection.y -= 1 * Time.deltaTime;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded){
            Debug.Log("SPACE GROUNDED");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }
/*
    public float speed;
    public float rotationSpeed;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f,2.0f,0.0f);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        
        if (movementDirection != Vector3.zero)
        {
            
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            //transform.rotation *= Quaternion.AngleAxis(-5f, Vector3.up);        
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    */
}
