﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public Renderer rend;
	[HideInInspector] public float size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		size = rend.bounds.size.x * rend.bounds.size.y;

	}
}
