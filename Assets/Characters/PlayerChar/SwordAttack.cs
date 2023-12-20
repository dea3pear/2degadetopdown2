using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    public float knockbackForce = 500f;

    Vector2 rightAttackOffset;

    public Collider2D swordCollider;

    private void Start() {
        rightAttackOffset = transform.position;
    }



    public void AttackRight() {
        print ("Attack Right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() {
        print ("Attack Left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            IDamageable damagableObject = other.GetComponent<IDamageable>();

            if(damagableObject != null) {
                Vector3 parentPosition = transform.parent.position;

                Vector2 direction = (Vector2) (other.gameObject.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockbackForce;

                damagableObject.OnHit(damage, knockback);
            } 
        }
    }
}
