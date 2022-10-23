using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<EnemySpawner> listOfSpawners;
    public int currentWave = 0;
    public int totalWave;
    public bool endOfWaves = false;
    public bool endOfStage = false;

    // Start is called before the first frame update
    void Start()
    {
        totalWave = listOfSpawners[0].listOfWaves.Count;
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
        EndofWave();

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
        EndofStage();
    }

    void StartNextWave()
    {
        foreach (EnemySpawner es in listOfSpawners)
        {
            es.StartWave();
        }
        endOfWaves = false;
        if(!endOfStage)
        currentWave++;

    }

    public void EndofWave()
    {
        GameController.instance.ShowEndWaveScreen();
    }

    public void EndofStage()
    {
        GameController.instance.ShowVictoryScreen();
    }
}
