using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Shooter{
public class Aim : MonoBehaviour {
	public Sprite sniperScope;//狙击镜贴图
	[Header("右键瞄准的设置")]
	[Range(1f,60f)]
	public float mainCameraFOV = 60f; //点击瞄准时主摄像机FOV
	[Range(1f,60f)]
	public float weaponCameraFOV = 60f; //点击瞄准时主摄像机FOV
	public Vector3 pos; //瞄准时武器的transform.localPosition
	public Vector3 rot; //瞄准时武器的transform.localRotation
	[Range(0,1),Header("模型移动速度")]
	public float aimSpeed; //瞄准速度
	[Range(0,1),Header("镜头缩放速度")]
	public float fovSpeed; //fov减少速度
	[Range(0,1),Header("反后坐力比率")]
	public float antiRecoilRate; //反后坐力比率
	
	public GameObject aimHideModel; //瞄准的时候要隐藏的武器模型
	
	private float originMainCameraFov; //主摄像机原始FOV
	private float originWeaponCmeraFov; //武器摄像机原始FOV
	private Vector3 originPos; //原始的transform.localPosition
	private Vector3 originRot; //原始的transform.localRotation
	private Camera weaponCamera; //武器摄像机
	private Camera mainCamera; //主摄像机
	private Weapon weapon; //武器脚本
	[HideInInspector]
	public Image sniperScopeImg;
	[HideInInspector]
	public GameObject scopeObj;
	private int fovLevel = 0;
	private float sniperZeroLevelCameraFov;
	public int sniperFovLevelMax = 1;
	public float sniperFovLevelIncrease = 15f;
	[HideInInspector]
	public GameObject crosshair;
	void Awake() {
		weapon = GetComponent<Weapon>();
		weaponCamera = GameObject.FindWithTag("WeaponCamera").GetComponent<Camera>();
		mainCamera = Camera.main;
		originPos = transform.localPosition;
		originRot = new Vector3(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z);
		originMainCameraFov = mainCamera.fieldOfView;
		originWeaponCmeraFov = weaponCamera.fieldOfView;
		//抵消后坐力
		//weapon.recoilShotAim = weapon.recoilShot * antiRecoilRate;
		//weapon.recoilShotAimMax = weapon.recoilShotMax * antiRecoilRate;
		//if(weapon.type == Weapon.types.sniper) {
			//sniperScopeImg = GameObject.Find("SniperScopeShell").GetComponent<Image>();
			//scopeObj = GameObject.Find("SniperScope");
		//}
		//Debug.Log(GameObject.Find("SniperScopeShell"));
		//crosshair = GameObject.Find("CrossHair");
	}
	
	void OnEnable() {
		scopeObj.active = false;
		mainCamera.fieldOfView = 45f;
		weaponCamera.fieldOfView = 60f;
	}
	
	void Start() {
		if(weapon.type == Weapon.types.sniper) {
			//RectTransform rt = UICanvas.GetComponent (typeof (RectTransform)) as RectTransform;
			//rt.sizeDelta = new Vector2 (100, 100);
			RectTransform rt = sniperScopeImg.gameObject.GetComponent<RectTransform>();
			//rt.sizeDelta = new Vector2(Screen.width,Screen.width);
			sniperScopeImg.sprite  = sniperScope;
			
			Color c = sniperScopeImg.color;
			c.a = 255f;
			sniperScopeImg.color  = c;
			scopeObj.active = false;
			sniperZeroLevelCameraFov = mainCameraFOV;
		}
		//scopeObj = sniperScopeImg.gameObject;
	}
	
	void LateUpdate() {
		if(weapon.type == Weapon.types.sniper) {
			crosshair.active = false;
		} else {
			crosshair.active = true;
		}
		originRot = new Vector3(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z);
		if(Input.GetButton("Fire2") && !weapon.isReloading && !weapon.isLoading && !weapon.isDrawing) {
			Aiming();
			if(weapon.type == Weapon.types.sniper && Input.GetKeyDown("v")) { //狙击枪多倍镜头
				fovLevel++;
				if(fovLevel > sniperFovLevelMax) {
					fovLevel = 0;
				}
				mainCameraFOV -= fovLevel * sniperFovLevelIncrease;
				if(mainCameraFOV < 1) {
					mainCameraFOV = 1;
				}
				if(fovLevel == 0) {
					mainCameraFOV = sniperZeroLevelCameraFov;
				}
			}
		} else {
			DeAiming();
		}
	}
	
	void Aiming() {
		weapon.isAiming = true;
		mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView,mainCameraFOV,fovSpeed);
		weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView,weaponCameraFOV,fovSpeed);
		if(weapon.type == Weapon.types.sniper) {
			scopeObj.active = true;
			aimHideModel.SetActive(false);
		} else {
			Quaternion rotTo = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(rot.x,rot.y,rot.z),aimSpeed);
			Vector3 posTo = Vector3.Slerp(transform.localPosition,pos,aimSpeed);
			transform.localRotation = rotTo;
			transform.localPosition = posTo;
		}
	}
	
	void DeAiming() {
		weapon.isAiming = false;
		mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView,originMainCameraFov,fovSpeed);
		weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView,originWeaponCmeraFov,fovSpeed);
		if(weapon.type == Weapon.types.sniper) {
			scopeObj.active = false;
			aimHideModel.SetActive(true);
		} else {
			Quaternion rotTo = Quaternion.Slerp(transform.localRotation,Quaternion.Euler(originRot.x,originRot.y,originRot.z),aimSpeed);
			Vector3 posTo = Vector3.Slerp(transform.localPosition,originPos,aimSpeed);
			transform.localRotation = rotTo;
			transform.localPosition = posTo;
		}
	}
}
}
