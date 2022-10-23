using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaScript : MonoBehaviour
{
    [SerializeField] Color canBuildColor;
    [SerializeField] Color cannotBuildColor;

    [SerializeField] Transform rangeRadius;
    [SerializeField] SpriteRenderer spriteRender;
    bool canBuild = false;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,0)) + new Vector3(0,0,10);

        if(Input.GetMouseButtonDown(0) && canBuild)
        {
            TowerSpawner.instance.BuildTower();
        }

        if(Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetRadius(float val)
    {
        rangeRadius.localScale = new Vector3(val * 2, val * 2, 1);
    }

    private void FixedUpdate()
    {
        spriteRender.color = canBuildColor;
        canBuild = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerPosition") || collision.CompareTag("Unbuildable"))
        {
            spriteRender.color = cannotBuildColor;
            canBuild = false;
        }
    }
}
