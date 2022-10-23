using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health;
    public float speed;
    public float currency;
    bool dead = false;

    [Space(25)]
    public SpriteRenderer spriteR;
    public Color color;

    [Space(25)]
    public GameObject childSpawn;
    public int childSpawnAmount;

    [Space(25)]
    public List<Transform> movePoints;
    public EnemySpawner spawner;
    int currentPoint;

    public Collider2D col;


    float slowDuration;
    float slowVal=0f;

    float dotDuration;
    float dotVal=0f;

    float flickerDuration;


    List<BaseTower> towerList;

    // Start is called before the first frame update
    void Start()
    {
        towerList = new List<BaseTower>();
        spriteR.color = color;
        spawner.listOfEnemies.Add(this.gameObject);
    }

    private void OnDisable()
    {
        foreach (BaseTower bt in towerList)
        {
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }
    }

    private void OnDestroy()
    {
        foreach (BaseTower bt in towerList)
        {
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            Movement();
            Flicker();
            SlowOverTime();
        }
    }

    public void SetMovePoints(List<Transform> points)
    {
        foreach(Transform t in points)
        {
            movePoints.Add(t);
        }
    }

    void Movement()
    {
        if ((Vector2.Distance(this.transform.position, movePoints[currentPoint].position) > 0.1f))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, movePoints[currentPoint].position, speed * (1.0f - slowVal) * Time.deltaTime);
        }
        else
        {
            if (currentPoint + 1 < movePoints.Count)
                currentPoint++;
        }    

    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            OnDeath();
        }
        else
        {
            flickerDuration = 0.1f;
        }
    }

    public void TakeDoT(float dmg, float duration)
    {
        if(this.gameObject.activeSelf)
        if(dmg >= dotVal)
        {
            dotVal = dmg;
            dotDuration = duration;
            StopCoroutine(DamageOverTime());
            StartCoroutine(DamageOverTime());
        }
 
    }

    public void TakeSlow(float val, float duration)
    {
        if(val >= slowVal)
        {
            slowVal = val;
            slowDuration = duration;
        }
    }

    void SlowOverTime()
    {
        if (slowDuration < 0.0f) slowVal = 0.0f;
        else slowDuration -= Time.deltaTime;
    }

    IEnumerator DamageOverTime()
    {
        while(dotDuration >= 0.0f)
        {
            TakeDamage(dotVal / 4.0f);
            yield return new WaitForSeconds(0.25f);
            dotDuration -= 0.25f;
        }
        dotVal = 0;
        yield return 0;
    }

    void OnDeath()
    {
        foreach (BaseTower bt in towerList)
        {
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }

        if (dead)
            return;

        dead = true;
        col.enabled = false;

        GameController.instance.AddGold(currentPoint);

        if (childSpawn)
        {
            StartCoroutine(SpawnChild());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnChild()
    {

        foreach (BaseTower bt in towerList)
        {
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }


        for (int i = 0; i < childSpawnAmount; i++)
        {
            float ranx = Random.Range(-0.2f, 0.2f);
            float rany = Random.Range(-0.2f, 0.2f);
            GameObject temp = Instantiate(childSpawn, this.transform.position+ new Vector3(ranx,rany,0.0f), Quaternion.identity);

            List<Transform> tempList = new List<Transform>();

            for (int j = currentPoint; j < movePoints.Count; j++)
                tempList.Add(movePoints[j]);


            EnemyBase tempEB = temp.GetComponent<EnemyBase>();

            tempEB.spawner = spawner;
            tempEB.SetMovePoints(tempList);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

        }

        foreach (BaseTower bt in towerList)
        {
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }

        this.gameObject.SetActive(false);
        yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("TowerRadius"))
        {
            BaseTower bt = collision.GetComponent<BaseTower>();
            towerList.Add(bt);
            bt.AddEnemyToList(this);
        }

        if (collision.CompareTag("DamageBox"))
        {
            int results;
            if(int.TryParse(collision.gameObject.name, out results))
             TakeDamage(results);
        }
        if (collision.CompareTag("DoTBox"))
        {
            int results;
            if (int.TryParse(collision.gameObject.name, out results))
                TakeDoT(results, 5f);
        }
        if (collision.CompareTag("SlowBox"))
        {
            int results;
            if (int.TryParse(collision.gameObject.name, out results))
                TakeSlow(results/10f, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerRadius"))
        {
            BaseTower bt = collision.GetComponent<BaseTower>();
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
            bt.RemoveEnemyFromList(this);
        }
    }

    void Flicker()
    {
        if (flickerDuration > 0.0f)
        {
            flickerDuration -= Time.deltaTime;

            if (spriteR.color == Color.white)
                spriteR.color = color;

            else
                spriteR.color = Color.white;

        }
        else
            spriteR.color = color;

    }

}
