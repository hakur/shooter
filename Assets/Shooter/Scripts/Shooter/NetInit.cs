using UnityEngine;
using System.Collections;
namespace Shooter{
public class NetInit : MonoBehaviour {
	public GameObject networkManagerPrefab;
	
	void Awake() {
		if(networkManagerPrefab != null) {
			GameObject netm = GameObject.FindWithTag("NetworkManager"); 
			if(netm == null) {
				Instantiate(networkManagerPrefab);
			}
		}
	}
}
}
