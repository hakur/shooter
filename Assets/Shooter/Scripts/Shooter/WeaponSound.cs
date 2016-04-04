using UnityEngine;
using System.Collections;

public class WeaponSound : MonoBehaviour {
	public AudioSource sound;
	public AudioClip soundDraw;
	public AudioClip soundFire;
	public AudioClip soundClipIn;
	public AudioClip soundClipOut;
	public AudioClip soundLoad;
	
	void draw() {
		sound.PlayOneShot(soundDraw);
	}
	
	void fire() {
		sound.PlayOneShot(soundFire);
	}
	
	void clipIn() {
		sound.PlayOneShot(soundClipIn);
	}
	
	void clipOut() {
		sound.PlayOneShot(soundClipOut);
	}
	
	void load() {
		sound.PlayOneShot(soundLoad);
	}
}
