using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Shooter{
[RequireComponent(typeof(Aim))]
[RequireComponent(typeof(WeaponAnim))]
[RequireComponent(typeof(GunFire))]
[RequireComponent(typeof(BulletHole))]
public class Weapon : MonoBehaviour {
	[HideInInspector]
	public bool isDrawing; //是否正在取出武器
	[HideInInspector]
	public bool isReloading; //是否正在换弹匣
	[HideInInspector]
	public bool isLoading; //是否正在拉枪栓 仅仅针对半自动枪械
	[HideInInspector]
	public bool isAiming; //是否正在瞄准
	[HideInInspector]
	public bool isWalking; //是否在走路
	[HideInInspector]
	public bool isRun; //是否在跑
	[HideInInspector]
	public bool isFiring; //是否正在开火
	[Header("角色控制器")]
	public CharacterController cc;
	
	public LayerMask layer;
	public enum types {rifle,sniper,pistol,smg,shotgun};
	public types type = types.rifle;
	
	[Header("武器精度设置")]
	[Header("武器基本配置")]
		[HideInInspector]
		public bool selected = false;
		public int damage = 50; //单发子弹伤害
		public int bulletsLeft = 30;
		public int bulletsPerMag = 30;
		public int magazines = 10; //弹匣数量
		public float fireRate = 0.046f; //连发子弹开火间隔时间
		public int range = 250;
	[Header("后坐力对相机的影响设置")]
		public Transform kickGO;
		public float kickUp = 0.5f; //上下晃动最大范围
		public float kickSideways = 0.5f; //左右晃动最大范围
	[Header("武器动画列表")]
	[Header("武器声音")]
		[HideInInspector]
		public AudioSource sound;
	
	private Text ammoFront;
	private Text ammoBack;
	
	void Awake() {
		sound = GameObject.Find("WeaponSound").GetComponent<AudioSource>();
		selected = false;
		ammoFront = GameObject.Find("AmmoFront").GetComponent<Text>();
		ammoBack = GameObject.Find("AmmoBack").GetComponent<Text>();
	}
	
	void Start() {
		magazines = magazines * bulletsPerMag;
	}
	
	public void init() {
		isReloading = false;
		isFiring = false;
		selected = true;
		isLoading = false;
	}
	
	void LateUpdate() {
		//ApplyRecoil();
		ammoFront.text = bulletsLeft.ToString();
		ammoBack.text = magazines.ToString();
		if(Mathf.Abs(cc.velocity.z) > 0.5f || Mathf.Abs(cc.velocity.x) > 0.5f) {
			isWalking = true;
		} else {
			isWalking = false;
		}
	}
	
	public void ApplyRecoil() {
		kickGO.localRotation = Quaternion.Euler(kickGO.localRotation.eulerAngles - new Vector3(kickUp, Random.Range(-kickSideways, kickSideways), 0));
	}
}
}
