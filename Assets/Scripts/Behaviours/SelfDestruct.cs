using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    [SerializeField]
    private float timeToLive;
	void Start () {
        Destroy(this.gameObject, timeToLive);
	}
	
	
}
