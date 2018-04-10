using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_ballAction : MonoBehaviour {

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
			print("red ball destroy on: " + coll.tag);
		}
		if (coll.tag == "blueObs")
		{
			Destroy(gameObject);
			print("red ball destroy on: " + coll.tag);
		}
		if (coll.tag == "redPlayer")
		{
			Destroy(gameObject);
			print("red ball destroy on: " + coll.tag);
		}
		if (coll.tag == "bluePlayer")
		{
			Destroy(gameObject);
			print("red ball destroy on: " + coll.tag);
		}
		if (coll.tag == "redBall")
		{
			Destroy(gameObject);
			print("red ball destroy on: " + coll.tag);
		}
		if (coll.tag == "blueBall")
		{
			Destroy(gameObject);
			print("red ball destroy on: " + coll.tag);
		}
		//if (coll.tag == "redPlayer")
		//{
		//}
		//else if (coll.tag == "redDetect")
		//{
		//}
		//else {
		//	//Destroy(coll.gameObject);
		//	Destroy(gameObject);
		//	print("delete trigger");
		//}
	}
}
