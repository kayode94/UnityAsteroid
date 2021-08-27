using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public int score = 0;

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if(this.lives <= 0)
        {
            GameOver();
        } 
        else 
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if(asteroid.size < 0.75f)
        {
            this.score += 100;
        } else if (asteroid.size < 1.25f){
            this.score += 50;
        } else {
            this.score += 25;
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        // TODO
    }
}
