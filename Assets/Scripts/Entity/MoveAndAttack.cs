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

    [SerializeField] private bool canRecenterOnDodge;
    [SerializeField] public float radius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float idleCooldown;
    [SerializeField] private float dodgeCooldown;
    [SerializeField] private float acceptableAttackAngleDegrees;
    [SerializeField] private float dodgeCompleteDistance;

    private float stateCooldown;
    private Vector3 center;
    private Vector3 currentWaypoint;
    private MoveState moveState;
    private GameObject target;
    private ProjectileAttack attack;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentWaypoint = center = transform.position;
        attack = GetComponent<ProjectileAttack>();

        // Randomize the enemy starts
        stateCooldown = UnityEngine.Random.Range(0f, idleCooldown);
    }

	// Update is called once per frame
	private void Update () 
    {
        stateCooldown -= Time.deltaTime;
        if(stateCooldown <= 0f)
        {
            IncrementState();
        }

        // Try to find the evil human
        if(target == null)
        {
            target = Game.instance.Player;
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
                    anim.SetTrigger("attack");
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
        return Vector3.Distance(transform.position, currentWaypoint) < dodgeCompleteDistance;
    }

    private void IncrementState()
    {
        switch(moveState)
        {
            case MoveState.Idle:
                moveState = MoveState.Dodge;
                // If we aren't leashed to our origin, reset the start position
                if(canRecenterOnDodge)
                {
                    center = transform.position;
                }

                currentWaypoint = ChooseDodgePosition();
                stateCooldown = dodgeCooldown;
                break;
            case MoveState.Attack:
                moveState = MoveState.Idle;
                stateCooldown = idleCooldown;
                break;
            case MoveState.Dodge:
                moveState = MoveState.Attack;
                stateCooldown = 0;
                break;
        }
    }
}
