using UnityEngine;
using System.Collections;
namespace Shooter{
public class GunFire : MonoBehaviour {
	private Weapon weapon;
	private float lastFireTime;
	private float lastDeRecoilTime;
	private RaycastHit hit;
	private BulletHole bulletHoleCom;
	private Vector2[] trajectory;
	public bool isAuto = true;
	private WeaponAnim anim;
	public GameObject throwThing;
	public WeaponManager manager;
	[Header("武器精度")]
	public bool reverse7 = false;
	public float spreadYShot = 0.008f; //每一枪Y轴高度增加
	public float recoil = 0f;
	public float deRecoil = 0.5f;
	public float addRecoil = 0.5f;
	public float recoilMax = 3f;
	public float recoilRangeStart = 0.01f;
	public float recoilRangeEnd = 0.9f;
	
	
	public float x ;
	public float y ;
	public float spreadY = 0.5f ;
	Vector3 pos;
	Vector3 dir;

	void Awake() {
		weapon = gameObject.GetComponent<Weapon>();
		bulletHoleCom = gameObject.GetComponent<BulletHole>();
		anim = gameObject.GetComponent<WeaponAnim>();
	}
	
	void OnEnable() {
		recoil = 0f;
		weapon.useSide = false;
	}
	
	void Start() {
		lastFireTime = -weapon.fireRate;
		lastDeRecoilTime = -weapon.fireRate;
	}
	
	void LateUpdate() {
		if(weapon.selected && ! weapon.isDrawing || weapon.netVar.isMinePlayer){ //被选为当前武器
			
			if(weapon.bulletsLeft < 1) {
				StartCoroutine(Reload());
			}
			if(Cursor.lockState == CursorLockMode.None) return; //非锁定鼠标的情况下不能射击  比如在使用UI
			
			if(isAuto) { //全自动开火模式
				if (Input.GetButton ("Fire")){
					Fire();
				} else {
					weapon.isFiring = false;	
				}
			} else { //非全自动需要一下一下开火, 半自动的狙击枪需要拉枪栓的动画
				if (Input.GetButtonDown ("Fire")){
					Fire();
				} else {
					weapon.isFiring = false;	
				}
			}
			if(!weapon.isFiring) {
				recoil -= deRecoil * Time.deltaTime;
				if(recoil < 0) {
					recoil = 0;
				}
			}
			if (Input.GetButtonDown ("Reload")){
				StartCoroutine(Reload());
			}
			if(recoil >= recoilMax) {
				weapon.useSide = true;
			} else {
				weapon.useSide = false;
			}
		}
	}
	
	public void Fire(){
		if(weapon.bulletsLeft < 1 && !weapon.isReloading && !weapon.isDrawing){ //子弹不足 非取出状态 非装弹状态  可以进入装弹逻辑
			StartCoroutine(Reload());
			return;
		}

		if ((Time.time > (weapon.fireRate + lastFireTime)) && !weapon.isLoading) { //当前时间-开火间隔 > 上一次开火时间  就表示可以开火 否则不能开火
			FireOneBullet();
		}
	}
	
	public void FireOneBullet (){
		if(weapon.isReloading || weapon.isDrawing) {
			return;
		}
		weapon.isFiring = true;
		lastFireTime = Time.time;
		if(!isAuto) {
			if(weapon.type == Weapon.types.sniper || weapon.type == Weapon.types.shotgun) {
				weapon.isLoading = true;
				weapon.useSide = false;
				anim.FireAnim(true);
			} else if(weapon.type == Weapon.types.grenade) {
				anim.FireAnim(false);
			}
		} else {
			anim.FireAnim(true);
		}
		
		x = y = 0;
		weapon.sideways();
		if(recoil >= recoilMax) {
			recoil = recoilMax;
			if(weapon.type != Weapon.types.sniper) {
				
				//达到最高后坐力 调整kickview的kickslideway
			}
			y = spreadYShot * (recoil - Random.Range(-spreadY,spreadY));
		} else {
			recoil += addRecoil * Random.Range(recoilRangeStart,recoilRangeEnd);
			y = spreadYShot * recoil;
		}
		
		
		if(weapon.type == Weapon.types.sniper && !weapon.isAiming) {
			x = Random.Range(-0.03f, 0.03f);
			y = Random.Range(-0.03f, 0.03f); //狙击枪没开镜随机弹道
		}
		
		//weapon.bulletsLeft--;
		pos = transform.parent.position;
		dir = gameObject.transform.parent.transform.TransformDirection(new Vector3(x,y,1));
		if (Physics.Raycast(pos, dir, out hit, weapon.range, weapon.layer)) {
			bulletHoleCom.AddHole(hit);
		}
		weapon.ApplyRecoil();
	}
	
	public IEnumerator Reload(){
		if(!weapon.isReloading && weapon.magazines > 0 && weapon.bulletsLeft != weapon.bulletsPerMag){
			weapon.isReloading = true;
			weapon.isFiring = false;
			anim.ReloadAnim();
			yield return new WaitForSeconds(anim.reloadTime);
			int need = weapon.bulletsPerMag - weapon.bulletsLeft;
			if(weapon.magazines - need > 0) {
				weapon.bulletsLeft += need;
				weapon.magazines -= need;
			} else {
				weapon.bulletsLeft += weapon.magazines;
				weapon.magazines = 0;
			}
			weapon.isReloading = false;
			yield return null;
		}
	}
}
}
