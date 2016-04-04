using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
namespace Shooter {
public class ShooterInit : NetworkBehaviour {
	public Camera worldCamera;
	public Camera weaponCamera;
	public Camera UICamera;
	public GameObject FirstPersonPlayer;
	public GameObject HUD;
	public GameObject ThrowThing;
	public GameObject ThirdPersonPlayer;
	void Awake() {
		
	}
	
	void Start() {
		if(isLocalPlayer) {
			Destroy(ThirdPersonPlayer);
		} else {
			Destroy(FirstPersonPlayer);
			Destroy(UICamera.gameObject);
			Destroy(ThrowThing);
			Destroy(HUD);
		}
	}
}
}
