using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameManager.instance.AddItem();
            Destroy(gameObject);
        }
    }
}
