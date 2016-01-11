using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle titleStyle;
	public GUIStyle buttonStyle;

	void OnGUI()
	{
		if (Application.loadedLevel == 0)
		{
			GUI.Label (new Rect ((Screen.width - 600) / 2, 40, 600, 300), "", titleStyle); 
			if (GUI.Button (new Rect ((Screen.width - 350) / 2, 450, 350, 60), "", buttonStyle))
			
				Application.LoadLevel(1);
			}
	}
}
