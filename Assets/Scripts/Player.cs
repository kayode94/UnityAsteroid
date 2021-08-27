using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Bullet bulletPrefab;
            
    private Rigidbody2D _rigidbody;

    public float thrustSpeed = 1.0f;

    public float turnSpeed = 1.0f;

    // variables to track the state of player movement
    private bool _thrusting;
    
    // using a float because we need to keep track of the direction
    private float _turnDirection;

    // gets called every single frame the game is running   
    // to check for player input
    private void Update() 
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // gets called on a fixed time interval
    // for physics related code
    private void FixedUpdate()
    {
        if(_thrusting){
            // .AddForce moves the object 
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if(_turnDirection != 0.0f){
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();

        }    
    }
}
