using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DebugHealth : MonoBehaviour 
{
    [SerializeField] private Health[] units;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

	private void Update () 
    {
        string newText = "";
        foreach(var unit in units)
        {
            newText += unit.gameObject.name + ": " + unit.Value + "/" + unit.MaxHealth + "\n";
        }
        text.text = newText;
    }
}
