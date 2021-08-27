using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnDistance = 15.0f;

    public float trajectoryVariance = 15.0f;

    public Asteroid asteroidPrefab;

    // A variable for the parameter of InvokeRepeating
    public float spawnRate = 2.0f;
    
    public int spawnAmount = 1;
    // We want to spawn a new asteroid at a regular rate
    // using InvokeRepeating()

    private void Start() 
    {
        // Will be called repeatedly every 2s 
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            // Calculate a random variance in the asteroid's rotation which
            // causes the trajectory to change
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Creating the new asteroid by cloing the prefab and setting a 
            // random size within the range
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            // Setting the trajectory to move in the direction of the spawner
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
