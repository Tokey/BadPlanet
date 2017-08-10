using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour {
    [SerializeField]
    private int numberofZones;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (numberofZones == 0)
        {
            if (collision.transform.tag == "Player")
            {

                GameManager.Instance.ExitLevel();

            }
        }
            
    }
}
