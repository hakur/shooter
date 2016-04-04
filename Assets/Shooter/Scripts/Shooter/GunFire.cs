using UnityEngine;
using System.Collections;
namespace Shooter{
public class GunFire : MonoBehaviour {
	private Weapon weapon;
	private float lastFireTime;
	private RaycastHit hit;
	private BulletHole bulletHoleCom;
	private Vector2[] trajectory;
	public bool isAuto = true;
	private WeaponAnim anim;
	public GameObject throwThing;
	public WeaponManager manager;
	
	
	float x ;
	float y ;
	Vector3 pos;
	Vector3 dir;

	void Awake() {
		weapon = gameObject.GetComponent<Weapon>();
		bulletHoleCom = gameObject.GetComponent<BulletHole>();
		anim = gameObject.GetComponent<WeaponAnim>();
	}
	
	void Start() {
		lastFireTime = -weapon.fireRate;
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
			
			if (Input.GetButtonDown ("Reload")){
				StartCoroutine(Reload());
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
				anim.FireAnim(true);
			} else if(weapon.type == Weapon.types.grenade) {
				anim.FireAnim(false);
			}
		} else {
			anim.FireAnim(true);
		}
		
		weapon.bulletsLeft--;
		pos = transform.parent.position;
		
		if(weapon.type == Weapon.types.sniper && !weapon.isAiming) {
			x = Random.Range(-0.09f, 0.09f);
			y = Random.Range(-0.09f, 0.09f); //狙击枪没开镜随机弹道
		}
		x = 0;
		y = 0;
		

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
