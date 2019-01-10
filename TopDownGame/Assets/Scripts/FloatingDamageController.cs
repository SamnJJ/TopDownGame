using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageController : MonoBehaviour {
    private static FloatingDamage popupText;
    private static GameObject canvas;

	// Use this for initialization
	public static void Initialize(FloatingDamage prefab)
    {
        canvas = GameObject.Find("WorldSpaceCanvas");
        if (!popupText)
        {
            popupText = prefab;
        }
	}
	
    public static void CreateFloatingText(string text, Vector3 location, Color textColor)
    {
        FloatingDamage instance = Instantiate(popupText, canvas.transform,false);
        //Vector3 textLocation = Camera.main.WorldToScreenPoint(location);
        //textLocation.y += 0.05f;
        instance.transform.position = location;
        instance.SetText(text);
        instance.SetTextColor(textColor);
    }

}
