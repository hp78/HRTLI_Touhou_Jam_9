using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public static TowerSpawner instance;

    [SerializeField] TowerStatSO _currTowerSO;
    [SerializeField] BulletStatSO _currBulletSO;

    [SerializeField] GameObject _baseTowerPrefab;

    [SerializeField] GameObject _buildPanel;
    [SerializeField] GameObject _mouseBuildArea;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrTower(TowerStatSO towerSO)
    {
        _currTowerSO = towerSO;
    }

    public void SetCurrBullet(BulletStatSO bulletSO)
    {
        _currBulletSO = bulletSO;
    }

    public void BtnSetBuildLocation()
    {
        _mouseBuildArea.SetActive(true);
    }

    public void TestBuildTower()
    {
        _mouseBuildArea.SetActive(false);
        GameObject go = Instantiate(_baseTowerPrefab,_mouseBuildArea.transform.position,Quaternion.identity);
        go.GetComponent<BaseTower>().SpawnTower(_currBulletSO, _currTowerSO);
    }
}
