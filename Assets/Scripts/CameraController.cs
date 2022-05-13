using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController Player;
    public BoxCollider2D BoundsBox;
    private float HalfHeight;
    private float HalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        HalfHeight = Camera.main.orthographicSize;
        HalfWidth = HalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {

            transform.position = new Vector3(
                Mathf.Clamp(Player.transform.position.x, BoundsBox.bounds.min.x + HalfWidth, BoundsBox.bounds.max.x - HalfWidth),
                Mathf.Clamp(Player.transform.position.y, BoundsBox.bounds.min.y + HalfHeight, BoundsBox.bounds.max.y - HalfHeight),                             
                transform.position.z
                );
        }
    }
}
