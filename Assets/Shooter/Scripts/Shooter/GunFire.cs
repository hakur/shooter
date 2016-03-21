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
		trajectory = new Vector2[7];
		trajectory[0] = Vector2.zero;
		trajectory[1] = new Vector2(0,0.00001f);
		trajectory[2] = new Vector2(0,0.01f);
		trajectory[3] = new Vector2(0,0.02f);
		trajectory[4] = new Vector2(0.01f,0.032f);
		trajectory[5] = new Vector2(0.025f,0.046f);
		trajectory[6] = new Vector2(0.04f,0.064f);
	}
	
	void LateUpdate() {
		if(weapon.selected && ! weapon.isDrawing){ //被选为当前武器
			
			if(weapon.bulletsLeft < 1) {
				StartCoroutine(Reload());
			}
			if(Cursor.lockState == CursorLockMode.None) return; //非锁定鼠标的情况下不能射击  比如在使用UI
			
			if(isAuto) { //全自动开火模式
				if (Input.GetButton ("Fire")){
					Fire();
				} else {
					weapon.isFiring = false;	
					//if(weapon.fpscontroller.controller.velocity.magnitude < (weapon.fpscontroller.walkSpeed / 2)) {
					//	weapon.DeRecoil();
					//}
				}
			} else { //非全自动需要一下一下开火, 半自动的狙击枪需要拉枪栓的动画
				if (Input.GetButtonDown ("Fire")){
					Fire();
				} else {
					weapon.isFiring = false;	
					//if(weapon.fpscontroller.controller.velocity.magnitude < (weapon.fpscontroller.walkSpeed / 2)) {
					//	weapon.DeRecoil();
					//}
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
			//StartCoroutine(weapon.setLoadOk(weapon.fireRate));
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
			}
		}
		//在这里加载弹道文件
		//Vector3 dir = gameObject.transform.TransformDirection(new Vector3(Random.Range(-0.01f, 0.01f) * weapon.recoil, Random.Range(-0.01f, 0.01f) * weapon.recoil,1));
		pos = transform.parent.position;
		//weapon.AddRecoil();//增加后坐力
		
		if(weapon.type == Weapon.types.sniper && !weapon.isAiming) {
			x = Random.Range(-0.09f, 0.09f);
			y = Random.Range(-0.09f, 0.09f); //狙击枪没开镜随机弹道
		} else {
			//if(weapon.fireBulletCount < trajectory.Length) {
			//	x = trajectory[Mathf.FloorToInt(weapon.fireBulletCount)].x;
			//	y = trajectory[Mathf.FloorToInt(weapon.fireBulletCount)].y;
			//} else {
			//	x = trajectory[trajectory.Length-1].x + Random.Range(-0.08f, 0f);
			//	y = trajectory[trajectory.Length-1].y + Random.Range(-0.005f, 0.003f);
			//}
		}
		x = 0;
		y = 0;
		

		dir = gameObject.transform.parent.transform.TransformDirection(new Vector3(x,y,1));
		//Debug.DrawLine(pos,dir,Color.red,250);
		if (Physics.Raycast(pos, dir, out hit, weapon.range, weapon.layer)) {
			//if (hit.rigidbody) //被击中物体 要被击飞
			//	hit.rigidbody.AddForceAtPosition(weapon.force * dir, hit.point);
			
			bulletHoleCom.AddHole(hit);
		}
		
		//weapon.sound.PlayOneShot(anim.soundFire);
		//weapon.m_LastFrameShot = Time.frameCount;
		if(isAuto) {
			anim.FireAnim(false);
		} else {
			anim.FireAnim(true);
		}
		weapon.ApplyRecoil();
		
		//weapon.fireBulletCount++;
		weapon.bulletsLeft--;
	}
	
	public IEnumerator Reload(){
		if(!weapon.isReloading && weapon.magazines > 0 && weapon.bulletsLeft != weapon.bulletsPerMag){
			weapon.isReloading = true;
			weapon.isFiring = false;
			//weapon.recoil = 0f; //换弹匣后坐力归零
			anim.ReloadAnim();
			//weapon.sound.PlayOneShot(anim.soundReload);
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
