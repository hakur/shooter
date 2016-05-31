using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
namespace Shooter {
public class NetworkSync : NetworkBehaviour {
	public bool isMinePlayer;
	public float lerpSpeed = 15f;
	
	//同步坐标
	public Vector3 playerPos = Vector3.zero;
	private Vector3 playerLastPos;
	private float minSyncDistance = 0.05f;
	[HideInInspector] [SyncVar] public Vector3 snycPos = Vector3.zero;
	
	//同步旋转
	private float minSyncRotation = 0.5f;
	[HideInInspector]
	public float playerRotX;
	private float playerRotLastX;
	[HideInInspector]
	public float playerRotY;
	private float playerRotLastY;
	[HideInInspector] [SyncVar] public float snycRotX;
	[HideInInspector] [SyncVar] public float snycRotY;
	
	//同步跳跃
	public bool jumping;
	[HideInInspector] [SyncVar] public bool syncJumping;
	
	void Start() {
		isMinePlayer = isLocalPlayer;
		GlobalVars.isLocalPlayer = isLocalPlayer;
	}
	
	void LateUpdate() {
		if(!isLocalPlayer) {
			transform.position = Vector3.Slerp(transform.position,snycPos,lerpSpeed * Time.deltaTime);
			Quaternion playerRot = Quaternion.Euler(0,snycRotX,0);
			transform.rotation = Quaternion.Slerp(transform.rotation,playerRot,lerpSpeed * Time.deltaTime);
		} else {
			if(Vector3.Distance(playerPos,playerLastPos) > minSyncDistance) {
				CmdSyncPos(playerPos);
			}
			if((Mathf.Abs(playerRotX-playerRotLastX)>minSyncRotation) || (Mathf.Abs(playerRotX-playerRotLastY)>minSyncRotation)) {
				CmdSyncRotXY(playerRotX,playerRotY);
			}
		}
		
		syncJumping = jumping;
	}
	
	[Command]
	void CmdSyncPos(Vector3 myPos) {
		snycPos = myPos;
		playerLastPos = playerPos;
	}
	[Command]
	void CmdSyncRotXY(float x,float y) {
		snycRotX = x;
		snycRotY = y;
		playerRotLastX = playerRotX;
		playerRotLastY = playerRotY;
	}
}
}
