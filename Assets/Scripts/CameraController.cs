using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float movementTime;
    public bool moveCam;

    Vector3 moveDirection;

    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    public Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCam)
        {
            HandleMovementInput();
        }
        
    }

    void HandleMovementInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);

    }
}
