﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
	[SerializeField]private Dialogue dialogue;
	private GameObject outline;
	private bool highlight = false;
	private GameController gameController;
	// Use this for initialization
	void Start()
	{
		if (transform.childCount > 0)
		{
			outline = transform.GetChild(0).gameObject;
			Material mat = outline.GetComponent<Renderer>().material;
			if (mat.shader.name != "Outlined/Silhouette Only") Debug.LogError("Missing outline material from interactable object \"" + outline.name + "\"");
		}
		else Debug.LogError("Missing outline child object from interactable object \"" + gameObject.name + "\"");

		gameController = FindObjectOfType<GameController>();
		if (gameController == null) Debug.LogError("Missing GameController object.");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (outline != null)
		{
			outline.GetComponent<MeshRenderer>().enabled = false;
			if (highlight) outline.GetComponent<MeshRenderer>().enabled = true;
			highlight = false;
		}
	}

	public void Highlight()
	{
		highlight = true;
	}

	public void ShowDialogue()
	{
		if (gameController != null)
		{
			gameController.PassDialogue(dialogue);
		}
	}
}
