using UnityEngine;
using System.Collections;
namespace Shooter{
public class ExplosionDelay : MonoBehaviour {
	public float delayTime = 1f;
	public GameObject explosionAnimObj;
	public GameObject bombAnimObj;
	
	void  Start() {
		bombAnimObj.SetActive(true);
		explosionAnimObj.SetActive(false);
		//StartCoroutine(boom());
		boom();
	}
	
	//IEnumerator boom() {
	void boom() {
		//yield return new WaitForSeconds(1f);
		bombAnimObj.SetActive(false);
		explosionAnimObj.SetActive(true);
		Debug.Log(2);
	}
}
}
