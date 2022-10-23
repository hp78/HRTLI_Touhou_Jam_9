using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveDataSO", order = 1)]
public class WaveDataSO : ScriptableObject
{
    public GameObject EnemyPrefab;
    public int AmountToSpawn;

    public float TimeStampToSpawn;
    public float SpawnInterval;

    public bool Spawned;
}
