using UnityEngine;
using UnityEngine;
using System.Collections;
namespace Shooter {
public class MapLoader : MonoBehaviour {
	public string scenePath;
	public void LoadMap() {
		Loading.levelName = scenePath;
		GlobalVars.currentMap = scenePath;
		Application.LoadLevelAsync("Shooter/Scenes/loading");
	}
}
}