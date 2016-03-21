using UnityEngine;
using System.Collections;
namespace Shooter {
public class Move : MonoBehaviour {
	private float inputX = 0f;
	private float inputY = 0f;
	private float inputModifyFactor = 0f; //同时朝着两个方向移动的减速率
	private CharacterController cc;
	private float rayDistance = 0f;
	private RaycastHit hit;
	private Status status;
	private Vector3 moveDirection;
	private float antiGravity = -1f;
	private float originSpeed = 0f;
	
	public Transform fallEffect;
	public Transform fallEffectWep;
	public float baseGravity = 24f;
	private float gravity = 24f;
	public float speed = 4f;
	public int state; // 0站立 1蹲下 2匍匐
	private float normalHeight = 0.9f;
	private float crouchHeight = 0.2f;
	public Transform cameraGO;
	public float jumpSpeed  = 8.0f;
	private float jumpTimer;
	public float ccNormalHeight = 1.8f;
	public float ccCrouchHeight = 1.2f;
	private float fallDistance = 0;
	private Vector3 lastPosition;
	
	private bool usingFallEffect;
	
	private float highestPoint;
	[HideInInspector]
	public bool isFalling;
	
	void Awake() {
		cc = GetComponent<CharacterController>();
		status = GetComponent<Status>();
		originSpeed = speed;
	}
	
	void Start() {
		rayDistance = cc.height / 2 + 0.1f;
		gravity = baseGravity;
	}
	
	void Update () {
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		//inputModifyFactor = (inputX != 0.0f && inputY != 0.0f)? 0.7071f : 1.0f;
		//inputModifyFactor = 1;
		
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance)){ //向下方发射一条射线
			//Debug.Log(hit.collider.gameObject);
		}
	}
	
	void FixedUpdate() {
		stateCheck(); //检查蹲伏状态
		if(usingFallEffect) {
			fallEffect.localPosition = Vector3.Slerp(fallEffect.localPosition,new Vector3(fallEffect.localPosition.x,-0.2f,fallEffect.localPosition.z),Time.deltaTime * 10f);
		} else {
			fallEffect.localPosition = Vector3.Slerp(fallEffect.localPosition,new Vector3(fallEffect.localPosition.x,0f,fallEffect.localPosition.z),Time.deltaTime * 10f);
		}
	}
	
	void LateUpdate() {
		move();
		fallCheck();
		lastPosition = transform.position;
	}

	void move() {
		if(moveDirection.y > -jumpSpeed) {
			moveDirection.y += Physics.gravity.y * Time.deltaTime * gravity;
		}
		if(Cursor.lockState == CursorLockMode.Locked) {
			moveDirection.x = inputX * speed;
			moveDirection.z = inputY * speed;
			if(cc.isGrounded == false) {
				//moveDirection.y += Physics.gravity.y * Time.deltaTime;
			}

			if (Input.GetButtonDown("Jump") && (cc.isGrounded == true)) {
				moveDirection.y = jumpSpeed;
			}
		}
		moveDirection = transform.TransformDirection(moveDirection);
		status.grounded = (cc.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
	}
	
	void stateCheck() {
		if(Input.GetKey("left ctrl")) {
			status.state = Status.states.crouch;
			cc.height = ccCrouchHeight;
			speed = originSpeed / 2;
			if(cameraGO.localPosition.y != crouchHeight){
				if(cameraGO.localPosition.y > crouchHeight){
					cameraGO.localPosition = new Vector3(cameraGO.localPosition.x,cameraGO.localPosition.y - Time.deltaTime * 6,cameraGO.localPosition.z);
				}
				if(cameraGO.localPosition.y < crouchHeight){
					cameraGO.localPosition = new Vector3(cameraGO.localPosition.x,cameraGO.localPosition.y + Time.deltaTime * 6,cameraGO.localPosition.z);
				}
				
			}
		} else {
			status.state = Status.states.stand;
			cc.height = ccNormalHeight;
			speed = originSpeed;
			if(cameraGO.localPosition.y > normalHeight){
				cameraGO.localPosition = new Vector3(cameraGO.localPosition.x,normalHeight,cameraGO.localPosition.z);
			} else if(cameraGO.localPosition.y < normalHeight){
				cameraGO.localPosition = new Vector3(cameraGO.localPosition.x,cameraGO.localPosition.y + Time.deltaTime * 6,cameraGO.localPosition.z);
			}
		}
	}
	
	
	void fallCheck() {
		if(transform.position.y > lastPosition.y){
			highestPoint = transform.position.y;
			isFalling = true;
		}
		
		if (cc.isGrounded) {
			isFalling = false;
			fallDistance = highestPoint - transform.position.y;
			if(fallDistance > 1){
				highestPoint = 0f;
				fallDistance = 0f;
				StartCoroutine(FallEffect());
			}
		}
	}
	
	IEnumerator FallEffect() {
		usingFallEffect = true;
		yield return new WaitForSeconds(0.15f);
		usingFallEffect = false;
	}
}
}