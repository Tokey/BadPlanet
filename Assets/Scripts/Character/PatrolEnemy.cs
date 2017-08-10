using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;


    void FixedUpdate()
    {
        transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveSpeed *= -1;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
            
        
    }

   

    
}
