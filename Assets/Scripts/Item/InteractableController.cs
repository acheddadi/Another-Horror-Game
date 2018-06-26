using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
	[SerializeField]private UnityEvent myUnityEvent;
	[SerializeField]private enum InteractionType { ITEM, EVENT, TEXT }
	[SerializeField]private InteractionType type;
	[SerializeField]private Dialogue dialogue;
	[SerializeField]private Dialogue alternateDialogue;
	private GameObject outline;
	private bool highlight = false, altDialogue = false;
	private GameController gameController;

	void Awake()
	{
		if (myUnityEvent == null)
        myUnityEvent = new UnityEvent();
    }

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
		if (alternateDialogue == null) alternateDialogue = dialogue;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (outline != null)
		{
			outline.GetComponent<MeshRenderer>().enabled = false;
			GameController.leftClick = highlight;
			highlight = false;
		}
	}

	public void Highlight()
	{
		highlight = true;
	}

	public void Interact()
	{
		gameController.PassInteraction(this);
	}

	public void StartEvent()
	 {
		myUnityEvent.Invoke();
		altDialogue = true;
		type = InteractionType.TEXT;
     }

	 public Dialogue ReturnDialogue()
	 {
		 if (!altDialogue) return dialogue;
		 else return alternateDialogue;
	 }

	 public bool IsEvent()
	 {
		 if (type == InteractionType.EVENT) return true;
		 else return false;
	 }
}
