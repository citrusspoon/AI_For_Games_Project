﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	private Animator _animator;
	private CanvasGroup _canvasGroup;

	public bool IsOpen
	{
		get { return _animator.GetBool("IsOpen"); }
		set { _animator.SetBool("IsOpen", value); }
	}

	public void Awake()
	{
		_animator = GetComponent<Animator> ();
		_canvasGroup = GetComponent<CanvasGroup> ();

		var rec = GetComponent<RectTransform> ();
		rec.offsetMax = rec.offsetMin = new Vector2 (0, 0);
	
	}

	public void Update()
	{
		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open"))
		{
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		}
		else
		{
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
		}
	}
}
