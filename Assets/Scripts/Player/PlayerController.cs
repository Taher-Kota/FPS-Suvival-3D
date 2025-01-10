using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterControl;
    public float speed;
    private Vector3 MoveDirection;
    private float VerticalVelocity;
    private float GravityForce = 50f;
    private float jumpForce = 25f;

    private void Awake()
    {
        characterControl = GetComponent<CharacterController>();
        speed = 4f;
    }

    private void FixedUpdate()
    {
       MovePlayer();
    }        

    protected void MovePlayer()
    {
        MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);
        ApplyGravity();
        characterControl.Move(MoveDirection * speed *Time.deltaTime);
    }

    private void ApplyGravity()
    {
        VerticalVelocity -= GravityForce * Time.deltaTime;
        Jump();
        MoveDirection.y = VerticalVelocity * Time.deltaTime;
    }

    private void Jump()
    {
        if (characterControl.isGrounded && Input.GetKey(KeyCode.Space))
        {
            VerticalVelocity = jumpForce;
        }
    }
}
