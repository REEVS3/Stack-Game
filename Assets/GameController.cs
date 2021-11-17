using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate {};

    private cubespawn[] spawners;
    private int spawnersIndex;
    private cubespawn currentSpawner;

    private void Awake()
    {
        spawners = FindObjectsOfType<cubespawn>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Move.Current != null)
                Move.Current.Stop();
            
            spawnersIndex = spawnersIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnersIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }




    }
}
