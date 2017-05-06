using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switch sprites and disable collider.
public class OpenDoor : Triggerable
{
	SpriteRenderer sp;
	Collider2D col;

	public Sprite closed;
	public Sprite open;
	public bool simpleDoor = false;
	bool pass = false;
	public string loadLevel = "";

	void Start ()
	{
		sp = GetComponent<SpriteRenderer>();
		//col = GetComponent<Collider2D>();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if ((pass || simpleDoor) && other.tag == "Player")
			SceneManager.LoadScene(loadLevel);
	}

	public override void Trigger()
	{
		sp.sprite = open;
		pass = true;
		//col.enabled = false;
	}

	public override void UnTrigger()
	{
		sp.sprite = closed;
		pass = false;
		//col.enabled = true;
	}
}
