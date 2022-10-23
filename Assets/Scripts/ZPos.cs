using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPos : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
    }
}
