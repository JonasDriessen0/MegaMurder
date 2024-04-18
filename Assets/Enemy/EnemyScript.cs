using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public Animator animator;
    public LayerMask Ground, whatIsPlayer;
    
    private bool eyeGlowPlayed = false;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    private float initialSpeed;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        initialSpeed = agent.speed;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Idle();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        animator.SetBool("DoRun", (agent.velocity.magnitude > 0 && alreadyAttacked == false));
    }

    private void Idle()
    {
        agent.isStopped = true;
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        if (!eyeGlowPlayed)
        {
            animator.SetTrigger("DoEyeGlow");
            
            eyeGlowPlayed = true;
        }
    }

    private void AttackPlayer()
    {
        agent.speed = 0;
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("DoAttack");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        agent.speed = initialSpeed;
    }
}
