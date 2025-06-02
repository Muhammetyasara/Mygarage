using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private AnimatorController animatorController;

    Rigidbody rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    public bool isCarrying;

    public Vector3 moveVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    Vector3 direction;
    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            if (Mathf.Abs(moveVector.x) > 0 && Mathf.Abs(moveVector.z) > 0)
            {
                animatorController.BlendTree(Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical));
            }
        }

        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            animatorController.BlendTree(0f);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -66.5f, 55.2f),
                                         0f, Mathf.Clamp(transform.position.z, -23f, 21.5f));

        rb.velocity = moveVector;
        transform.rotation = Quaternion.LookRotation(direction);

        animatorController.isCarrying = isCarrying;
    }
}