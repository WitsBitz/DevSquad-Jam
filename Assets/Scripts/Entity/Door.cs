using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Selectable {

	[SerializeField]
	private DestinationRoom destinationRoom;
	[SerializeField]
	private Vector3 spawnDestination;
	[SerializeField]
	private Vector3 spawnRotOFfset;
	private Animator anim;
	

	public bool Locked { get; set; }

	public override void Awake()
	{
		base.Awake();
		anim = GetComponent<Animator>();
	}

	public override void OnSelectEnter()
	{
		if(!Locked)
		{
			base.OnSelectEnter();
		}
	}

	public override void OnSelectExit()
	{
		if(!Locked)
		{
		base.OnSelectExit();
		}
	}

	public override void OnUse()
	{
		if(!Locked)
		{
			anim.SetTrigger("OpenDoor");
			Invoke("LoadScene", 1f);
		}
	}

	void LoadScene () {
		Game.instance.SpawnPosition = spawnDestination;
		Game.instance.SpawnRotOffset = spawnRotOFfset;
		SceneManager.LoadScene(destinationRoom.ToString());
	}
	
	void Update () {
		
	}
}

public enum DestinationRoom
{
	Start,
	Room1,
	Room2,
	Room3,
	Room4,
	Room5,
	Room6,
	BossRoom,
}
