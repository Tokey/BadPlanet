using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    public Animator MyAnimator { get; private set; }

    
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected Transform bulletPos;

    [SerializeField]
    protected GameObject bullet;


    protected bool facingRight;

    [SerializeField]
    protected Stat healthStat;

    [SerializeField]
    private EdgeCollider2D laserCollider;

    [SerializeField]
    private List<string> damageSources = new List<string>();
    [SerializeField]
    private float[] dmgAmt;
    public abstract bool IsDead { get; }
    public bool Attacking { get; set; }
    public bool TakingDamage { get; set; }

    public EdgeCollider2D LaserCollider
    {
        get
        {
            return laserCollider;
        }

    }

    public virtual void Start () {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        healthStat.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public virtual void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.y);


    }

    public virtual void FireBullet()
    {
        GameObject tmp = (GameObject)Instantiate(bullet, bulletPos.position, Quaternion.identity);
        if (facingRight)
        {

            tmp.GetComponent<Bullet>().Initalize(Vector2.right);
        }
        else
        {

            tmp.GetComponent<Bullet>().Initalize(Vector2.left);
        }
    }

    public abstract IEnumerator TakeDamage(float dmgAmt);



    public void MeleeAttack() {
        LaserCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage(dmgAmt[(damageSources.IndexOf(other.tag))]));
        }
    }
}
