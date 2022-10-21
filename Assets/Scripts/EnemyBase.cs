using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public float speed;

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

    // Start is called before the first frame update
    void Start()
    {
        spriteR.color = color;
        spawner.listOfEnemies.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            Movement();
            
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
            this.transform.position = Vector2.MoveTowards(this.transform.position, movePoints[currentPoint].position, speed * Time.deltaTime);
        }
        else
        {
            if (currentPoint + 1 < movePoints.Count)
                currentPoint++;
        }    

    }

    void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            OnDeath();
    }

    void OnDeath()
    {
        dead = true;
        col.enabled = false;

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
        for (int i = 0; i < childSpawnAmount; i++)
        {
            GameObject temp = Instantiate(childSpawn, this.transform.position, Quaternion.identity);

            List<Transform> tempList = new List<Transform>();

            for (int j = currentPoint; j < movePoints.Count; j++)
                tempList.Add(movePoints[j]);


            EnemyBase tempEB = temp.GetComponent<EnemyBase>();

            tempEB.spawner = spawner;
            tempEB.SetMovePoints(tempList);

            yield return new WaitForEndOfFrame();
        }

        this.gameObject.SetActive(false);
        yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(5);
    }
}
