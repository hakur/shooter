using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
namespace Shooter{
public class MapSync : NetworkBehaviour {
	[SyncVar] string currentMap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer) {
			
			CmdSyncMap();
			if(GlobalVars.joinGameType == GlobalVars.joinGameTypes.client) {
				Debug.Log(currentMap);
				if(currentMap != null && GlobalVars.currentMap != currentMap) {
					Debug.Log(currentMap);
					Loading.levelName = "Shooter/Scenes/map_otherMap";
					Application.LoadLevelAsync("Shooter/Scenes/loading");
				}
			}
		}
	}
	[Command]
	void CmdSyncMap() {
		if(isServer) {
			currentMap = GlobalVars.currentMap;
		}
	}
}
}
