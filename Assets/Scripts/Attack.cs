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
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        boxCollider2D.enabled = !boxCollider2D.enabled;
    }
    private void Update()
    {
        timer += Time.deltaTime;
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
        boxCollider2D.enabled = !boxCollider2D.enabled;
        yield return new WaitForSeconds(.3f);
        boxCollider2D.enabled = !boxCollider2D.enabled;
        hasAttacked = true;
    }
    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            if(canAttack)
                StartCoroutine(PerformAttack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage();
        }
    }
}
