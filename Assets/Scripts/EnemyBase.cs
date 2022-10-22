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

    float flickerDuration;

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
            Flicker();
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
        else
        {
            flickerDuration = 0.15f;
        }
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
        }

        this.gameObject.SetActive(false);
        yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBox"))
        {
            int results;
            if(int.TryParse(collision.gameObject.name, out results))
             TakeDamage(results);
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
