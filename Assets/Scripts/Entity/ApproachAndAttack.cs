using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachAndAttack : MonoBehaviour 
{
    private enum MoveState
    {
        Idle,
        Approach,
        Attack
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float idleCooldown;
    [SerializeField] private float moveCooldown;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float acceptableAttackAngleDegrees;
    [SerializeField] private float attackDistance;

    private float stateCooldown;
    private float currentAttackCooldown;
    private Vector3 center;
    private MoveState moveState;
    private GameObject target;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        // Randomize the enemy starts
        stateCooldown = UnityEngine.Random.Range(0f, idleCooldown);
    }

	// Update is called once per frame
	private void Update () 
    {
        stateCooldown -= Time.deltaTime;
        currentAttackCooldown -= Time.deltaTime;

        // Try to find the evil human
        if(target == null)
        {
            target = Game.instance.Player;
        }
        TurnTowardsTarget(target);

        switch(moveState)
        {
            case MoveState.Idle:
                if(stateCooldown <= 0f)
                {
                    IncrementState();
                }
                break;
            case MoveState.Attack:
                if(IsFacingTarget(target) && currentAttackCooldown <= 0f)
                {
                    currentAttackCooldown = attackCooldown;
                    anim.SetTrigger("attack");
                    // If we just attacked, go to idle
                    moveState = MoveState.Idle;
                    anim.SetTrigger("idle");
                    stateCooldown = idleCooldown;
                }
                else
                {
                    IncrementState();
                }
                break;
            case MoveState.Approach:
                var waypoint = GetDestination();
                if(!HasReachedCurrentWaypoint(waypoint))
                {
                    var dir = (waypoint - transform.position).normalized;
                    transform.position += dir * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // If we've reached the evil human, try to attack.
                    IncrementState();
                }
                break;
        }
    }

    private bool IsFacingTarget(GameObject target)
    {
        if(target != null)
        {
            return Vector3.Angle(transform.forward, target.transform.position - transform.position) < acceptableAttackAngleDegrees;
        }
        return false;
    }

    private void TurnTowardsTarget(GameObject target)
    {
        if(target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    private bool HasReachedCurrentWaypoint(Vector3 waypoint)
    {
        return Vector3.Distance(transform.position, waypoint) < attackDistance;
    }

    private Vector3 GetDestination()
    {
        var player = Game.instance.Player.transform.position;
        return new Vector3(player.x, transform.position.y, player.z);
    }

    private void IncrementState()
    {
        switch(moveState)
        {
            case MoveState.Idle:
                moveState = MoveState.Approach;
                stateCooldown = moveCooldown;
                break;
            case MoveState.Attack:
                moveState = MoveState.Approach;
                stateCooldown = moveCooldown;
                break;
            case MoveState.Approach:
                moveState = MoveState.Attack;
                stateCooldown = 0;
                break;
        }
    }
}