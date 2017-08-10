using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTile : MonoBehaviour {

    [SerializeField]
    private float time;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(DestroyTheTile(time));
            Debug.Log("Player Detected");
        }
            


    }

    IEnumerator DestroyTheTile(float time)
    {
       
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
