using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Shooter{
public class WeaponManager : MonoBehaviour {
	public GameObject current; //当前武器
	public GameObject[] weapons = new GameObject[4];
	private GameObject lastWeapon;//上一个武器 按Q且切换武器用
	public GameObject[] weaponList; //预置物体
	private int currentIndex = 0;
	private int lastIndex = 0;
	private bool inited = false;
	public Image sniperScopeImg;
	public GameObject crosshair;
	public RectTransform leftCrossHair;
	public RectTransform rightCrossHair;
	public RectTransform upCrossHair;
	public RectTransform downCrossHair;
	void Awake() {
		//sniperScopeImg = GameObject.Find("SniperScopeShell").GetComponent<Image>();
		//crosshair = GameObject.Find("CrossHair");
	}
	
	void  Start() {
		init();
		if(weapons[0] != null) {
			useWeapon(0);
			setUse(0);
		} else if(weapons[1] != null) {
			useWeapon(1);
		} else if(weapons[2] != null) {
			useWeapon(2);
		} else if(weapons[3] != null) {
			useWeapon(3);
		}
	}
	
	void LateUpdate() {
		if(Input.GetKeyDown("1") && weapons[0] != null) {
			useWeapon(0);
		} else if(Input.GetKeyDown("2") && weapons[1] != null) {
			useWeapon(1);
		} else if(Input.GetKeyDown("3") && weapons[2] != null) {
			useWeapon(2);
		} else if(Input.GetKeyDown("4") && weapons[3] != null) {
			useWeapon(3);
		} else if(Input.GetKeyDown("q") && lastWeapon!= null) {
			useWeapon(lastIndex);
		}
	}
	
	void useWeapon(int i) {
		if(currentIndex == i && inited) {
			return;
		}
		if(weapons[i]==null) {
			return;
		}
		inited = true;
		lastWeapon = current;
		lastIndex = currentIndex;
		Weapon w;
		for(int a=0;a<weapons.Length;a++) {
			if(weapons[a]!=null) {
				w = weapons[a].GetComponent<Weapon>();
				w.selected = false;
				weapons[a].active = false;
			}
		}
		weapons[i].active = true;
		WeaponAnim anim = weapons[i].GetComponent<WeaponAnim>();
		anim.DrawAnim();
		
		currentIndex = i;
		current = weapons[i];
		current.GetComponent<Weapon>().init();
	}
	
	void init() {
		for(int i=0;i<weapons.Length;i++) {
			if(weapons[i] != null) {
				if(IsPrefab(weapons[i].transform)) {
					weapons[i] = Instantiate(weapons[i]) as GameObject;
					weapons[i].transform.parent = transform;
					weapons[i].layer = LayerMask.NameToLayer("Player");
					weapons[i].active = false;
				} else {
					weapons[i].active = false;
				}
				Aim aim = weapons[i].GetComponent<Aim>();
				aim.sniperScopeImg = sniperScopeImg;
				aim.crosshair = crosshair;
				aim.scopeObj = sniperScopeImg.gameObject;
				CrossHair cross = weapons[i].GetComponent<CrossHair>();
				if(cross != null) {
					cross.down = downCrossHair;
					cross.up = upCrossHair;
					cross.right = rightCrossHair;
					cross.left = leftCrossHair;
				}
			//scopeObj = GameObject.Find("SniperScope");;
			}
		}
	}
	void setUse(int i) {
		if(weapons[i]!=null) {
			weapons[i].GetComponent<Weapon>().selected = true;
		}
	}
	
	bool IsPrefab(this Transform This)
     {
         var TempObject = new GameObject();
         try
         {
             TempObject.transform.parent = This.parent;
             var OriginalIndex = This.GetSiblingIndex();
             This.SetSiblingIndex(int.MaxValue);
             if (This.GetSiblingIndex() == 0) return true;
             This.SetSiblingIndex(OriginalIndex);
             return false;
         }
         finally
         {
             Object.DestroyImmediate(TempObject);
         }
     }
}
}