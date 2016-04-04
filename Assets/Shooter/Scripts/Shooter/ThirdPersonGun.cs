using UnityEngine;
using System.Collections;
namespace Shooter{
public class ThirdPersonGun : MonoBehaviour {
	public NetworkSync netVar;
	
	void LateUpdate() {
		if(!netVar.isMinePlayer) {
			transform.localRotation = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(netVar.snycRotY,0,0),netVar.lerpSpeed * Time.deltaTime);
		}
	}
}
}
