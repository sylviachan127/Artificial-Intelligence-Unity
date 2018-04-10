using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour {
	static Animator anim;
	static Rigidbody rigidBody;
	static BoxCollider boxCollider;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigidBody = anim.GetComponent<Rigidbody>();
		boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("z"))
		{
			anim.SetBool("open", true);
		}
		if (Input.GetKeyUp("y"))
		{
			anim.SetBool("open", false);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		print("door colliding");
		if (other.CompareTag("Player"))
			
		{
			print("door colliding with player");
			anim.SetBool("open", true);
		}
	}

	//void OnTriggerExit(Collider other)
	//{
	//	anim.SetBool("open", false);
	//}
}
