using UnityEngine;
using System.Collections;

public class Kickview : MonoBehaviour {
	public float returnSpeed = 2.0f;

	void LateUpdate () {
		transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * returnSpeed);
	}
}