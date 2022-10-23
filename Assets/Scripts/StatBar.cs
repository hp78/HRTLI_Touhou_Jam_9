using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Image[] bars;

    public void SetBar(float val)
    {
        int intVal = (int)val;
        intVal = Mathf.Clamp(intVal, 0 , 20);

        foreach(Image i in bars)
        {
            i.color = Color.grey;
        }

        for(int i = 0; i < intVal; ++i)
        {
            bars[i].color = Color.white;
        }
    }
}
