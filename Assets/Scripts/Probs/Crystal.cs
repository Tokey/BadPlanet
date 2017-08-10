using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

    [SerializeField]
    private GameObject crystalHitParticle;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameManager.Instance.CollectCrystal();

            Instantiate(crystalHitParticle,transform.position,Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
