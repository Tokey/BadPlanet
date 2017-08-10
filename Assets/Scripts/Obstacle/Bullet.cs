using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 direction; 
    float fMagnitude = 1f;

    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]

    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        
	}

    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }

    public void Initalize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "World")
        {
            Instantiate(destroyEffect, this.transform.position,Quaternion.identity);
            Destroy(gameObject);

        }

        if (collision.transform.tag == "ExplosiveBox")
        {

            Instantiate(destroyEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);

        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
