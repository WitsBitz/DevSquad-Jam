﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour {

	[SerializeField] protected Renderer rend;
	[SerializeField] protected UnityEvent onUse;

	public virtual void Awake () {
		if(rend == null)
		{
			if(GetComponent<Renderer>() != null)
				rend = GetComponent<Renderer>();
			else
				rend = GetComponentInChildren<Renderer>();
		}
	}
	
	public virtual void OnSelectEnter()
	{
		Color baseColor = Color.white;
		float emission = .2f;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		rend.material.SetColor("_EmissionColor", finalColor);
	}

	public virtual void OnSelectExit()
	{
		Color baseColor = Color.white;
		float emission = 0;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		rend.material.SetColor("_EmissionColor", finalColor);
	}

	public virtual void OnSelect()
	{

	}

	public virtual void OnUse()
	{
        onUse.Invoke();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
