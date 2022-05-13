using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float MovementSpeed;
    public float JumpForce;
    public Transform GroundPoint;

    private bool IsOnGround;
    public LayerMask GroundLayer;
    public BulletController Projectile;
    public Transform GunPosition;

    public Animator Animator;

    void Start()
    {
    }

    void Update()
    {

        Rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed , Rigidbody.velocity.y);

        if (Rigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (Rigidbody.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        IsOnGround = Physics2D.OverlapCircle(GroundPoint.position, .2f, GroundLayer);

        if (Input.GetButtonDown("Jump") && IsOnGround)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y + JumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Projectile, GunPosition.position, GunPosition.rotation).MoveDir = new Vector2(transform.localScale.x, 0f);
            Animator.SetTrigger("ShotFired");
        }

        Animator.SetBool("IsOnGround", IsOnGround);
        Animator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));

       
    }
}
