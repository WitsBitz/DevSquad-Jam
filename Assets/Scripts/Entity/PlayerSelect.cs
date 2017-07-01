using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour {

	[SerializeField] private GameObject hand;

	void Start () {
		
	}
	
	void Update () {
		Debug.DrawLine(hand.transform.position, hand.transform.position + Vector3.forward, Color.red);
	}
}
