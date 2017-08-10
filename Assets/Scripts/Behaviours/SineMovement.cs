using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour {

    public float verticalSpeed;
    public float amplitude;

    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = transform.position;
    }

    void FixedUpdate()
    {
        
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }
}
