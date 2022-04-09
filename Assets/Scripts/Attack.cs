using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] bool canAttack;
    bool hasAttacked;
    [SerializeField] float timeUntilNextAttack = 3f;
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
    private void Update()
    {
        timer += Time.deltaTime;
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
        yield return new WaitForSeconds(.001f);
        myCapsuleCollider2D.enabled = !myCapsuleCollider2D.enabled;
        hasAttacked = true;
        myPlayerMovement.attackButtonPressed = false;
    }
    void ActivateAttack()
    {
        if (myPlayerMovement.attackButtonPressed)
        {
            if(canAttack)
                StartCoroutine(PerformAttack());
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
            collision.gameObject.GetComponent<Health>().TakeDamage();
        }
    }
}
