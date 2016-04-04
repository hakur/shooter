using UnityEngine;
using System.Collections;
namespace Shooter{
public class CrossHair : MonoBehaviour {
	private Weapon weapon;
	[HideInInspector]
	public RectTransform left;
	private float offsetLeft;
	private float offsetLeftBase;
	[HideInInspector]
	public RectTransform right;
	private float offsetRight;
	private float offsetRightBase;
	[HideInInspector]
	public RectTransform up;
	private float offsetUp;
	private float offsetUpBase;
	[HideInInspector]
	public RectTransform down;
	private float offsetDown;
	private float offsetDownBase;
	public float basicOffset = 0.5f;
	private float offset;
	public float offsetMax = 10f;
	public float increase = 0.1f;
	public float returnSpeed = 0.2f;
	private bool canIncrease = true;
		
	void Awake() {
		weapon = gameObject.GetComponent<Weapon>();
	}
	
	void Start() {
		offsetLeftBase = offsetLeft += basicOffset;
		offsetLeftBase += left.localPosition.x;
		offsetRightBase = offsetRight += basicOffset;
		offsetRightBase += right.localPosition.x;
		offsetUpBase = offsetUp += basicOffset;
		offsetUpBase += up.localPosition.y;
		offsetDownBase = offsetDown += basicOffset;
		offsetDownBase += down.localPosition.y;
	}
	
	void FixedUpdate () {
		if(weapon.selected) {
			if(weapon.isFiring && canIncrease) {
				offset += increase;
				if(offset > offsetMax) {
					offset = offsetMax;
					canIncrease = false;
					StartCoroutine(doIncrease());
				}
			} else {
				offset -= returnSpeed;
				if(offset < basicOffset) {
					offset = basicOffset;
				}
				//Debug.Log(offset);
			}
			
			left.localPosition = new Vector3(offsetLeftBase-offset,left.localPosition.y,left.localPosition.z);
			right.localPosition = new Vector3(offsetRightBase+offset,right.localPosition.y,right.localPosition.z);
			up.localPosition = new Vector3(up.localPosition.x,offsetUpBase+offset,up.localPosition.z);
			down.localPosition = new Vector3(down.localPosition.x,offsetDownBase-offset,down.localPosition.z);
		}	
	}
	
	IEnumerator doIncrease() {
		yield return new WaitForSeconds(0.032f);
		canIncrease = true;
	}
}
}
