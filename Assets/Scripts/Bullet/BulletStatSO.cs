using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletType", order = 1)]
public class BulletStatSO : ScriptableObject
{
    [Space(5)]
    public string bulletName;

    [Space(5)]
    public Sprite gameSprite;

    [Space(5)]
    public float cooldown;

    [Space(5)]
    public int cost;

    [Space(5)]
    public float range;

    [Space(5)]
    public float damage;

    [Space(5)]
    public float speed;

    [Space(5)]
    public int pierce;

    [Space(5)]
    public float dotDmg;
    public float dotDura;

    [Space(5)]
    public float slowVal;
    public float slowDura;

    [Space(5)]
    public float boomerDmg;

    [Space(5)]
    public List<GameObject> pfSpawnOnHit;

    [Space(5)]
    public List<GameObject> pfSpawnOnExpire;
}
