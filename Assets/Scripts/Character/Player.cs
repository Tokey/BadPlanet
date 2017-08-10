using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();


public class Player : Character {

    private static Player instance;
    private AudioManager audioManager;

    [SerializeField]
    private GameObject attackEffect;


    [SerializeField]
    public int coinValue;

    [SerializeField]
    public int bigCoinValue;

    public event DeadEventHandler Dead;

    private bool immortal = false;

    

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

    private float direction;

    private bool move;

    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Player>();

            return instance;
        }
    }


    [SerializeField]
    private Transform[] goundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;

    



    public Rigidbody2D MyRigidbody { get; set; }

    public bool Jumping { get; set; }

    public bool OnGround { get; set; }

    private Vector2 startPos;

    public bool IsFalling
    {
        get {

            return MyRigidbody.velocity.y < 0;

        }

    }



    public override void Start() {

        base.Start();
        startPos = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();


        MyRigidbody = GetComponent<Rigidbody2D>();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("no reference to audio dhur :@");
        }

    }

    void Update()
    {

        if (!TakingDamage && !IsDead)
        {



            if (transform.position.y <= -14f)
            {
                TakeDamage(1000f);
                Death();
                
                //MyRigidbody.velocity = Vector2.zero;
                //transform.position = startPos;
            }
            HandleInputs();
        }

      
    }
    void FixedUpdate() {

        if (!TakingDamage && !IsDead) {

            float horizontal = Input.GetAxis("Horizontal");   
            OnGround = IsGrounded();

            if (move)
            {
                Flip(direction);
                HandleMovement(direction);
            }

            else
            {
                HandleMovement(horizontal);
                Flip(horizontal);
            }
           
            HandleLayers();

        }


    }

    public void OnDead() {
        if (Dead != null) {
            Dead();
        }

    }


    private void HandleMovement(float horizontal)
    {
        if (Jumping && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if (IsFalling)
        {

            gameObject.layer = 10;
            MyAnimator.SetBool("Land", true);
        }
        if (!Attacking && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
       

        MyAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }


    }

    private void HandleInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audioManager.PlaySound("Saber");
            MyAnimator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Space) && !IsFalling)
        {
            MyAnimator.SetTrigger("Jump");

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!GameManager.Instance.BlasterCollected)
                return;
            audioManager.PlaySound("LaserFire");
            MyAnimator.SetTrigger("Fire");

        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in goundPoints)
            {
                Collider2D[] colliders =
                    Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {

                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void FireBullet()
    {
        if (!OnGround&& !GameManager.Instance.BlasterCollected)
        {
            return;
        }
        
        base.FireBullet();
    }



    public override IEnumerator TakeDamage(float dmgAmt)
    {
        if (!immortal)
        {

            healthStat.CurrentVal -= dmgAmt;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("Damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());

                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("Die");

            }
        }
    }

    private IEnumerator IndicateImmortal() {

        while (immortal) {

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);


            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);


        }

    }



    public override bool IsDead
    {
        get
        {

            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }
            return healthStat.CurrentVal <= 0;
        }
    }

    public override void Death()
    {
        MyAnimator.SetBool("Alive", true);
        healthStat.CurrentVal = healthStat.MaxVal;
        MyRigidbody.velocity = Vector2.zero;
        transform.position = startPos;

    }

    public void BtnJump()
    {
        MyAnimator.SetTrigger("Jump");
        Jumping = true;
    }
    public void BtnAttack()
    {
        audioManager.PlaySound("Saber");
        MyAnimator.SetTrigger("Attack");
    }
    public void BtnFire()
    {

        if (!GameManager.Instance.BlasterCollected)
            return;
        audioManager.PlaySound("LaserFire");
        MyAnimator.SetTrigger("Fire");
    }

    public void BtnMove(float direction)
    {
        this.direction = direction;
        move = true;
    }
    public void StopBtnMove()
    {
        direction = 0;
        move = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "World")
        {
            if (Jumping)
            {
                Jumping = false;
                // MyRigidbody.velocity.Set(0,-1);
                //Debug.Log("KAAAAJ KORE NA!");
            }

        }
    }
        private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "World")
        {
            if (Jumping)
            { Jumping = false;
               // MyRigidbody.velocity.Set(0,-1);
                //Debug.Log("KAAAAJ KORE NA!");
            }
            
        }

       if (other.gameObject.tag == "Coin") {

          GameManager.Instance.CollectedCoins+= coinValue;
          Destroy(other.gameObject);

        }

        if (other.gameObject.tag == "BigCoin")
        {

            GameManager.Instance.CollectedCoins += bigCoinValue;
            Destroy(other.gameObject);

        }

        else if (other.gameObject.tag == "Blaster")
        {

            GameManager.Instance.BlasterCollected = true;
            Destroy(other.gameObject);

        }

    }

    public void AttackEffect()
    {
        Instantiate(attackEffect, bulletPos);
    }

}
