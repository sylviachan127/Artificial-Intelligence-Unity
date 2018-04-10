using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDetection_Blue : MonoBehaviour {

	public bool enemyDetect;
	// Use this for initialization
	Collider other;

	void Start()
	{
		enemyDetect = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (enemyDetect && !other)
		{
			enemyDetect = false;
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "redBall")
		{
			enemyDetect = true;
			this.other = coll;
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "redBall")
		{
			enemyDetect = false;
		}
	}
}
