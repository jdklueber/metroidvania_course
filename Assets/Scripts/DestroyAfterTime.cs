using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Lifetime;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }


}
