using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuildItem : MonoBehaviour
{
    [SerializeField] BulletStatSO bulletStat;

    public void OnClick()
    {
        TowerSpawner.instance.SetCurrBullet(bulletStat);
    }
}
