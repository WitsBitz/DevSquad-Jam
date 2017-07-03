using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour {

	Health playerHealth;
	TextMeshPro tmp;
	void Start()
	{
		playerHealth = Game.instance.Player.GetComponent<Health>();
		tmp = GetComponent<TextMeshPro>();
	}

	void Update()
	{
		tmp.text = playerHealth.Value.ToString();
	}
}
