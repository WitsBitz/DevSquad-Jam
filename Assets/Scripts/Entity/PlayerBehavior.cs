using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	Health playerHealth;
	void Awake()
	{
		playerHealth = GetComponent<Health>();
	}

	public void TakeDamage()
	{
		Debug.Log("Ouch!");
		if(GetComponent<Health>().Value <= 0)
		{
			PlayerDeath();
		}
	}

	void PlayerDeath()
	{
		Game.instance.LoadScene("Start");
		playerHealth.ResetHealth();
	}
}
