using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playIfCollide : MonoBehaviour {
	public AudioSource woodHit;
	public AudioClip impact;
	// Use this for initialization
	void Start () {
		woodHit = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionStay(Collision col)
	{
		print ("col is: " + col + " is playing. ");
		if(col.gameObject.tag=="Player") {
			if(!woodHit.isPlaying){
				woodHit.Play();
			}
		}
		if(col.gameObject.tag=="Moveable") {
			if(!woodHit.isPlaying){
				woodHit.PlayOneShot(impact, 0.7F);
			}
		}
	}
}

