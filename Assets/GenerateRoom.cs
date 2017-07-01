using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour {
	[SerializeField] private GameObject ground;
	[SerializeField] private GameObject wall;

	Transform spawnedRoom;

	void Start () {
		SpawnRoom();
	}
	
	void SpawnRoom()
	{
		// Room
		spawnedRoom = Instantiate(ground, Vector3.zero, Quaternion.identity).transform;
		//North Wall
		Instantiate(wall, new Vector3(0, 2.5f, 5), Quaternion.identity).transform.SetParent(spawnedRoom.transform);
		//South Wall
		Instantiate(wall, new Vector3(0, 2.5f, -5), Quaternion.identity).transform.SetParent(spawnedRoom.transform);
		//West Wall
		Instantiate(wall, new Vector3(-5, 2.5f, 0), Quaternion.Euler(0,90,0)).transform.SetParent(spawnedRoom.transform);
		//East Wall
		Instantiate(wall, new Vector3(5, 2.5f, 0), Quaternion.Euler(0,-90,0)).transform.SetParent(spawnedRoom.transform);		
	}
}
