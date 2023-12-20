using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DamageableChar : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public float Health {
        set {
            if( value < health) {
                animator.SetTrigger("Hit");
            }

            health = value;

            if(health <= 0) {
                animator.SetBool("Alive", false);
            }
        }
        get {
            return health;
        }
    }

    public float health = 1;



    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("Alive", true);

        rb = GetComponent<Rigidbody2D>();
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }

    public void PlayerDeath(){
        rb.simulated = false;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
}
