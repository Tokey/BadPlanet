using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    [SerializeField]
    private GameObject Lock;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Lock.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }


}
