using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickHit : MonoBehaviour {

	public AudioSource brickSound;
	public AudioClip impact;
	// Use this for initialization
	void Start()
	{
		brickSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!brickSound.isPlaying)
			{
				brickSound.PlayOneShot(impact, 0.7F);
			}
		}
		if (col.gameObject.tag == "Moveable")
		{
			if (!brickSound.isPlaying)
			{
				brickSound.PlayOneShot(impact, 0.7F);
			}
		}
	}
}