using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float TimeToExplode = .5f;
    public GameObject Explosion;

    public float BlastRange;
    public LayerMask Destructable;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        TimeToExplode -= Time.deltaTime;
        if (TimeToExplode <= 0)
        {
            if (Explosion != null)
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, BlastRange, Destructable)) {
                Destroy(collider.gameObject);
            }
        }
    }
}
