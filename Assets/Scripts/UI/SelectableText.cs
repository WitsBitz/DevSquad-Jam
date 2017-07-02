using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectableText : Selectable 
{
    [SerializeField] private Color highlightColor = Color.white;
    private Color originalColor;
    private TextMeshProUGUI text;

    public override void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(text != null)
        {
            originalColor = text.color;
        }
    }

    public override void OnSelectEnter()
	{
        if(text != null)
        {
            text.color = highlightColor;
        }
	}

	public override void OnSelectExit()
	{
        if(text != null)
        {
            text.color = originalColor;
        }
	}
}
