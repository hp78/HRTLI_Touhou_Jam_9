using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<EnemySpawner> listOfSpawners;

    public bool endOfWaves = false;
    public bool endOfStage = false;

    // Start is called before the first frame update
    void Start()
    {
       // StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endOfWaves) CheckSpawnersAreDoneForCurrentWave();

        if (Input.GetKeyDown(KeyCode.S))
            StartNextWave();

        if(!endOfStage)
        {
            CheckSpawnersFinishAllWaves();
        }
    }

    void CheckSpawnersAreDoneForCurrentWave()
    {
        endOfWaves = true;
        foreach (EnemySpawner es in listOfSpawners)
        {
            if (!es.allDead)
            {
                endOfWaves = false;
                return;
            }
        }

        if(!endOfStage)
        foreach (EnemySpawner es in listOfSpawners)
        {
            es.ShowDirection();
        }

    }

    void CheckSpawnersFinishAllWaves()
    {
        foreach (EnemySpawner es in listOfSpawners)
        {
            if (!es.noMoreWave)
            {
                endOfStage = false;
                return;
            }
        }
        endOfStage = true;

    }

    void StartNextWave()
    {
        foreach (EnemySpawner es in listOfSpawners)
        {
            es.StartWave();
        }
        endOfWaves = false;
    }
}
