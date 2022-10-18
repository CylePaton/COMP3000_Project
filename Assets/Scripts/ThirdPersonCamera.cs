using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerModel;
    public Rigidbody rb;

    [Header("Variables")]
    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        // rotate orientation
        Vector3 playerDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = playerDir.normalized;

        // rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
