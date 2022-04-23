using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 moveDirection;
    public float speedModifier;
    public Animator animator;
    public AudioSource audioData;
    public float lastX = 1;
    public float lastY = 1;
    public int soundTimer;
    int frameCounter = 0;


    void Update()
    {
        ProcessInputs();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);
        
        lastX = movement.x == 1 || movement.x == -1 ? movement.x : lastX;
        lastY = movement.y == 1 || movement.y == -1 ? movement.y : lastX;

        animator.SetFloat("Horizontal", rigidBody.velocity.x);
        animator.SetFloat("Vertical", rigidBody.velocity.y);


        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }

        animator.SetFloat("Magnitude",movement.magnitude);

        animator.SetBool("IsIdle",rigidBody.velocity.x == 0 && rigidBody.velocity.y==0);
        
    }

    void FixedUpdate()
    {
        Move();
        
        if(rigidBody.velocity.x > 0.05 || rigidBody.velocity.x < -0.05 || rigidBody.velocity.y > 0.05 || rigidBody.velocity.y < -0.05) {
            frameCounter = (frameCounter>=soundTimer/(Input.GetButton("Sprint") ? 1.5 : 1) ? 0 : frameCounter+1);
            if(frameCounter==1) AudibleStep();
        }
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() 
    {
        float trueSpeed = (float)(movementSpeed * (Input.GetButton("Sprint") ? 1.5 : 1));
        animator.speed = (float)(Input.GetButton("Sprint") ? 1.5 : 1);
        rigidBody.velocity = new Vector2(moveDirection.x * trueSpeed, moveDirection.y * trueSpeed);
    }

    void AudibleStep() {

        audioData.Play(0);

    }
}
