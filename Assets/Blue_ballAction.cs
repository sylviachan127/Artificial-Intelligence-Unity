using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_ballAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "redObs")
		{
			Destroy(gameObject);
			//print("blue destroy on: " + coll.tag);
		}
		if (coll.tag == "blueObs")
		{
			Destroy(gameObject);
		}
		if (coll.tag == "bluePlayer"){
			Destroy(gameObject);
		}
		if (coll.tag == "redPlayer")
		{
			Destroy(gameObject);
		}
		if (coll.tag == "redBall")
		{
			Destroy(gameObject);
		}
		if (coll.tag == "blueBall")
		{
			Destroy(gameObject);
		}
	}
}
