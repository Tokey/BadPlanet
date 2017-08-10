using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject CollectablesHitParticle;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameManager.Instance.CollectedCoins++;

          //  Instantiate(CollectablesHitParticle, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
