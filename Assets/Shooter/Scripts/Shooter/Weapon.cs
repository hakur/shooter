using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Shooter{
[RequireComponent(typeof(Aim))]
[RequireComponent(typeof(WeaponAnim))]
[RequireComponent(typeof(BulletHole))]
public class Weapon : MonoBehaviour {
	[HideInInspector]
	public bool isDrawing; //是否正在取出武器 is taking out weapon
	[HideInInspector]
	public bool isReloading; //是否正在换弹匣 is reloding
	[HideInInspector]
	public bool isLoading; //是否正在拉枪栓 仅仅针对半自动枪械 semi-auto weapon need reload
	[HideInInspector]
	public bool isAiming; //是否正在瞄准 is aiming
	[HideInInspector]
	public bool isWalking; //是否在走路 is moving
	[HideInInspector]
	public bool isRun; //是否在跑 is running
	[HideInInspector]
	public bool isFiring; //是否正在开火 is firing
	[Header("角色控制器")]
	[HideInInspector]
	public CharacterController cc;
	
	public LayerMask layer;
	public enum types {rifle,sniper,pistol,smg,shotgun,grenade};
	public types type = types.rifle;
	[HideInInspector]
	public int slotIndex = 0;
	public float weight = 1f;
	[HideInInspector]
	public NetworkSync netVar;
	
	[Header("武器精度设置")]
	[Header("武器基本配置")]
		[HideInInspector]
		public bool selected = false;
		public int damage = 50; //单发子弹伤害 damage per bullet
		public int bulletsLeft = 30;
		public int bulletsPerMag = 30;
		public int magazines = 10; //弹匣数量 magazines num   will using in start()
		public float fireRate = 0.046f; //连发子弹开火间隔时间 time between two shot
		public int range = 250;
	[Header("后坐力对相机的影响设置")]
		public Transform kickGO;
		public float kickUp = 0.5f; //上下晃动最大范围 world camera kick up (rotation)
		public float kickSideways = 0.5f; //左右晃动最大范围 world camera kick left or right (rotation)
	[Header("武器动画列表")]
	[Header("武器声音")]
		[HideInInspector]
		public AudioSource sound;
	
	public Text ammoFront;
	public Text ammoBack;
	
	void Awake() {
		selected = false;
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
		if(netVar.isMinePlayer) {
			//ApplyRecoil();
			ammoFront.text = bulletsLeft.ToString();
			ammoBack.text = magazines.ToString();
			if(Mathf.Abs(cc.velocity.z) > 0.5f || Mathf.Abs(cc.velocity.x) > 0.5f) {
				isWalking = true;
			} else {
				isWalking = false;
			}
		}
	}
	
	public void ApplyRecoil() {
		kickGO.localRotation = Quaternion.Euler(kickGO.localRotation.eulerAngles - new Vector3(kickUp, Random.Range(-kickSideways, kickSideways), 0));
	}
	
	public void GrenadeRemoveCoreComponent() {
		Destroy(GetComponent<GunFire>());
	}
}
}
