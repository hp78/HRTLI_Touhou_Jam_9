using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUnlockItem : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public Image itemImage;

    public int upgradeIndex;

    [Space(4)]
    [SerializeField] GameObject _barPanel;
    [SerializeField] TMP_Text _costText;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        GameController.instance.SelectUpgrade(upgradeIndex);
    }

    public void SetUpgradeIndex(int val)
    {
        upgradeIndex = val;
    }

    public void SetTower(TowerStatSO stat)
    {
        _barPanel.SetActive(true);
        itemDesc.gameObject.SetActive(false);
        itemName.text = stat.towerName;
        itemImage.sprite = stat.gameSprite;

        _costText.gameObject.SetActive(true);
        _costText.text = "" +stat.cost;

        _buildDamageStat.SetBar(stat.damage + 2);
        _buildFireRateStat.SetBar((1 - stat.cooldown) * 19f);
        _buildRangeStat.SetBar(stat.range);
        _buildSpeedStat.SetBar(stat.speed - 3f);
        _buildPierceStat.SetBar(stat.pierce);


        _buildDotDmgStat.SetBar(stat.dotDmg);
        _buildDotDuraStat.SetBar(stat.dotDura);


        _buildSlowValStat.SetBar(stat.slowVal * 19f);
        _buildSlowDuraStat.SetBar(stat.slowDura);
    }

    public void SetBullet(BulletStatSO stat)
    {
        _barPanel.SetActive(true);
        itemDesc.gameObject.SetActive(false);
        itemName.text = stat.bulletName;
        itemImage.sprite = stat.gameSprite;

        _costText.gameObject.SetActive(true);
        _costText.text = "" +stat.cost;

        _buildDamageStat.SetBar(stat.damage + 2);
        _buildFireRateStat.SetBar((1 - stat.cooldown) * 19f);
        _buildRangeStat.SetBar(stat.range);
        _buildSpeedStat.SetBar(stat.speed - 3f);
        _buildPierceStat.SetBar(stat.pierce);


        _buildDotDmgStat.SetBar(stat.dotDmg);
        _buildDotDuraStat.SetBar(stat.dotDura);


        _buildSlowValStat.SetBar(stat.slowVal * 19f);
        _buildSlowDuraStat.SetBar(stat.slowDura);
    }

    public void SetGold()
    {
        _barPanel.SetActive(false);
        _costText.gameObject.SetActive(false);
        itemName.text = "Gold";
        itemDesc.gameObject.SetActive(true);
        itemDesc.text = "Gain random amount of bonus gold";
    }

    public void SetLife()
    {
        _barPanel.SetActive(false);
        _costText.gameObject.SetActive(false);
        itemName.text = "Lives";
        itemDesc.gameObject.SetActive(true);
        itemDesc.text = "Gain 5 additional lives";

    }
}
