using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour {
	public Collider[] legs;
	public GameObject leg;
	public GameObject player;
	static Animator anim;
	//public GameObject player;

	static BoxCollider collider;
	//static SphereCollider sphereCollider;
	static Rigidbody rigidBody;
	//AudioSource grassSound;
	public AudioSource[] sounds;
	public AudioSource waterSound;
	public AudioSource rockSound;
	public AudioSource grassSound;
	public AudioSource woodSound;
	bool onGrass;
	bool isMoving;
	bool onWater;
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

	Vector3 lastPosition;
	Transform myTransform;

	private List<GameObject> spawnPoints = new List<GameObject>();

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		//PrepareSpawnPointList();
		//print("ready to start");
		isMoving = false;
		myTransform = transform;
		lastPosition = myTransform.position;

		grassSound = leg.GetComponent<AudioSource>();
		collider = leg.GetComponent<BoxCollider>();

		sounds = GetComponents<AudioSource>();
		waterSound = sounds[3];
		rockSound = sounds[2];
		grassSound = sounds[1];
		woodSound = sounds[0];
		//anim = GetComponent<Animator>();
		////anim.transform.FindChild
		//leg = anim.transform.FindChild("Leg").gameObject;
		//print("i found leg: "+ leg);
		//collider = leg.GetComponent<BoxCollider>();
		//grassSound = leg.GetComponent<AudioSource>();

		//legs = GetComponentsInChildren<Collider>();
		//foreach (Collider l in legs)
		//{
		//	print("hi");
		//}

	}

	private void PrepareSpawnPointList()
	{
		Component[] childGroup = transform.GetComponentsInChildren<Component>();
		foreach (Component child in childGroup)
		{
			//print(n);
			//n += 1;
			//print(child.tag);
			if (child.CompareTag("Leg"))
			{
				grassSound = child.GetComponent<AudioSource>();
				collider = child.GetComponent<BoxCollider>();
			}
				//print("i am here");
		}
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
				animSpeed = (i + 1) * 0.5f;
			}
		}
		//collider.Raycast
		//if (Input.GetKeyUp("z"))
		//{
		//	noise1.Play();
		//}
		//if (Input.GetKeyUp("x"))
		//{
		//	noise2.Play();
		//}
		//if (rigidBody.IsSleeping())
		//{
		//	print("sleeping");
		//	isMoving = false;
		//}
		//else {
		//	isMoving = true;
		//}
		//Input.GetButtonDown
		//if (Input.GetAxis("Horizontal") > 0) 
		//{
		//	isMoving = true;
		//}
		//else {
		//	isMoving = false;
		//}

		if ((Mathf.Abs((collider.transform.position.x - lastPosition.x)) > 0.01) || (Mathf.Abs((collider.transform.position.z - lastPosition.z)) > 0.01))
		{
			isMoving = true;
			//print("My trasform" + myTransform.position);
			//print("lastPostion" + lastPosition);
		}
		////if ((Mathf.Abs((myTransform.position.x - lastPosition.x))>0.01) || (Mathf.Abs((myTransform.position.z - lastPosition.z)) > 0.01))
		////{
		////	isMoving = true;
		////	//print("My trasform" + myTransform.position);
		////	//print("lastPostion" + lastPosition);
		////}
		else {
			isMoving = false;
			//print("My trasform" + myTransform.position);
			//print("lastPostion" + lastPosition);
		}
		lastPosition = collider.transform.position;

	}

	//void OnTriggerEnter(Collider other)
	//{
	//	print("colliding");
	//	if (other.CompareTag("Grass"))
	//	{
	//		onGrass = true;
	//		//grassSound.Play();
	//	}
	//	if (other.CompareTag("Water"))
	//	{
	//		//grassSound.Play();
	//		onGrass = false;
	//	}

	//}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Grass"))
		{
			if ((grassSound.isPlaying == false) && (isMoving))
			{
				grassSound.pitch = animSpeed;
				grassSound.Play();
			}
		}
		if (other.CompareTag("Rock"))
		{
			if ((rockSound.isPlaying == false) && (isMoving))
			{
				rockSound.pitch = animSpeed;
				rockSound.Play();
				//print("playing rock");
			}
		}
		if (other.CompareTag("Wood"))
		{
			if ((woodSound.isPlaying == false) && (isMoving))
			{
				woodSound.pitch = animSpeed;
				woodSound.Play();
			}
		}
		if (other.CompareTag("Water"))
		{
			if ((waterSound.isPlaying == false) && (isMoving))
			{
				waterSound.pitch = animSpeed;
				//print("playing water step");
				waterSound.Play();
			}
		}
		if (other.CompareTag("WoodHit"))
		{
			print("hitting woodHit");
			if ((waterSound.isPlaying == false))
			{
				waterSound.pitch = animSpeed;
				//print("playing water step");
				waterSound.Play();
			}
		}
		print("tag" + other);
	}
}
