using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] List<EnemyBase> _enemyList;

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
        _enemyList = new List<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShot(Time.deltaTime);
    }

    public virtual void UpdateShot(float deltaTime)
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

    public virtual void Shoot(Transform enemy)
    {
        Vector3 direction = enemy.position - transform.position;

        GameObject go = Instantiate(_pfShot,transform.position,Quaternion.identity);
        go.GetComponent<ShotBehaviour>().SpawnBullet(_bulletStat, 
            _towerStat,direction);

        _towerSpriteRender.flipX = (direction.x > 0);
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
            foreach(EnemyBase b in _enemyList)
            {
                float currDist = (b.transform.position - transform.position).magnitude;
                if (b.gameObject.activeInHierarchy && currDist < closestDist)
                {
                    closestEnemy = b.transform;
                    closestDist = currDist;
                }
            }
        }

        return closestEnemy;
    }

    public void AddEnemyToList(EnemyBase enemy)
    {
        if(!_enemyList.Contains(enemy))
            _enemyList.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyBase enemy)
    {
        _enemyList.Remove(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
