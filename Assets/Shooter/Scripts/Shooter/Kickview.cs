using UnityEngine;
using System.Collections;

public class Kickview : MonoBehaviour {
	public float returnSpeed = 2.0f;

	void LateUpdate () {
		//Quaternion.identity;
		//Quaternion ret = Quaternion.Euler(0,transform.localRotation.eulerAngles.y,0);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * returnSpeed);
	}
}