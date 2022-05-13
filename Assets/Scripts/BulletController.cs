using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletSpeed;
    public Rigidbody2D Rigidbody;
    public Vector2 MoveDir;
    public GameObject ImpactEffect;

    // Update is called once per frame
    void Update()
    {
        Rigidbody.velocity = MoveDir * BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
