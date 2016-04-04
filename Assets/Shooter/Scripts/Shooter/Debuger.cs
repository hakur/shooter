using UnityEngine;
using System.Collections;
namespace Shooter {
public class Debuger : MonoBehaviour {
	void Awake() {
		Application.targetFrameRate = 3000;
	}

	void Update () {
		lockMouse();
	}
	
	void lockMouse () {
		if (Input.GetKeyDown("escape")) {
			//Screen.lockCursor = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	
	void OnGUI()
    {
        if(GUI.Button(new Rect(3,3,100,30), "锁定屏幕"))  
        {
           //Screen.lockCursor = true;
		   Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
}