using UnityEngine;
using System.Collections;
namespace Shooter {
public class Status : MonoBehaviour {
	public bool grounded = false; //是否落地
	public bool canRun = false; //是否可以跑
	public bool running = false; //是否正在奔跑
	public bool canJump = false; //是否可以跳跃
	public enum states {stand,crouch,jump}; //状态列表
	public states state = states.stand; //默认站立状态
	public bool jumping = false;
	
	private CharacterController cc; //角色控制器
	
	void Awake() {
		cc = GetComponent<CharacterController>();
	}
	
	void Start() {
		
	}
	
	void Update() {
		if(grounded) { //落地
			canRun = true; //注意检查是否在管道里面
			canJump = true;
			state = states.stand;
		} else {
			canJump = false;
			canRun = false;
			state = states.jump;
		}
	}
}
}