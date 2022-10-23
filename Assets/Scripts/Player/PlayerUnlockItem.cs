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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpgradeIndex(int val)
    {
        upgradeIndex = val;
    }

    public void SetTower(TowerStatSO stat)
    {
        itemName.text = stat.towerName;
        itemImage.sprite = stat.gameSprite;
    }

    public void SetBullet(BulletStatSO stat)
    {
        itemName.text = stat.bulletName;
        itemImage.sprite = stat.gameSprite;
    }

    public void SetGold()
    {
        itemName.text = "Gold";
    }

    public void SetLife()
    {
        itemName.text = "Lives";
    }
}
