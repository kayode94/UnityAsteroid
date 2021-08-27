using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float bulletSpeed = 500.0f;

    public float maxLifetime = 10.0f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        // The bullet only needs a force to be added once since they have no
        // drag to make them stop moving
        _rigidbody.AddForce(direction * this.bulletSpeed);

        // Destroy the bullet after it reaches it max lifetime
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {   
        // Destroy the bullet as soon as it collides with anything
        Destroy(this.gameObject);
    }
    
}
