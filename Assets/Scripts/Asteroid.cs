using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        // Randomizing Size, Rotation and Sprite upon start.


        // Randomizing Sprite
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Randomizing Rotation
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        // Random Size 
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size * 2;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if((this.size * 0.5f) > this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        // Set the new asteroid poistion to be the same as the current asteroid
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.05f;

        // Create the new asteroid at half the size of the current
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size / 2;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
