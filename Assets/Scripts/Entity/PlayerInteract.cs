using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	[SerializeField] private GameObject firePoint;
	[SerializeField] private GameObject hand;

	public Selectable Selected { get; private set; }
	
	void Update () {
		RaycastHit hit;

		LayerMask layerMask = 1 << 8;

		if(Physics.Raycast(firePoint.transform.position, hand.transform.forward, out hit, 2, layerMask))
		{
			if(hit.transform.GetComponent<Selectable>() != null && Selected == null)
			{
				Selected = hit.transform.GetComponent<Selectable>();
				Selected.OnSelectEnter();
			}
		}
		else if(Selected != null)
		{
			Selected.OnSelectExit();
			Selected = null;
		}

		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && Selected != null || Input.GetKeyDown(KeyCode.Space) && Selected != null)
		{	

			Selected.OnUse();
		}

	}
}
