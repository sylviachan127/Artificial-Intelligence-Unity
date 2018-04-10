using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPesronCamera : MonoBehaviour
{

	private Transform lookAt;
	private Vector3 startOffSet;

	// Use this for initialization
	void Start()
	{
		lookAt = GameObject.FindGameObjectWithTag("Player").transform;
		startOffSet = transform.position - transform.forward - lookAt.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = lookAt.position + startOffSet;
		//transform.RotateAround(lookAt.position, Vector3.up, Time.deltaTime * 5);
		//if (Input.GetKeyDown(KeyCode.A)){
		//	transform.RotateAround(lookAt.position, Vector3.up, Time.deltaTime * 5);
		//}
		//transform.position = transform.position - transform.forward * dist;
		//
		transform.LookAt(lookAt.position);
		//transform.forward = Vector3.Lerp(transform.forward, lookAt.forward, Time.deltaTime * 3);
	}
}