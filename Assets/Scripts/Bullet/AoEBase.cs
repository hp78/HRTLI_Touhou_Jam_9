using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEBase : MonoBehaviour
{
    public float aoeDamage;
    public float aoeRadius;
    public float dotDamage;
    public float dotDuration;
    public float slowStrength;
    public float slowDuration;

    public float fxfadetime;
    float activeTime = 0.05f;
    float time = 0;

    List<GameObject> listofenemies = new List<GameObject>();

    SpriteRenderer spriteRenderer;
    public Color color;
    public Color newColor;

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = aoeRadius;

        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        newColor = color;
        newColor.a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > activeTime)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (time < fxfadetime)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(color, newColor, time / fxfadetime);
        }

        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBase eb = collision.GetComponent<EnemyBase>();

            if (eb == null) return;

            foreach (GameObject existingeb in listofenemies)
            {
                if (eb.Equals(eb.gameObject)) return;
            }

            listofenemies.Add(eb.gameObject);
            eb.TakeDamage(aoeDamage);
            eb.TakeDoT(dotDamage,dotDuration);
            eb.TakeSlow(slowStrength, slowDuration);
        }
    }
}
