using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveAndAttack))]
[RequireComponent(typeof(ApproachAndAttack))]
public class BossMovement : MonoBehaviour 
{
    private enum BossMode
    {
        Ranged,
        Melee
    }

    [SerializeField] private float switchCooldown;
    
    private MoveAndAttack ranged;
    private ApproachAndAttack melee;
    private float currentSwitchCooldown;
    private BossMode mode;

    void Start () 
    {
        ranged = GetComponent<MoveAndAttack>();
        melee = GetComponent<ApproachAndAttack>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        currentSwitchCooldown -= Time.deltaTime;
        if(currentSwitchCooldown <= 0f)
        {
            currentSwitchCooldown = switchCooldown;

            switch(mode)
            {
                case BossMode.Melee:
                    mode = BossMode.Ranged;
                    ranged.enabled = true;
                    melee.enabled = false;
                    break;
                case BossMode.Ranged:
                    mode = BossMode.Melee;
                    ranged.enabled = false;
                    melee.enabled = true;
                    break;
            }

        }
    }
}
