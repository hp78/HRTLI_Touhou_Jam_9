using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] int currGold = 0;
    [SerializeField] int currLife = 50;
    [SerializeField] int currWave = 0;

    [Space(10)]
    [SerializeField] TowerStatSO[] towerStats;
    [SerializeField] BulletStatSO[] bulletStats;

    [Space(10)]
    [SerializeField] Toggle[] towerToggles = new Toggle[10];
    [SerializeField] int[] unlockedTowers = new int[10];
    [SerializeField] Toggle[] bulletToggles = new Toggle[10];
    [SerializeField] int[] unlockedBullets = new int[10];

    [Header("UI STUFF")]
    [SerializeField] TMP_Text _lifeTxt;
    [SerializeField] TMP_Text _goldTxt;
    [SerializeField] TMP_Text _waveTxt;

    [SerializeField] Button _buyButton;
    [SerializeField] Button _nextWaveButton;

    [Space(10)]
    [SerializeField] TMP_Text _buildTowerCost;
    [SerializeField] TMP_Text _buildBulletCost;
    [Space(4)]
    [SerializeField] TMP_Text _towerName;
    [SerializeField] TMP_Text _bulletName;
    [Space(4)]
    [SerializeField] StatBar _buildDamageStat;
    [SerializeField] StatBar _buildFireRateStat;
    [SerializeField] StatBar _buildRangeStat;
    [SerializeField] StatBar _buildSpeedStat;
    [SerializeField] StatBar _buildPierceStat;
    [Space(4)]
    [SerializeField] StatBar _buildDotDmgStat;
    [SerializeField] StatBar _buildDotDuraStat;
    [Space(4)]
    [SerializeField] StatBar _buildSlowValStat;
    [SerializeField] StatBar _buildSlowDuraStat;

    [Space(10)]
    [SerializeField] GameObject _playerUnlockPanel;
    [SerializeField] PlayerUnlockItem[] _playerUnlockItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        RefreshBuildMenu();
        RefreshUpgrades();
        AddGold(95);
        _lifeTxt.text = "Lives : " + currLife;
        _waveTxt.text = "Wave : 1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int val)
    {
        currGold += val;
        _goldTxt.text = "Gold : " +currGold;
    }

    public void LoseLife()
    {
        --currLife;
        _lifeTxt.text = "Lives : " + currLife;
    }

    public void AddLife(int val)
    {
        currLife += val;
        _lifeTxt.text = "Lives : " + currLife;
    }

    public void AddInterest()
    {
        currGold = currGold + (int)Mathf.Ceil(currGold * 0.1f);
        _goldTxt.text = "Gold : " + currGold;
    }

    public void RefreshBuildMenu()
    {
        for(int i = 0; i < 10; ++i)
        {
            towerToggles[i].interactable = (unlockedTowers[i] > 0);
            bulletToggles[i].interactable = (unlockedBullets[i] > 0);
        }

        towerToggles[0].Select();
        towerToggles[0].isOn = true;
        bulletToggles[0].Select();
        bulletToggles[0].isOn = true;

        RefreshBuildButton(towerStats[0], bulletStats[0]);
    }

    public void RefreshBuildButton(TowerStatSO towerStat, BulletStatSO bulletStat)
    {
        _buyButton.interactable = (currGold >= (towerStat.cost + bulletStat.cost));

        _buildTowerCost.text = "" +towerStat.cost;
        _buildBulletCost.text = "" +bulletStat.cost;

        _towerName.text = towerStat.towerName;
        _bulletName.text = bulletStat.bulletName;

        _buildDamageStat.SetBar(towerStat.damage + bulletStat.damage + 2);
        _buildFireRateStat.SetBar((1 - towerStat.cooldown - bulletStat.cooldown) * 19f);
        _buildRangeStat.SetBar(towerStat.range + bulletStat.range);
        _buildSpeedStat.SetBar(towerStat.speed + bulletStat.speed - 3f);
        _buildPierceStat.SetBar(towerStat.pierce + bulletStat.pierce);

        _buildDotDmgStat.SetBar(towerStat.dotDmg + bulletStat.dotDmg);
        _buildDotDuraStat.SetBar(towerStat.dotDura + bulletStat.dotDura);

        _buildSlowValStat.SetBar((towerStat.slowVal + bulletStat.slowVal) * 19f);
        _buildSlowDuraStat.SetBar(towerStat.slowDura + bulletStat.slowDura);
    }

    public void ShowEndWaveScreen()
    {
        AddInterest();
        RefreshUpgrades();
        _playerUnlockPanel.SetActive(true);
        _nextWaveButton.interactable = true;
    }

    public void StartNextWave()
    {
        SpawnManager.instance.StartNextWave();
    }

    public void ShowVictoryScreen()
    {

    }

    public void ShowLoseScreen()
    {

    }

    public void TogglePauseMenu()
    {

    }

    int GetItemToUpgrade()
    {
        int val = 0;
        bool isValid = false;

        while(!isValid)
        {
            val = Random.Range(0, 22);
            if(val < 10)
            {
                if (unlockedTowers[val] < 1)
                    isValid = true;
            }
            else if(val < 20)
            {
                if (unlockedBullets[val - 10] < 1)
                    isValid = true;
            }
            else
            {
                isValid = true;
            }
        }

        return val;
    }
    public void RefreshUpgrades()
    {
        foreach(PlayerUnlockItem pui in _playerUnlockItems)
        {
            int val = GetItemToUpgrade();
            pui.SetUpgradeIndex(val);

            if (val < 10)
            {
                pui.SetTower(towerStats[val]);
            }
            else if (val < 20)
            {
                pui.SetBullet(bulletStats[val - 10]);
            }
            else if (val == 20)
            {
                pui.SetGold();
            }
            else
            {
                pui.SetLife();
            }
        }
    }

    public void SelectUpgrade(int val)
    {
        // towers
        if(val < 10)
        {
            unlockedTowers[val] += 1;
            RefreshBuildMenu();
        }

        // shots
        else if(val <20)
        {
            unlockedBullets[val - 10] += 1;
            RefreshBuildMenu();
        }

        // misc
        else if(val == 20)
        {
            AddGold((int)((currWave + 1)*Random.Range(1,14.99f)));
        }
        else if(val == 21)
        {
            AddLife(5);
        }
    }
}
