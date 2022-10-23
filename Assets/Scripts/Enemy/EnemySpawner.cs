using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public List<WaveDataSO> waves;
}

public class EnemySpawner : MonoBehaviour
{

    public List<Transform> movePoints;
    int currentPoint;
    bool showDirection =true; 
    public List<Waves> listOfWaves;

    public List<GameObject> listOfEnemies;

    public TrailRenderer directionShow;

    public int waveNumber;
    public float timeStamp;

    public bool noMoreWave;

    public bool allSpawned;
    public bool allDead;
    public bool stageOngoing;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Waves w in listOfWaves)
        {
            foreach (WaveDataSO wso in w.waves)
            {
                wso.Spawned = false;
            }
        }
        ShowDirection();

    }

    // Update is called once per frame
    void Update()
    {
        if (stageOngoing)
        {
            timeStamp += Time.deltaTime;

            if(!allSpawned)
            CheckWaveSO();


            CheckEnemies();
        }

        if (showDirection) Movement();
        
    }

    public void StartWave()
    {
        if (!noMoreWave)
        {
            timeStamp = 0;

            if (listOfWaves[waveNumber].waves.Count == 0)
            {
                stageOngoing = false;
                allDead = true;
                allSpawned = true;

                if (waveNumber + 1 < listOfWaves.Count) waveNumber++;
                else noMoreWave = true;
            }

            else
            {
                stageOngoing = true;
                allDead = false;
                allSpawned = false;
            }
        }
        showDirection = false;
    }

    void CheckEnemies()
    {
        if (allSpawned)
        {
            foreach (GameObject go in listOfEnemies)
            {
                if (go.activeSelf)
                {
                    allDead = false;
                    return;
                }

            }
            allDead = true;
            stageOngoing = false;

            foreach (WaveDataSO so in listOfWaves[waveNumber].waves)
            {
                so.Spawned = false;
            }

            if (waveNumber + 1 < listOfWaves.Count) waveNumber++;
            else noMoreWave = true;

            ClearAllEnemiesGO();
        }
    }

    void CheckWaveSO()
    {
        allSpawned = true;
        foreach(WaveDataSO so in listOfWaves[waveNumber].waves)
        {
            
            if(!so.Spawned)
            {
                if (timeStamp > so.TimeStampToSpawn)
                    StartCoroutine(SpawnWave(so));

                allSpawned = false;
            }
        }
    }

    IEnumerator SpawnWave(WaveDataSO waveData)
    {
        waveData.Spawned = true;
        for(int i = 0;i<waveData.AmountToSpawn; i++)
        {
            GameObject temp = Instantiate(waveData.EnemyPrefab, this.transform.position, Quaternion.identity);
            EnemyBase tempEB = temp.GetComponent<EnemyBase>();

            tempEB.spawner = this;
            tempEB.SetMovePoints(movePoints);

            yield return new WaitForSeconds(waveData.SpawnInterval);
        }
        yield return 0;
    }

    void ClearAllEnemiesGO()
    {
        foreach(GameObject go in listOfEnemies)
        {
            Destroy(go);
        }
        listOfEnemies.Clear();

    }

    void Movement()
    {
        if ((Vector2.Distance(directionShow.transform.position, movePoints[currentPoint].position) > 0.1f))
        {
            directionShow.transform.position = Vector2.MoveTowards(directionShow.transform.position, movePoints[currentPoint].position, 15f * Time.deltaTime);
        }
        else
        {
            if (currentPoint + 1 < movePoints.Count)
                currentPoint++;

            else
            {
                currentPoint = 0;
            }
        }

        if (currentPoint == 0)
        {
            directionShow.emitting = false; 
        }
        else
            directionShow.emitting =true;

    }

    public void ShowDirection()
    {
        if (!noMoreWave)
        {
            if (listOfWaves[waveNumber].waves.Count == 0)
                showDirection = false;
            else
            {
                showDirection = true;
                directionShow.transform.position = this.transform.position;
                currentPoint = 0;
            }
        }
    }

}
