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

    [Space(10)]
    [SerializeField] GameObject _playerUnlockPanel;
    [SerializeField] PlayerUnlockItem[] _playerUnlockItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        RefreshBuildMenu();
        RefreshUpgrades();
        AddGold(20);
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
        bulletToggles[0].Select();
    }

    public void RefreshBuildButton(TowerStatSO towerStat, BulletStatSO bulletStat)
    {
        _buyButton.interactable = (currGold > (towerStat.cost + bulletStat.cost));
    }

    public void ShowEndWaveScreen()
    {
        AddInterest();
        _playerUnlockPanel.SetActive(true);
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
            unlockedBullets[val] += 1;
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
