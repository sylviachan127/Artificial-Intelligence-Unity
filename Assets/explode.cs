using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {
	// explosion Effect
	public GameObject player;
	public Rigidbody player_body;
	public float radius = 5.0F;
	public float power = 10.0F;

	// ragDoll effect
	public CapsuleCollider capsuleCollider;
	public Animator animator;
	public GameObject spawnPoint;

	public AudioSource bombExplode;
	bool playerDead = false;
	bool respawnNow = false;
	Vector3 explosionPos;
	// Use this for initialization
	void Start () {
		explosionPos = player.transform.position;
		//test = ball.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDead)
		{
			StartCoroutine(HandleIt());
		}
		if (respawnNow)
		{
			undie();
		}
		//else {
		//	undie();
		//}
	}

	private IEnumerator HandleIt()
	{
		print("i am here");
		yield return new WaitForSeconds(3.0f);
		playerDead = false;
		respawnNow = true;
	}

	void blowUp()
	{
		player_body.AddExplosionForce(power, explosionPos, radius, 3.0F);
		bombExplode.Play();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Bomb")
		{
			print("hit a bomb");
			blowUp();
			die();
		}
	}

	void die()
	{
		//rigidBody.isKinematic = true;
		capsuleCollider.enabled = false;
		player_body.isKinematic = true;
		animator.enabled = false;
		playerDead = true;
		//waiting();
		//yield return new WaitForSeconds(5);
		//undie();

	}
	IEnumerator waiting()
	{
		print("waiting");
		yield return new WaitForSeconds(1);
		print("time to undie");
		undie();
	}

	void undie()
	{
		player_body.isKinematic = false;
		capsuleCollider.enabled = true;
		animator.enabled = true;
		transform.position = spawnPoint.transform.position;
		respawnNow = false;
		playerDead = false;

	}
}
