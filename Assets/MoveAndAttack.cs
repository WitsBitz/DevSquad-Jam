using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileAttack))]
public class MoveAndAttack : MonoBehaviour 
{
    private enum MoveState
    {
        Idle,
        Dodge,
        Attack
    }

    [SerializeField] public float radius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float idleCooldown;
    [SerializeField] private float dodgeCooldown;
    [SerializeField] private float acceptableAttackAngleDegrees;

    private float currentCooldown;
    private Vector3 center;
    private Vector3 currentWaypoint;
    private MoveState moveState;
    private GameObject target;
    private ProjectileAttack attack;

    private void Start()
    {
        currentWaypoint = center = transform.position;
        attack = GetComponent<ProjectileAttack>();

        // Randomize the enemy starts
        currentCooldown = UnityEngine.Random.Range(0f, idleCooldown);
    }

	// Update is called once per frame
	private void Update () 
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            IncrementState();
        }

        // Try to find the evil human
        if(target == null)
        {
            target = Game.instance.player;
        }
        TurnTowardsTarget(target);

        switch(moveState)
        {
            case MoveState.Idle:
                // Do nothing, obviously
                break;
            case MoveState.Attack:
                if(IsFacingTarget(target))
                {
                    attack.ShootAt(target);
                }
                break;
            case MoveState.Dodge:
                if(!HasReachedCurrentWaypoint())
                {
                    var dir = (currentWaypoint - transform.position).normalized;
                    transform.position += dir * moveSpeed * Time.deltaTime;
                }
                else
                {
                    currentWaypoint = ChooseDodgePosition();
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

    private Vector3 ChooseDodgePosition()
    {
        Vector2 randomInsideCircle = UnityEngine.Random.insideUnitCircle;
        return center + new Vector3(randomInsideCircle.x, 0f, randomInsideCircle.y);
    }

    private bool HasReachedCurrentWaypoint()
    {
        const float CLOSE_ENOUGH = 0.1f;
        return Vector3.Distance(transform.position, currentWaypoint) < CLOSE_ENOUGH;
    }

    private void IncrementState()
    {
        switch(moveState)
        {
            case MoveState.Idle:
                moveState = MoveState.Dodge;
                currentWaypoint = ChooseDodgePosition();
                currentCooldown = dodgeCooldown;
                break;
            case MoveState.Attack:
                moveState = MoveState.Idle;
                currentCooldown = idleCooldown;
                break;
            case MoveState.Dodge:
                moveState = MoveState.Attack;
                currentCooldown = 0;
                break;
        }
    }
}
