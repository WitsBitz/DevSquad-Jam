using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private GameObject explosionFX;
	
	private Rigidbody rb;

	public int Damage{ get; set; }
	public float Speed{ get; set; }

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start () 
	{
		rb.AddForce(transform.forward * Speed * 5);
	}
	
	void OnCollisionEnter (Collision col) 
	{
	    explosionFX.transform.SetParent(null);
		explosionFX.transform.localScale = new Vector3(1,1,1);
        explosionFX.SetActive(true);

		if(col.gameObject.CompareTag("Enemy"))
		{
			if(col.gameObject.GetComponent<Health>() != null)
			{
				col.gameObject.GetComponent<Health>().TakeDamage(Damage);
			}
		}
		else
			Debug.Log("Hit " + col.gameObject.tag);
		Destroy(this.gameObject);
	}
	
}
