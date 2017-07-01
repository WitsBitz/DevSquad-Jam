using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour 
{
    [SerializeField] private GameObject attackOrigin;
    [SerializeField] private GameObject projectilePrefab;

    public void ShootAt(GameObject target)
    {
        if(target != null)
        {
            if(projectilePrefab != null && attackOrigin != null)
            {
                var projectileObj = Instantiate(projectilePrefab, 
                    attackOrigin.transform.position, attackOrigin.transform.rotation);
            }
        }

    }
}
