using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Health : NetworkBehaviour {
	public float hp = 100;
	public float hpmax = 100;
	
	public Text HPText;
	private Image HPMask;
	private Image HPShellBG;
	
	void Start () {
		if(hp > hpmax) {
			hp = hpmax;
		}
	}
	
	void Update () {
		if(isLocalPlayer) {
			HPText.text = Mathf.CeilToInt(hp).ToString();
		}
	}
	
	void ApplyDamage(float h) {
		hp -= h;
		if(hp < 1) {
			Die();
		}
	}
	
	void Die() {
		Debug.Log("die");
	}
}
