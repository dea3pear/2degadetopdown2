using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float damage = 1;

    public float moveSpeed = 1f;

    public float knockbackForce = 30;

    public DetectionZone detectionZone;
    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if(detectionZone.detectedObjs.Count > 0) {
            // direction to player
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            // move towards player
            rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {
        {
        Debug.Log("Entered trigger with tag: " + col.tag);
        if (col.CompareTag("Enemy") || col.CompareTag("Player") && col.GetComponent<IDamageable>() != null)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.OnHit(damage);
            }
        }
        }
    }
}
