using UnityEngine;
using System.Collections;

namespace Shooter{
public class ThirdPersonAnim : MonoBehaviour {
	public Animation anim;
	//public Animator amt;
	public NetworkSync netVar;
	private Vector3 lastPos;

	void Awake() {
		//amt = GetComponent<Animator>();
	}
	
	void Update () {
		Vector3 pos = transform.position;
		if(pos.x - lastPos.x > 0 || pos.z - lastPos.z > 0 && !netVar.jumping) {
			anim.Play("urban_run",PlayMode.StopAll);
			Debug.Log("run");
		} else if(netVar.syncJumping) {
			anim.Play("urban_jump",PlayMode.StopAll);
			Debug.Log("jump");
		}
	}
	
	void LateUpdate() {
		lastPos = transform.position;
	}
}
}
