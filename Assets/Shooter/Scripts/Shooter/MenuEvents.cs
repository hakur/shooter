using UnityEngine;
using System.Collections;
namespace Shooter{
public class MenuEvents : MonoBehaviour {
	public NetManager netm;
	
	void Start() {
		netm = GameObject.FindWithTag("NetworkManager").GetComponent<NetManager>();
	}
	
	public void StartHost() {
		GlobalVars.joinGameType = GlobalVars.joinGameTypes.server;
		netm.StartHost();
	}
	
	public void JoinGame() {
		GlobalVars.joinGameType = GlobalVars.joinGameTypes.client;
		netm.StartClient();
	}
}
}
