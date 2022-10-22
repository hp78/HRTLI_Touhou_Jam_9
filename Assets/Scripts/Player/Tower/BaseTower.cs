using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] List<Transform> _enemyList;

    [SerializeField] SpriteRenderer _towerSpriteRender;
    [SerializeField] CircleCollider2D _rangeCollider;

    [SerializeField] float _shotCooldown;
    [SerializeField] float _currShotCooldown;

    [SerializeField] GameObject _pfShot;

    [SerializeField] BulletStatSO _bulletStat;
    [SerializeField] TowerStatSO _towerStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShot(Time.deltaTime);
    }

    void UpdateShot(float deltaTime)
    {
        _currShotCooldown -= deltaTime;
        if(_currShotCooldown < 0)
        {
            Transform enemy = GetClosestEnemy();
            if(enemy != null)
            {
                _currShotCooldown = _shotCooldown;
                Shoot(enemy);
            }
        }
    }

    void Shoot(Transform enemy)
    {
        Vector3 direction = enemy.position - transform.position;

        GameObject go = Instantiate(_pfShot,transform.position,Quaternion.identity);
        go.GetComponent<ShotBehaviour>().SpawnBullet(_bulletStat, 
            _towerStat,direction);
    }

    public void SpawnTower(BulletStatSO bulletStat, TowerStatSO towerStat)
    {
        _bulletStat = bulletStat;
        _towerStat = towerStat;

        _towerSpriteRender.sprite = towerStat.gameSprite;

        _shotCooldown = Mathf.Clamp((_bulletStat.cooldown + _towerStat.cooldown), 0.01f, 99f);
        _currShotCooldown = 0f;

        _rangeCollider.radius = Mathf.Clamp((_bulletStat.range + _towerStat.range), 1f, 99f);
    }

    Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDist = float.MaxValue;

        //
        if(_enemyList.Count > 0)
        {
            foreach(Transform t in _enemyList)
            {
                float currDist = (t.position - transform.position).magnitude;
                if (currDist < closestDist)
                {
                    closestEnemy = t;
                    closestDist = currDist;
                }
            }
        }

        return closestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter : " + collision.ToString());
        if(collision.CompareTag("Enemy"))
        {
            _enemyList.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyList.Remove(collision.transform);
        }
    }
}
