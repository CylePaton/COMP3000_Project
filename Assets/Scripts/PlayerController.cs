using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Input
    private ThirdPersonInput playerInput;
    private InputAction move;
    private InputAction attack;

    //Values
    public float moveSpeed;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVecocity;
    private Vector3 moveDir;
    private bool isMoving;
    private bool isAttacking;

    //References
    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
        playerInput = new ThirdPersonInput();
    }

    private void OnEnable()
    {
        move = playerInput.Player.Move;
        attack = playerInput.Player.Attack;
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        moveDir = new Vector3(move.ReadValue<Vector2>().x, 0f, move.ReadValue<Vector2>().y).normalized;
        isMoving = moveDir.x != 0 || moveDir.z != 0;

        animator.SetBool("IsMoving", isMoving);

        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 isoInput = matrix.MultiplyPoint3x4(moveDir);

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(isoInput.x, isoInput.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVecocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(isoInput * moveSpeed * Time.deltaTime);
        }
        

    }

    private void Attack()
    {
        playerInput.Player.Attack.started += ctx => isAttacking = ctx.ReadValueAsButton();
        playerInput.Player.Attack.canceled += ctx => isAttacking = ctx.ReadValueAsButton();
        animator.SetBool("IsAttacking", isAttacking);
    }
}
