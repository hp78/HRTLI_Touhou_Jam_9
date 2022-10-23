using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        RefreshBuildMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int val)
    {
        currGold += val;
    }

    public void LoseLife()
    {
        --currLife;
    }

    public void AddLife(int val)
    {
        currLife += val;
    }

    public void AddInterest()
    {
        currGold = currGold + (int)Mathf.Ceil(currGold * 0.1f);
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
}
