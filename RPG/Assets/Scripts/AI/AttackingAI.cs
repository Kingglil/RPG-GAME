using System.Collections;
using UnityEngine;

public class AttackingAI : MonoBehaviour
{

    public bool inCombat = false;
    public bool block = false;
    public float blockChance = 10;
    private bool coroutineStarted = false;
    private float startCombatDistance = 20f;
    private float jumpBoost = 325f;
    private float moveSpeed = 255f;

    private float minActionCooldown = 1f;
    private float maxActionCooldown = 3.5f;

    private Enemy enemy;

    private Vector3[] directions = new Vector3[4] { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coroutineStarted = true;
        enemy = GetComponent<Enemy>();
        StartCoroutine(MoveCoroutine());
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(Player.position, transform.position) <= startCombatDistance)
        {
            inCombat = true;
            AudioManager.Play();
            StartCoroutine(CombatMode());
        }
        else if(!coroutineStarted)
        {
            print("Out of Combat.");
            StopCoroutine(CombatMode());
            coroutineStarted = true;
            inCombat = false;
            StartCoroutine(MoveCoroutine());
            AudioManager.Stop();
        }
    }

    IEnumerator CombatMode()
    {
        while(true)
        {
            if(Random.Range(0, 100) <= blockChance)
            {
                block = true;
            }
            if(Vector3.Distance(transform.position, Player.position) > enemy.attackRange)
            {
                Move(Player.position - transform.position);
                if (Vector3.Distance(transform.position, Player.position) < enemy.attackRange)
                {
                    Attack();
                }
            }
            else
            {
                Attack();
            }
            yield return new WaitForSeconds(Random.Range(minActionCooldown, maxActionCooldown));
        }
    }

    void Attack()
    {
        Player.health -= enemy.damage;
        if(Player.health <= 0)
        {
            Player.Die();
        }
        else
        {
            UIManager.instance.UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        enemy.hp -= damage;
        if(enemy.hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator MoveCoroutine()
    {
        while (!inCombat)
        {
            Move(directions[Random.Range(0, 4)]);
            yield return new WaitForSeconds(2);
        }
        print("In Combat");
        coroutineStarted = false;
        StopCoroutine(MoveCoroutine());
    }

    void Move(Vector3 direction)
    {
        rb.AddForce(Vector3.up * jumpBoost + direction * moveSpeed);
    }
}
