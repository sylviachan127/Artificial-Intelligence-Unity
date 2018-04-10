using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseHit : MonoBehaviour {
	public AudioSource horseSound;
	public AudioClip impact;
	// Use this for initialization
	void Start()
	{
		horseSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!horseSound.isPlaying)
			{
				horseSound.PlayOneShot(impact, 0.7F);
			}
		}
		if (col.gameObject.tag == "Moveable")
		{
			if (!horseSound.isPlaying)
			{
				horseSound.PlayOneShot(impact, 0.7F);
			}
		}
	}
}