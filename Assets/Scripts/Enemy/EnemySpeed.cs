using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    public float speedDuration;
    public float speedCooldown;
    public float speedMultiplier;
    public EnemyBase enemybase;
    public GameObject[] speedEffect;
    float baseSpeed;
    float cd;
    float dura;
    // Start is called before the first frame update
    void Start()
    {
        cd = -1f;
        dura = speedDuration;
        baseSpeed = enemybase.speed;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedSkill();
    }

    void SpeedSkill()
    {
        if (cd > 0.0f) cd -= Time.deltaTime;

        if (cd < 0.0f)
        {
            foreach (GameObject go in speedEffect)
                go.SetActive(true);

            enemybase.speed = baseSpeed * speedMultiplier;
            dura -= Time.deltaTime;

            if (dura < 0.0f)
            {
                foreach (GameObject go in speedEffect)
                    go.SetActive(false);

                enemybase.speed = baseSpeed;
                cd = speedCooldown;
                dura = speedDuration;
            }
        }
    }
}
