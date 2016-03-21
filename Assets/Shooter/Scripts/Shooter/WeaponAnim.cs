using UnityEngine;
using System.Collections;
namespace Shooter{
public class WeaponAnim : MonoBehaviour {
	//[Header("声音设置")]
	//	public AudioClip soundDraw; //取出枪的时候播放的声音
	//	public AudioClip soundFire; //开火的声音
	//	public AudioClip soundReload; //换弹匣的声音
	//	public AudioClip soundEmpty; //没子弹的声音
	//	public AudioClip soundLoad; //半自动枪械拉栓的声音
	//	public AudioClip switchModeSound; //切换开火模式的声音
		
	[Header("动画设置")]
		public string fireAnim = "_fire"; //开火动画--------
		[Range(0.0f, 5.0f)]
		public float fireAnimSpeed = 1f; //
		
		public string drawAnim = "_draw"; //取出动画---------
		[Range(0.0f, 5.0f)]
		public float drawAnimSpeed = 1f; //
		//[Range(0.0f, 5.0f)]
		[HideInInspector]
		public float drawTime = 1.5f; //
		
		public string reloadAnim = "_reload"; //换弹匣动画----------
		[Range(0.0f, 5.0f)]
		public float reloadAnimSpeed  = 1f; //
		//[Range(0.0f, 5.0f)]
		[HideInInspector]
		public float reloadTime = 1.5f; //
		
		public string idleAnim = "_idle"; //空闲动画
		[Range(0f,5f)]
		public float idleAnimSpeed = 1f; //
		
		public string walkAnim = "_walk"; //走路动画
		[Range(0f,5f)]
		public float walkAnimSpeed = 1f;
		
		public string runAnim = "_run"; //跑步动画
		[Range(0f,5f)]
		public float runAnimSpeed = 1f;
		
		public string loadAnim = "_load"; //半自动枪械拉枪栓动画
		[Range(0f,5f)]
		public float loadAnimSpeed = 1f;
		[HideInInspector]
		public float loadTime = 0.3f;
	
	private Weapon weapon;
	public Animation anim;
	
	void Awake() {
		weapon = GetComponent<Weapon>();
	}
	void Start() {
		if(anim[reloadAnim] != null) {
			reloadTime = anim[reloadAnim].length / reloadAnimSpeed;
		}
		
		if(anim[drawAnim] != null) {
			drawTime = anim[drawAnim].length / drawAnimSpeed;
		}
		
		if(anim[loadAnim] != null) {
			loadTime = anim[loadAnim].length / loadAnimSpeed;
		}
	}
	
	void LateUpdate() {
		IdleAnim();
	}
	
	public void IdleAnim() {
		if(!weapon.isDrawing && !weapon.isWalking && !weapon.isFiring && !anim.isPlaying && anim[idleAnim] != null) {
			anim[idleAnim].speed = idleAnimSpeed;
			anim.Rewind(idleAnim);
			anim.Play(idleAnim);
		} else {
			WalkRunAnim();
		}
	}
	
	public void WalkRunAnim() {
		if(weapon.isWalking && !anim.isPlaying && anim[walkAnim] != null) {
			anim[walkAnim].speed = walkAnimSpeed;
			anim.Rewind(walkAnim);
			anim.Play(walkAnim);
		} else if(weapon.isRun && !anim.isPlaying && anim[runAnim] != null) {
			anim[runAnim].speed = runAnimSpeed;
			anim.Rewind(runAnim);
			anim.Play(runAnim);
		}
	}
	
	public void FireAnim(bool playLoadAnim = false) {
		if(weapon.isFiring && anim[fireAnim] != null) {
			anim[fireAnim].speed = fireAnimSpeed;
			anim.Rewind(fireAnim);
			anim.Play(fireAnim);
			if(playLoadAnim) {
				StartCoroutine(LoadAnim(anim[fireAnim].length));
			}
		}
	}
	
	IEnumerator LoadAnim(float clipLenth) {
		if(anim[loadAnim] != null) {
			yield return new WaitForSeconds(clipLenth);
			if(!weapon.isReloading) {
				StartCoroutine(setLoadOk(anim[loadAnim].length));
				anim[loadAnim].speed = loadAnimSpeed;
				anim.Rewind(loadAnim);
				anim.PlayQueued(loadAnim);
				//weapon.sound.clip = soundLoad;
				//weapon.sound.Play();
			}
		}
	}
	
	public IEnumerator setLoadOk(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		weapon.isLoading = false;
	}
	
	public void ReloadAnim() {
		weapon.isLoading = false;//半自动枪械已加载
		if(weapon.isReloading && anim[reloadAnim] != null) {
			StartCoroutine(setReloadOk(reloadTime));
			anim[reloadAnim].speed = reloadAnimSpeed;
			anim.Rewind(reloadAnim);
			anim.Play(reloadAnim,PlayMode.StopAll);
			
		}
	}
	
	public void DrawAnim() {
		weapon.isDrawing = true;
		//weapon.sound.clip = soundDraw;
		//weapon.sound.Play();
		if(anim[drawAnim] != null) {
			anim[drawAnim].speed = drawAnimSpeed;
			anim.Rewind(drawAnim);
			anim.Play(drawAnim,PlayMode.StopAll);
		}
		StartCoroutine(setDrawOk());
	}
	
	IEnumerator setDrawOk() {
		yield return new WaitForSeconds(drawTime);
		weapon.isDrawing = false;
	}
	
	IEnumerator setReloadOk(float time) {
		yield return new WaitForSeconds(time);
		weapon.isReloading = false;
	}
}
}
