using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody rb;

	public float Damage{ get; set; }
	public float Speed{ get; set; }

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start () 
	{
		rb.AddForce(transform.forward * Speed * 20);
	}
	
	void OnCollisionEnter (Collision col) 
	{
		Destroy(this.gameObject);
	}
}
