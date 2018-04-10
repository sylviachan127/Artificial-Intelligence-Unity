using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingSound : MonoBehaviour {

	static BoxCollider collider;
	AudioSource grassSound;
	bool onGrass;
	bool onWater;
	// Use this for initialization

	void Start () {
		collider = GetComponent<BoxCollider>();
		grassSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (onGrass)
		//{
		//	collider.
		//	grassSound.Play();
		//}
	}

	void OnTriggerEnter(Collider other)
	{
		print ("colliding");
		if (other.CompareTag("Grass"))
		{
			onGrass = true;
			//grassSound.Play();
		}
		if (other.CompareTag("Water"))
		{
			//grassSound.Play();
			onGrass = false;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Grass"))
		{
			if (grassSound.isPlaying == false)
			{
				grassSound.Play();
			}
			//grassSound.Play();
		}
	}
}
