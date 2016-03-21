using UnityEngine;
using System.Collections;
namespace Shooter{
public class BulletHole : MonoBehaviour {
[Header("子弹打在物体上的弹孔")]
//弹孔是public的  也许你会拥有一把步枪和闪电枪  两者弹孔肯定不一样
	[HideInInspector]
	public Object Concrete; //
	[HideInInspector]
	public Object Wood; //打在tag为wood木头上的弹孔
	[HideInInspector]
	public Object Metal; //打在tag为metal金属上的弹孔
	[HideInInspector]
	public Object Dirt; //打在tag为dirt脏东西上的弹孔
	[HideInInspector]
	public Object Blood; //打在tag为blood血浆上的弹孔
	[HideInInspector]
	public Object Water; //打在tag为water水体上的弹孔
	[HideInInspector]
	public Object Untagged; //打在tag为Untagged上
	[HideInInspector]
	private Weapon weaponCom;

	void Awake () {
		/*Concrete = Resources.Load("ParticleEffects/particles/BullrtHits/particle_concrete");
		Wood = Resources.Load("ParticleEffects/particles/BullrtHits/particle_wood2");
		Metal = Resources.Load("ParticleEffects/particles/BullrtHits/particle_metal");
		Dirt = Resources.Load("ParticleEffects/particles/BullrtHits/particle_dirt");
		Blood = Resources.Load("ParticleEffects/particles/BullrtHits/particle_body");
		Water = Resources.Load("ParticleEffects/particles/BullrtHits/particle_concrete");
		Untagged = Resources.Load("ParticleEffects/particles/BullrtHits/particle_untagged");*/
		Wood = Resources.Load("bulletHole/wood");
		weaponCom = gameObject.GetComponent<Weapon>();
	}
	
	public void AddHole(RaycastHit hit) {
		Vector3 contact = hit.point;
		float rScale = 0.2f;
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
		GameObject woodMark = Instantiate (Wood, contact, rot) as GameObject;
		woodMark.transform.localPosition += .02f * hit.normal;
		woodMark.transform.localScale = new Vector3(rScale, rScale, rScale);
		woodMark.transform.parent = hit.transform;
		/*if (hit.collider.tag == "Concrete") {
			GameObject concMark = Instantiate (Concrete, contact, rot) as GameObject;
			concMark.transform.localPosition += .02f * hit.normal;
			concMark.transform.localScale = new Vector3(rScale, rScale, rScale);
			concMark.transform.parent = hit.transform;
	
		}else if (hit.collider.tag == "Enemy") {
			Instantiate (Blood, contact, rot);
			hit.collider.SendMessageUpwards("ApplyDamage", weaponCom.damage, SendMessageOptions.DontRequireReceiver);
		}else if (hit.collider.tag == "Damage") {
			Instantiate (Blood, contact, rot);
			hit.collider.SendMessageUpwards("ApplyDamage", weaponCom.damage, SendMessageOptions.DontRequireReceiver);		
		}else if (hit.collider.tag == "Wood") {
			GameObject woodMark = Instantiate (Wood, contact, rot) as GameObject;
			woodMark.transform.localPosition += .02f * hit.normal;
			woodMark.transform.localScale = new Vector3(rScale, rScale, rScale);
			woodMark.transform.parent = hit.transform;
			hit.collider.SendMessageUpwards("ApplyDamage", weaponCom.damage, SendMessageOptions.DontRequireReceiver);
		}else if (hit.collider.tag == "Metal") {
			GameObject metalMark = Instantiate (Metal, contact, rot) as GameObject;
			metalMark.transform.localPosition += .02f * hit.normal;
			metalMark.transform.localScale = new Vector3(rScale, rScale, rScale);
			metalMark.transform.parent = hit.transform;
			
		}else if (hit.collider.tag == "Dirt" || hit.collider.tag == "Grass") {
			GameObject dirtMark = Instantiate (Dirt, contact, rot) as GameObject;
			dirtMark.transform.localPosition += .02f * hit.normal;
			dirtMark.transform.localScale = new Vector3(rScale, rScale, rScale);
			dirtMark.transform.parent = hit.transform;
			
		}else if (hit.collider.tag == "Water") {
		   Instantiate (Water, contact, rot);

		}else if (hit.collider.tag == "Usable") {
			hit.collider.SendMessageUpwards("ApplyDamage", weaponCom.damage, SendMessageOptions.DontRequireReceiver);
			
		}else{
			GameObject def = Instantiate (Untagged, contact, rot) as GameObject;
			def.transform.localPosition += .02f * hit.normal;
			def.transform.localScale = new Vector3(rScale, rScale, rScale);
			def.transform.parent = hit.transform;
		}*/
	}
}
}
