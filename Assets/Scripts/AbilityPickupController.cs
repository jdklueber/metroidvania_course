using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPickupController : MonoBehaviour
{
    public bool UnlockDoubleJump;
    public bool UnlockDash;
    public bool UnlockBall;
    public bool UnlockDropBomb;

    public GameObject AbilityPickupEffect;
    public string UnlockMessage;

    public TMP_Text UnlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityManager abilities = other.GetComponentInParent<PlayerAbilityManager>();
            if (UnlockDoubleJump)
            {
                abilities.CanDoubleJump = true;
            }

            if (UnlockDash)
            {
                abilities.CanDash = true;
            }

            if (UnlockBall)
            {
                abilities.CanTurnIntoBall = true;
            }

            if (UnlockDropBomb)
            {
                abilities.CanDropBomb = true;
            }

            UnlockText.transform.parent.SetParent(null);
            UnlockText.transform.parent.position = transform.position;
            UnlockText.text = UnlockMessage;
            UnlockText.gameObject.SetActive(true);
            Instantiate(AbilityPickupEffect, transform.position, transform.rotation);
            Destroy(UnlockText.transform.parent.gameObject, 5f);
            Destroy(gameObject);
            

            
        }
    }

}
