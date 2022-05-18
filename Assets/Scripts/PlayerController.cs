using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float MovementSpeed;
    public float JumpForce;
    public float DashSpeed;
    public float DashTime;
    public Transform GroundPoint;
    

    private bool IsOnGround;
    public LayerMask GroundLayer;
    public BulletController Projectile;
    public Transform GunPosition;

    public GameObject Standing, Ball;
    public float WaitToBall;
    public float BallCounter;
    public Animator BallAnimator;

    public Animator Animator;

    private float DashCounter = 0;
    public float DashCooldown;
    private bool CanDoubleJump = false;
    public SpriteRenderer Sprite;
    public SpriteRenderer Afterimage;
    public float AfterimageLifetime;
    public float TimeBetweenAfterimages;
    private float AfterimageCounter;
    public Color AfterimageColor;
    private float DashCooldownCounter;

    public Transform BombPoint;
    public GameObject Bomb;

    private PlayerAbilityManager Abilities;

    void Start()
    {
        Abilities = GetComponent<PlayerAbilityManager>();
    }

    void Update()
    {
        if (DashCooldownCounter > 0)
        {
            DashCooldownCounter -= Time.deltaTime;
        } else if (Input.GetButtonDown("Fire2") && DashCooldownCounter <= 0 && Standing.activeSelf && Abilities.CanDash)
        {
            DashCounter = DashTime;
            DisplayAfterimage();
        }

        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;
            AfterimageCounter -= Time.deltaTime; 
            if (AfterimageCounter <= 0)
            {
                DisplayAfterimage();
            }


            Rigidbody.velocity = new Vector2(DashSpeed * transform.localScale.x, Rigidbody.velocity.y);
            DashCooldownCounter = DashCooldown;

        }
        else
        {
            
            Rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed, Rigidbody.velocity.y);

            if (Rigidbody.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Rigidbody.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            } 
        }


        IsOnGround = Physics2D.OverlapCircle(GroundPoint.position, .2f, GroundLayer);

        if (Input.GetButtonDown("Jump") && (IsOnGround || (CanDoubleJump && Abilities.CanDoubleJump) ))
        {
            if (IsOnGround)
            {
                CanDoubleJump = true;
            }
            else 
            { 
                CanDoubleJump= false;
                Animator.SetTrigger("DoubleJump");
            }

            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y + JumpForce);
        }

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (Standing.activeSelf)
            {
                Instantiate(Projectile, GunPosition.position, GunPosition.rotation).MoveDir = new Vector2(transform.localScale.x, 0f);
                Animator.SetTrigger("ShotFired");
            }

            if (Ball.activeSelf && Abilities.CanDropBomb)
            {
                Instantiate(Bomb, BombPoint.position, BombPoint.rotation);
            }
        }

        //ball mode
        if (!Ball.activeSelf && Abilities.CanTurnIntoBall)
        {
            if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                BallCounter -= Time.deltaTime;
                if (BallCounter < 0)
                {
                    Ball.SetActive(true);
                    Standing.SetActive(false);
                }
            }
            else 
            {
                BallCounter = WaitToBall;
            }
        } else
        {
            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                BallCounter -= Time.deltaTime;
                if (BallCounter < 0)
                {
                    Ball.SetActive(false);
                    Standing.SetActive(true);
                }
            }
            else
            {
                BallCounter = WaitToBall;
            }
        }

        if (Standing.activeSelf)
        {
            Animator.SetBool("IsOnGround", IsOnGround);
            Animator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
        }

        if (Ball.activeSelf)
        {
            BallAnimator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
        }


    }

    public void DisplayAfterimage()
    {
        SpriteRenderer image = Instantiate(Afterimage, transform.position, transform.rotation);
        image.sprite = Sprite.sprite;
        image.transform.localScale = transform.localScale;
        image.color = AfterimageColor;
        Destroy(image.gameObject, AfterimageLifetime);
        AfterimageCounter = TimeBetweenAfterimages;
    }
}
