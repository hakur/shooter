using UnityEngine;
using System.Collections;
namespace Shooter{
public class Loading : MonoBehaviour {
	public static string levelName = "";
	
	void Start() {
		Application.LoadLevelAsync(levelName);
	}
}
}
