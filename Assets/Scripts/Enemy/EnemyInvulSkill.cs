using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvulSkill : MonoBehaviour
{

    public float invulduration;
    public float invulCooldown;
    public EnemyBase enemybase;
    public GameObject invulEffect;
    float cd;
    float dura;
    // Start is called before the first frame update
    void Start()
    {
        cd = invulCooldown;
        dura = invulduration;
    }

    // Update is called once per frame
    void Update()
    {
        if (dura > 0.0f) invulduration -= Time.deltaTime;
        if (cd > 0.0f) cd -= Time.deltaTime;
    }
}
