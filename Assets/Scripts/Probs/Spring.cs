using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    private Animator myAnimator;

	void Start () {
        myAnimator = GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            myAnimator.SetTrigger("PlayerDetected");
            
        }
    }

    
}
