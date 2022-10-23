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
        cd = -1;
        dura = invulduration;
    }

    // Update is called once per frame
    void Update()
    {
        InvulSkill();
    }

    void InvulSkill()
    {
        if (cd > 0.0f) cd -= Time.deltaTime;

        if (cd <0.0f)
        {
            invulEffect.SetActive(true);
            enemybase.col.enabled = false;
            dura -= Time.deltaTime;

            if(dura < 0.0f)
            {
                invulEffect.SetActive(false);
                enemybase.col.enabled = true;
                cd = invulCooldown;
                dura = invulduration;
            }
        }

    }
}
