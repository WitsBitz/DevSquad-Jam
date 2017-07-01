using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	[SerializeField] private GameObject firePoint;
	[SerializeField] private GameObject hand;
	[SerializeField] private GameObject target;

	private Selectable selected;
	
	void Update () {
		RaycastHit hit;

		LayerMask layerMask = 1 << 8;

		if(Physics.Raycast(firePoint.transform.position, hand.transform.forward, out hit, 2, layerMask))
		{
			if(hit.transform.GetComponent<Selectable>() != null && selected == null)
			{
				Debug.Log("Hit Door");
				selected = hit.transform.GetComponent<Selectable>();
				selected.OnSelectEnter();
			}
		}
		else if(selected != null)
		{
			selected.OnSelectExit();
			selected = null;
		}

		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && selected != null)
		{	
			selected.OnUse();
		}

		if(Physics.Raycast(firePoint.transform.position, hand.transform.forward, out hit, Mathf.Infinity))
		{
			target.transform.position = hit.point;
		}
	}
}
