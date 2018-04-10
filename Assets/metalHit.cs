using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metalHit : MonoBehaviour {
	public AudioSource metalSound;
	public AudioClip impact;
	// Use this for initialization
	void Start()
	{
		metalSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!metalSound.isPlaying)
			{
				metalSound.PlayOneShot(impact, 0.7F);
			}
		}
		if (col.gameObject.tag == "Moveable")
		{
			if (!metalSound.isPlaying)
			{
				metalSound.PlayOneShot(impact, 0.7F);
			}
		}
	}
}