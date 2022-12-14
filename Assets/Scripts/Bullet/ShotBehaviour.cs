using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer _bulletSprite;
    [SerializeField] GameObject _slowEffect;
    [SerializeField] GameObject _dotEffect;

    float _speed;

    int _pierce;
    int _currPierce;

    float _dotDmg;
    float _dotDura;

    float _slowVal;
    float _slowDura;

    List<GameObject> _pfSpawnOnHit = new List<GameObject>();
    List<GameObject> _pfSpawnExpire = new List<GameObject>();

    BulletStatSO _bulletStat;
    TowerStatSO _towerStat;

    Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    public void SpawnBullet(BulletStatSO bulletStat, TowerStatSO towerStat, Vector3 direction)
    {
        //
        _bulletSprite.sprite = bulletStat.gameSprite;

        //
        _bulletStat = bulletStat;
        _towerStat = towerStat;

        //
        _speed = Mathf.Clamp((bulletStat.speed + towerStat.speed),0.1f,99);
        _pierce = Mathf.Clamp((bulletStat.pierce + towerStat.pierce), 0, 99);
        _currPierce = _pierce;

        //
        _dotDmg = bulletStat.dotDmg + towerStat.dotDmg;
        _dotDura = bulletStat.dotDura + towerStat.dotDura;
        _dotEffect.SetActive(_dotDmg > 0);

        //
        _slowVal = bulletStat.slowVal + towerStat.slowVal;
        _slowDura = bulletStat.slowDura + towerStat.slowDura;
        _slowEffect.SetActive(_slowVal > 0);

        //
        _pfSpawnOnHit.AddRange(bulletStat.pfSpawnOnHit);
        _pfSpawnOnHit.AddRange(towerStat.pfSpawnOnHit);

        //
        _pfSpawnExpire.AddRange(bulletStat.pfSpawnOnExpire);
        _pfSpawnExpire.AddRange(towerStat.pfSpawnOnExpire);

        //
        _direction = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _currPierce -= 1;

            EnemyBase eb = collision.GetComponent<EnemyBase>();

            if (eb == null) return;

            // do damage to enemy
            eb.TakeDamage( Mathf.Clamp((_bulletStat.damage + _towerStat.damage),0.1667f,99f));

            // do dot to enemy
            eb.TakeDoT(_dotDmg, _dotDura);

            // do slow to enemy
            eb.TakeSlow(_slowVal, _slowDura);
            
            // spawn on contact prefabs
            if(_pfSpawnOnHit.Count > 0)
            {
                foreach(GameObject pf in _pfSpawnOnHit)
                {
                    Instantiate(pf, transform.position, Quaternion.identity);
                }
            }

            if (_currPierce < 0)
            {
                // spawn on destroyed prefabs
                if (_pfSpawnExpire.Count > 0)
                {
                    foreach (GameObject pf in _pfSpawnExpire)
                    {
                        Instantiate(pf, transform.position, Quaternion.identity);
                    }
                }
                //Debug.Log("Bullet despawned");
                Destroy(gameObject);
            }
                
        }

        if(collision.CompareTag("MapBoundary"))
        {
            Destroy(gameObject);
        }
    }
}
