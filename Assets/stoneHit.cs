using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneHit : MonoBehaviour {
	public AudioSource rockHit;
	public AudioClip impact;
	// Use this for initialization
	void Start()
	{
		rockHit = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!rockHit.isPlaying)
			{
				rockHit.PlayOneShot(impact, 0.7F);
			}
		}
		if (col.gameObject.tag == "Moveable")
		{
			if (!rockHit.isPlaying)
			{
				rockHit.PlayOneShot(impact, 0.7F);
			}
		}
	}
}
