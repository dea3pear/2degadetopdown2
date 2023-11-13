using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {   if(canMove) {
        if(movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if(!success) {
                success = TryMove( new Vector2(movementInput.x, 0));
            

                if(!success) {
                success = TryMove( new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("IsMoving", success);
        } else {
            animator.SetBool("IsMoving", false);
        }

      }
    } 

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
        // check for collisions
        int count = rb.Cast(
                direction, // x and y values between -1 and 1 that represent the direction from body to look for the collisions
                movementFilter, // where a collusion can occur
                castCollisions, // list of collisions
                moveSpeed * Time.fixedDeltaTime + collisionOffset // the movement plus an offset
            );
            
        if(count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
            } else {
            return false;
            }
        } else {
            return false;
        }
    }


    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("sAttack");
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
