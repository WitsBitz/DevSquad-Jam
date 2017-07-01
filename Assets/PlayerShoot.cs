using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject bulletPrefab;

	[SerializeField] private GameObject torch;
	[SerializeField] private GameObject gun;

	public float projectileDamage = 1f;
	public float projectileSpeed = 1f;
	public float cooldown = 1f;

	private Bullet bullet;
	private float timeStamp;

	void Start () 
	{
		timeStamp = Time.time + cooldown;
	}
	
	void Update () 
	{
		
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown("space"))
		{
			if(gun.activeSelf)
			{
				if(timeStamp <= Time.time)
				{
					FireOneShot();
					timeStamp = Time.time + cooldown;
				}
			}
		}

		//Swipe Up
		if(OVRInput.Get(OVRInput.Button.Up) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			SwitchWeapons();
		}

		//Swipe Down
		if(OVRInput.Get(OVRInput.Button.Down) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			SwitchWeapons();
		}
	}

	void SwitchWeapons()
	{
		torch.SetActive(!torch.activeSelf);
		gun.SetActive(!gun.activeSelf);
	}

	void FireOneShot()
	{
		bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
		bullet.Damage = projectileSpeed;
		bullet.Speed = projectileSpeed;
	}
}
