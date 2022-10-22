using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] int currGold = 0;
    [SerializeField] int currLife;
    [SerializeField] int currWave = 0;

    [Space(5)]
    [SerializeField] bool[] unlockedTowers = new bool[10];
    [SerializeField] bool[] unlockedBullets = new bool[10];

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int val)
    {
        currGold += val;
    }

    public void LoseLife()
    {
        --currLife;
    }

    public void AddLife(int val)
    {
        currLife += val;
    }

    public void AddInterest()
    {
        currGold = currGold + (int)Mathf.Ceil(currGold * 0.1f);
    }
}
