using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWalking : MonoBehaviour {

	public AudioSource[] sounds;
	public AudioSource waterSound;
	public AudioSource rockSound;
	public AudioSource grassSound;
	public AudioSource woodSound;
	public BoxCollider rightFoot;
	public BoxCollider leftFoot;


	Vector3 rightFootPosition;
	Vector3 leftFootPosition;
	bool onGrass = false;
	bool onRock = false;
	bool onWater = false;
	bool onWood = false;

	public float animSpeed = 1.0F;
	private KeyCode[] keyCodes = {
		 KeyCode.Alpha1,
		 KeyCode.Alpha2,
		 KeyCode.Alpha3,
		 KeyCode.Alpha4,
		 KeyCode.Alpha5,
		 KeyCode.Alpha6,
		 KeyCode.Alpha7,
		 KeyCode.Alpha8,
		 KeyCode.Alpha9,
	 };

	// Use this for initialization
	void Start () {
		rightFootPosition = rightFoot.transform.position;
		leftFootPosition = leftFoot.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("0"))
		{
			animSpeed = 10;
		}
		for (int i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				animSpeed = ((i + 1) * 0.5f) + 0.1f;
			}
		}
		waterSound.pitch = animSpeed;
		grassSound.pitch = animSpeed;
		woodSound.pitch = animSpeed;
		rockSound.pitch = animSpeed;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Grass"))
		{
			onGrass = true;
			onRock = false;
			onWood = false;
			onWater = false;
		}
		if (other.CompareTag("Rock"))
		{
			onGrass = false;
			onRock = true;
			onWood = false;
			onWater = false;
		}
		if (other.CompareTag("Wood"))
		{
			onGrass = false;
			onRock = false;
			onWood = true;
			onWater = false;
		}
		if (other.CompareTag("Water"))
		{
			onGrass = false;
			onRock = false;
			onWood = false;
			onWater = true;
		}
	}

	void testRightFoot()
	{
		if (onGrass)
		{
			if(!grassSound.isPlaying){
				grassSound.Play();
			}
		}
		if (onRock)
		{
			if (!rockSound.isPlaying)
			{
				rockSound.Play();
			}
		}
		if (onWood)
		{
			if (!woodSound.isPlaying)
			{
				woodSound.Play();
			}
		}
		if (onWater)
		{
			if (!waterSound.isPlaying)
			{
				waterSound.Play();
			}
		}
		// Use hieracy to find Collider and transform
	}

	void testLeftFoot()
	{
		if (onGrass)
		{
			if(!grassSound.isPlaying){
				grassSound.Play();
			}
		}
		if (onRock)
		{
			if (!rockSound.isPlaying)
			{
				rockSound.Play();
			}
		}
		if (onWood)
		{
			if (!woodSound.isPlaying)
			{
				woodSound.Play();
			}
		}
		if (onWater)
		{
			if (!waterSound.isPlaying)
			{
				waterSound.Play();
			}
		}
		// Use hieracy to find Collider and transform
	}
}
