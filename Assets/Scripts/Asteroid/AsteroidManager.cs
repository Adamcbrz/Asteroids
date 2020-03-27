using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{

    #region Unity Exposed Variables

    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int preAllocatedAstroids = 10;
    [SerializeField] private GameObject asteroidParticlePrefab;
    [SerializeField] private Range spawnRange = new Range { min = 10, max = 30 };

    #endregion

    #region Private Variables

    private ItemPool<Asteroid> asteroidPool;
    private ItemPool<AsteroidParticle> particlePool;
    private int activeAsteroidCount = 0;

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        asteroidPool = new ItemPool<Asteroid>(asteroidPrefab, preAllocatedAstroids);
        particlePool = new ItemPool<AsteroidParticle>(asteroidParticlePrefab, preAllocatedAstroids);
        
    }

    #endregion

    #region Public Methods

    public void CreateAstroids(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Asteroid asteroid = asteroidPool.Borrow();
            asteroid.SetType(Asteroid.AsteroidType.Large);
            asteroid.onDestroyed.AddListener(OnAsteroidDestroyed);
            asteroid.transform.position = EuclideanTorus.current.Wrap(RandomPointInCircle());
            asteroid.gameObject.SetActive(true);
        }
    }

    #endregion

    #region Private Methods

    Vector3 RandomPointInCircle()
    {
        return Quaternion.Euler(0, 0, UnityEngine.Random.Range(-180f, 180f)) * (Vector3.up * spawnRange.Random());
    }

    void CreateSmallAsteroidGroup(Vector3 position)
    {
        for (int i = 0; i < 3; i++)
        {
            Asteroid asteroid = asteroidPool.Borrow();
            asteroid.SetType(Asteroid.AsteroidType.Small);
            asteroid.onDestroyed.AddListener(OnAsteroidDestroyed);
            asteroid.transform.position = position + Quaternion.Euler(0, 0, 120 * i) * Vector3.up * 1;
            asteroid.gameObject.SetActive(true);
        }
    }

    void OnAsteroidDestroyed(Asteroid asteroid)
    {
        asteroid.onDestroyed.RemoveListener(OnAsteroidDestroyed);
        AsteroidParticle particle = particlePool.Borrow();
        particle.transform.localScale = asteroid.transform.localScale;
        particle.transform.position = asteroid.transform.position;
        particle.gameObject.SetActive(true);
        particle.onDisposed.AddListener(OnParticleDisposed);
        if (asteroid.asteroidType == Asteroid.AsteroidType.Large)
            CreateSmallAsteroidGroup(asteroid.transform.position);
        asteroidPool.Return(asteroid);
    }

    void OnParticleDisposed(AsteroidParticle particle)
    {
        particle.onDisposed.RemoveListener(OnParticleDisposed);
        particlePool.Return(particle);
    }

    #endregion
}