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
    public List<Waves> listOfWaves;

    public List<GameObject> listOfEnemies;

    public int waveNumber;
    public bool noMoreWave;
    public float timeStamp;

    public bool allSpawned;
    public bool allDead;
    public bool stageOngoing;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Waves w in listOfWaves)
        {
            foreach(WaveDataSO wso in w.waves )
            {
                wso.Spawned = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stageOngoing)
        {
            timeStamp += Time.deltaTime;
            CheckWaveSO();
            CheckEnemies();
        }
        if (Input.GetKeyDown(KeyCode.S))
            StartWave();
    }

    public void StartWave()
    {
        if (!noMoreWave)
        {
            timeStamp = 0;

            if (listOfWaves[waveNumber].waves.Count == 0)
                stageOngoing = false;

            else
            {
                stageOngoing = true;
                allDead = false;
                allSpawned = false;
            }
        }
    }

    void CheckEnemies()
    {
        if (allSpawned)
        {
            allDead = true;
            foreach (GameObject go in listOfEnemies)
            {
                if (go.activeSelf)
                {
                    allDead = false;
                    return;
                }

            }
            stageOngoing = false;
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
}
