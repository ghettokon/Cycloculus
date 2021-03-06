﻿using UnityEngine;
using System.Collections;

public class makeObjects : MonoBehaviour {

	GameObject[] targetList;
	public GameObject theCube, mann1, mann2, mann3, mann4;
	public int score = 0;
	public GUIText myText;

	// Use this for initialization
	void Start () {
		InvokeRepeating("makeStuff", 2, 2);
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("score = " + score);
		myText.text = "Score: " + score;
	}

	void makeStuff() {
		targetList = GameObject.FindGameObjectsWithTag("Respawn");
		//Debug.Log(targetList.Length);
		if (targetList.Length < 9) {
			GameObject tmp = theCube;
			float die = Random.value;
			float numSides = 5f;
			if (die < 1f / numSides) {
				tmp = theCube;
			} else if (die >= 1f / numSides && die < (1f / numSides) * 2f) {
				tmp = mann1;
			} else if (die >= (1f / numSides) * 2f && die < (1f / numSides) * 3f) {
				tmp= mann2;
			} else if (die >= (1f / numSides) * 3f && die < (1f / numSides) * 4f) {
				tmp = mann3;
			} else if (die >= (1f / numSides) * 4f) {
				tmp = mann4;
			}
			float limit = 50f;
			GameObject newThang = Instantiate(tmp, new Vector3(Random.Range(-limit, limit), 0, Random.Range(0, limit)), Quaternion.identity) as GameObject;
			newThang.rigidbody.velocity = BallisticVel(newThang.transform);
			//Debug.Log(newThang.rigidbody.velocity);

			/*
			AudioSource audioSource = newThang.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("sounds/danger") as AudioClip;
			audioSource.minDistance = 50;
			audioSource.loop = true;
			audioSource.Play();
			*/

			GetComponent<AudioSource>().audio.Play();
		}
	}

	// Thanks to http://answers.unity3d.com/questions/448681/how-do-i-make-a-projectile-arc-and-always-hit-a-mo.html for this method.
	Vector3 BallisticVel(Transform projectile) {
		Vector3 dir = Camera.main.transform.position - projectile.position; // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		dir.y = dist;  // set elevation to 45 degrees
		dist += h;  // correct for different heights
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
		return vel * dir.normalized;  // returns Vector3 velocity
	}
}
