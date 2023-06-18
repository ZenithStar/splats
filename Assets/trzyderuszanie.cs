using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class trzyderuszanie : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float movePower = 2000;
    [SerializeField] float airMovePower = 500;
    [SerializeField] float dragFactor = 10;
    [SerializeField] float rotationalVelocity = 100;
    [SerializeField] int numJumps = 2;

    public Vector3 jumpBoostForce;

    private int jumpCount;

    private Vector2 movementVector;
    private float rotationCommand;
    private List <GameObject> currentCollisions = new List <GameObject> (); // TODO: apparently this is not a dependable implementation, as the hooks can be unreliable. look deeper into this

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    private void OnRotate(InputValue input)
    {
        rotationCommand = input.Get<float>();
    }

    private void OnJump(InputValue input)
    {
        if (jumpCount < numJumps)
        {
            jumpCount++;
            rigidBody.AddForce( jumpBoostForce );
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        currentCollisions.Add (collision.gameObject);
        foreach (ContactPoint contact in collision.contacts){
            if (true) { // TODO: Check if the contact is the ground and not a wall or roof (create a normal-force orientation cutoff)
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        currentCollisions.Remove (collision.gameObject);

        if (currentCollisions.Count == 0){
            if ( jumpCount == 0 ) { // e.g. falling off a ledge
                jumpCount = 1;
            }
        }
    }

    void FixedUpdate()
    {
        var powerFactor = currentCollisions.Count == 0 ? airMovePower : movePower;
        rigidBody.AddRelativeForce( new Vector3( movementVector.x * powerFactor, 0.0f, movementVector.y * powerFactor) * Time.fixedDeltaTime);
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotationalVelocity * rotationCommand, 0) * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);

        // inverse squared velocity "air resistance" sim
        var inverseHorizontalVelocity = -(rigidBody.velocity.sqrMagnitude * rigidBody.velocity.normalized); //TODO: instead of normalized, it should be sign, but cbf to write that out for now
        rigidBody.AddForce(dragFactor * inverseHorizontalVelocity * Time.fixedDeltaTime);
    }
}
