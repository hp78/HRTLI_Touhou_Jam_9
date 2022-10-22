using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildItem : MonoBehaviour
{
    [SerializeField] TowerStatSO towerStat;

    public void OnClick()
    {
        TowerSpawner.instance.SetCurrTower(towerStat);
    }
}
