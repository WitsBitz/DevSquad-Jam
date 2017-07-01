using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour 
{
    [SerializeField] private GameObject attackOrigin;
    [SerializeField] private float rotationSpeed;
    private GameObject target;

    // Weapon details
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown;
    private float currentCooldown;

	private void Update () 
    {
        // Try to find the evil human
        if(target == null)
        {
            target = GameObject.Find("OVRPlayerController");
        }

        TurnTowardsTarget(target);
        ShootAt(target);
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


    private void ShootAt(GameObject target)
    {
        currentCooldown -= Time.deltaTime;
        if(target != null && currentCooldown <= 0)
        {
            currentCooldown = cooldown;

            if(projectilePrefab != null && attackOrigin != null)
            {
                var projectileObj = Instantiate(projectilePrefab, 
                    attackOrigin.transform.position, attackOrigin.transform.rotation);
            }
        }

    }
}
