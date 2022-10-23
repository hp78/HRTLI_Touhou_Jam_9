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
    [SerializeField] BuildAreaScript _buildAreaScript;

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
        RefreshPurchaseAbility();
    }

    public void SetCurrBullet(BulletStatSO bulletSO)
    {
        _currBulletSO = bulletSO;
        RefreshPurchaseAbility();
    }

    void RefreshPurchaseAbility()
    {
        GameController.instance.RefreshBuildButton(_currTowerSO, _currBulletSO);
    }

    public void BtnSetBuildLocation()
    {
        _mouseBuildArea.SetActive(true);
        _buildAreaScript.SetRadius(_currTowerSO.range + _currBulletSO.range);
    }

    public void BuildTower()
    {
        _mouseBuildArea.SetActive(false);
        GameObject go = Instantiate(_baseTowerPrefab,_mouseBuildArea.transform.position,Quaternion.identity);
        go.GetComponent<BaseTower>().SpawnTower(_currBulletSO, _currTowerSO);

        GameController.instance.AddGold(-(_currTowerSO.cost + _currBulletSO.cost));
    }
}
