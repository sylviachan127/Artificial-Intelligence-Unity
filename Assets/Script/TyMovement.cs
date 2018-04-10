using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyMovement : MonoBehaviour {

	static Animator anim;
	static Rigidbody rigidBody;
	public float animSpeed = 1.0F;
	public float rotationSpeed = 100.0F;
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
	private float verticalVelocity;
	private float gravity = 14f;
	private float jumpForce = 10f;
	private float acceptHeight = 5f;
	private bool isGrounded = false;
	public Transform groundCheck;
	private Vector3 hiehgtCheck = new Vector3(0,1,0);
	private Vector3 currentPosition;

	public CapsuleCollider capsuleCollider;
	public Animator animator;
	public GameObject spawnPoint;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigidBody = anim.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis("Horizontal");              // setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");                // setup v variables as our vertical input axis

		if (Input.GetKey("0")){
			animSpeed = 5f;
		}
		for (int i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				animSpeed = (i+1)*0.5f;
			}
		}
		anim.speed = animSpeed;
		anim.SetFloat("Speed", v*anim.speed);                          // set our animator's float parameter 'Speed' equal to the vertical input axis				
		// hard left turn
		if (Input.GetKeyDown("a"))
		{
			h = -1;
			anim.SetBool("inPlace", true);
		}
		if (Input.GetKeyUp("a"))
		{
			anim.SetBool("inPlace", false);
			//anim.SetBool("leftTurn", false);
		}
		if (Input.GetKey("q"))
		{
			h = -0.5f;
			anim.SetBool("inPlace", true);
		}
		if (Input.GetKeyUp("q"))
		{
			anim.SetBool("inPlace", false);
		}
		if (Input.GetKey("d"))
		{
			h = 1f;
			anim.SetBool("inPlace", true);
		}
		if (Input.GetKeyUp("d"))
		{
			anim.SetBool("inPlace", false);
		}
		if (Input.GetKey("e"))
		{
			h = 0.5f;
			anim.SetBool("inPlace", true);
		}
		if (Input.GetKeyUp("e"))
		{
			anim.SetBool("inPlace", false);
		}
		anim.SetFloat("Direction", h);                      // set our animator's float parameter 'Direction' equal to the horizontal input axis		
															//set speed
															//transform.Translate(0, 0, v);
															//transform.Rotate(0, h, 0);
		Vector3 currentP = anim.bodyPosition;
		currentPosition = currentP - hiehgtCheck;

		if ((anim.transform.position.y > 1) && !(Physics.Raycast(currentPosition, hiehgtCheck)))
		{
			anim.SetBool("Falling", true);
		}
		else {
			anim.SetBool("Falling", false);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//verticalVelocity = jumpForce;
			anim.SetBool("Jumping", true);
		}
		else {
			anim.SetBool("Jumping", false);
		}
	}

	void die()
	{
		//rigidBody.isKinematic = true;
		capsuleCollider.enabled = false;
		rigidBody.isKinematic = true;
		animator.enabled = false;

	}
	void undie()
	{
		rigidBody.isKinematic = false;
		capsuleCollider.enabled = true;
		animator.enabled = true;
		transform.position = spawnPoint.transform.position;

	}
	void testRightFoot()
	{
	}
	void testLeftFoot()
	{
	}
}
