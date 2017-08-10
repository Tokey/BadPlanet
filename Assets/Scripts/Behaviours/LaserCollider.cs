using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag) {
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
