using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    private float y0;
    private float x0;

    public float amplitude;
    public float speed;


    [SerializeField]
    public int position;

    public void Start()
    {
       
        y0 = transform.position.y;
        x0 = transform.position.x;
    }
    void Update()
    {
        Hover();
    }

    void Hover()
    {
        if(position==1)
        transform.position = new Vector3(transform.position.x, y0 + amplitude * Mathf.Sin(speed * Time.time), 0f);
        else if(position==2)
            transform.position = new Vector3(transform.position.x, y0 + amplitude * Mathf.Sin(speed * Time.time)*(-1), 0f);

    }
}
