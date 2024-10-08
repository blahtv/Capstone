using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float speed = 0.125f;
    float jumpCooldown = 0.5f;
    Rigidbody rb;
    PlayerInput input;

    bool grounded = true;

    void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
        this.input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = input.actions.FindAction("Move").ReadValue<Vector2>();
        rb.MovePosition(new Vector3(this.transform.position.x + (moveVector.x * speed), this.transform.position.y, this.transform.position.z));

        jumpCooldown -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && jumpCooldown < 0)
            grounded = true;
    }

    public void Jump()
    {
        if(grounded)
        {
            grounded = false;
            jumpCooldown = 0.5f;
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
        }
        
    }
}
