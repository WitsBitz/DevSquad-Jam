using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour {
	public float timeUntilDestroy = 4f;

	void Start () {
		Invoke("ExecuteUltimateDestroyMethod", timeUntilDestroy);
	}
	
	void ExecuteUltimateDestroyMethod()
	{
		Destroy(this.gameObject);
	}

}
