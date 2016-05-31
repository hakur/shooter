using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
namespace Shooter{
public class LoopCreatePlayer : MonoBehaviour {
	public NetManager netm;
	public bool showPlayer=true;
	void Awake() {
		netm = GameObject.FindWithTag("NetworkManager").GetComponent<NetManager>();
	}
	void Start()
    {
        netm.RespawnPlayer(showPlayer);
    }
}
}
