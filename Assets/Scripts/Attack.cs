using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] bool canAttack;
    [SerializeField] SpriteRenderer attackSprite;
    bool hasAttacked;
    public float timeUntilNextAttack = 1f;
    float timer = 0f;
    CapsuleCollider2D myCapsuleCollider2D;
    PlayerMovement myPlayerMovement;

    private void Awake()
    {
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myPlayerMovement = GetComponentInParent<PlayerMovement>();
    }
    private void Start()
    {
        myCapsuleCollider2D.enabled = !myCapsuleCollider2D.enabled;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (GetComponentInParent<Block>().isBlocking)
            canAttack = false;
        ActivateAttack();
        if (timer < timeUntilNextAttack)
        {
            canAttack = false;
        } 
        else if(timer >= timeUntilNextAttack)
        {
            canAttack = true;
        }
        if (hasAttacked)
        {
            hasAttacked = false;
            timer = 0;
        }
    }
    IEnumerator PerformAttack()
    {
        myCapsuleCollider2D.enabled = !myCapsuleCollider2D.enabled;
        yield return new WaitForSeconds(.01f);
        myCapsuleCollider2D.enabled = !myCapsuleCollider2D.enabled;
        hasAttacked = true;
        myPlayerMovement.attackButtonPressed = false;
    }
    void ActivateAttack()
    {
        if (myPlayerMovement.attackButtonPressed)
        {
            if(canAttack)
            {
                StartCoroutine(DisplayAttackSprite());
                StartCoroutine(PerformAttack());
            }               
        }
    }

    /* private void OnTriggerEnter2D(Collider2D collision) //Idk why I initially made the attack a trigger point
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage();
        }
    } */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Block>().isBlocking)
                collision.gameObject.GetComponent<Health>().TakeMitigatedDamage();
            else
                collision.gameObject.GetComponent<Health>().TakeDamage();
        }
    }

    IEnumerator DisplayAttackSprite()
    {
        attackSprite.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        attackSprite.gameObject.SetActive(false);
    }
}
