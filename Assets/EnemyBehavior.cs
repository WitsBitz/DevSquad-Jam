using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

	private Renderer rend;
	private Health enemyHealth;
	private Animator anim;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		enemyHealth = GetComponent<Health>();
		rend = GetComponentInChildren<Renderer>();
	}

	public void TakeDamage()
	{
		StartCoroutine(FlashHurt());

		if(enemyHealth.Value <= 0)
		{
			Die();
		}
	}

	IEnumerator FlashHurt()
	{
		// Color normal = rend.material.color;
		// rend.material.color = Color.red;
		yield return new WaitForSeconds(.2f);
		// rend.material.color = normal;
	}

	void Die()
	{
		anim.SetTrigger("die");
		Game.instance.Enemies.Remove(this.gameObject);
		GetComponent<CapsuleCollider>().enabled = false;
		
		if(GetComponent<NavMeshAgent>() != null)
			GetComponent<NavMeshAgent>().enabled = false;

		MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour c in comps)
		{
			c.enabled = false;
		}
	}

	void Update () 
	{
		
	}
}
