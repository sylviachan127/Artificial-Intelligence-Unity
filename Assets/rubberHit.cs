using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberHit : MonoBehaviour {
	public AudioSource rubberSound;
	public AudioClip impact;
	// Use this for initialization
	void Start()
	{
		rubberSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!rubberSound.isPlaying)
			{
				rubberSound.PlayOneShot(impact, 0.7F);
			}
		}
		if (col.gameObject.tag == "Moveable")
		{
			if (!rubberSound.isPlaying)
			{
				rubberSound.PlayOneShot(impact, 0.7F);
			}
		}
	}
}
