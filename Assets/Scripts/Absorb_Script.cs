﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb_Script : MonoBehaviour
{

	public GameObject background;
	//public GameObject gameController;


	// Use this for initialization
	void Start ()
	{
		
	}
		
	void OnCollisionEnter (Collision col)
	{

		if (col.gameObject.tag == "Bounds" && this.gameObject.tag == "Player")
			print ("Hit bounds");

		if (col.gameObject.tag != "Bounds") {
			float thisSize = this.gameObject.GetComponent<Entity> ().size;
			float colSize = col.gameObject.GetComponent<Entity> ().size;

			if (thisSize > colSize) {
				col.gameObject.SetActive (false);
				col.gameObject.GetComponent<Entity> ().removeSelfFromList ();
				StartCoroutine (this.sizeInc (this.transform, thisSize, colSize));

				//plays the consume sound only if it was the player that got bigger
				if (this.gameObject.tag == "Player") {
					this.gameObject.GetComponent<Player_Controller> ().audio.Play ();
				}



			} else {
				//what should we do if they're the same size?
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator sizeInc (Transform source, float bigger, float smaller)
	{
		//minimum of 1, maximum of 2. Usually falls between 1.2 and 1.5
		float deltaScale = (smaller/bigger) + 1;
		float rateOfChange = .1f;
		Vector3 target = source.localScale + new Vector3 (deltaScale, deltaScale, deltaScale);

		//This information is only used if the player grows, so it's in an if statement to prevent enemies from trying to access a null gameobject
		Vector3 backgroundTarget = Vector3.zero;
		if (this.gameObject.tag == "Player") {
			backgroundTarget = background.transform.localScale + new Vector3 (deltaScale + 0.5f, deltaScale + 0.5f, deltaScale + 0.5f);

			//maybe replace this with some math later
			if(deltaScale < 1.2)
				this.gameObject.GetComponent<Player_Controller> ().updateEnergy (10f);
			else if(deltaScale < 1.4f)
				this.gameObject.GetComponent<Player_Controller> ().updateEnergy (50f);
			else
				this.gameObject.GetComponent<Player_Controller> ().updateEnergy (100f);
				
		}
		
		//this.gameObject.GetComponent<Light> ().range += deltaScale*2;
		this.gameObject.GetComponent<Light> ().range *= this.gameObject.transform.localScale.x*2;
		//print (this.gameObject.transform.localScale.x);

		while (source.localScale.x < target.x) {
			source.localScale = Vector3.Lerp (source.localScale, target, rateOfChange);

			//Only scales background when the player grows
			if (this.gameObject.tag == "Player") {
				
				background.transform.localScale = Vector3.Lerp (background.transform.localScale, backgroundTarget, rateOfChange);
				Game_Controller.instance.planeSize = backgroundTarget;



			}


			this.gameObject.GetComponent<Light> ().range = this.gameObject.transform.localScale.x*2;
			print (this.gameObject.transform.localScale.x);
			yield return null;
		}

	}

}
