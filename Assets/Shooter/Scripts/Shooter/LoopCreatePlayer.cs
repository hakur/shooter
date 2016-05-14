using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
namespace Shooter{
public class LoopCreatePlayer : NetworkBehaviour {
	public NetworkManager netm;
	
	void Awake() {
		netm = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
	}
	void Start()
    {
        
        //CmdSpawn();
    }
	[Command]
	void CmdSpawn() {
		Debug.Log("复活");
		GameObject player = Instantiate(netm.playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(player);
	}
	public override void OnStartLocalPlayer()
     {
         base.OnStartLocalPlayer();
 Debug.Log("复活111111111111");
         // you can use this function to do things like create camera, audio listeners, etc.
         // so things which has to be done only for our player
     }
}
}
