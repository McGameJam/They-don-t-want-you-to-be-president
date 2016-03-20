﻿using UnityEngine;
using System.Collections;

public class SelectMvt : MonoBehaviour {
	public Vector3 target;
	private GameObject[] cylinders;
	private bool hasdest;
	private int count;

	void Start () {
		cylinders = GameObject.FindGameObjectsWithTag("Node");
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				count = 0;
				Invoke ("shutDownLights",0.1f);
                GameObject go = hit.transform.gameObject;
				Behaviour h = (Behaviour)go.GetComponent ("Halo");
				h.enabled = !h.enabled;
				if (Physics.Raycast(ray, out hit, 100)){
						target = new Vector3(hit.point.x, 0.01f, hit.point.z);
						hasdest = true;
					if (hasdest){
						transform.position = Vector3.MoveTowards(transform.position, target, 1.0f*Time.deltaTime);
					}
					GameObject[] movingObjects = GameObject.FindGameObjectsWithTag ("MovingPlayer");
					for (int i = 0; i < movingObjects.Length; i++) {
						movingObjects [i].transform.position = Vector3.MoveTowards (transform.position, hit.collider.transform.position, 0.1f * Time.deltaTime);
					}
				}
			}
		}
	}
	void shutDownLights(){
		while (count < cylinders.Length) {
			Debug.Log (count);
			Behaviour j = (Behaviour)cylinders [count].GetComponent ("Halo");
			j.enabled = false;
			count++;
		}
	}
}
