using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
	public float hp = 100;
	public float hpmax = 100;
	
	private Text HPText;
	private Image HPMask;
	private Image HPShellBG;
	
	void Awake() {
		HPText = GameObject.Find("HPNum").GetComponent<Text>();
		//HPMask = GameObject.Find("HPMask").GetComponent<Image>();
		//HPShellBG = GameObject.Find("HPShellBG").GetComponent<Image>();
	}
	
	void Start () {
		if(hp > hpmax) {
			hp = hpmax;
		}
	}
	
	void Update () {
		HPText.text = Mathf.CeilToInt(hp).ToString();
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
