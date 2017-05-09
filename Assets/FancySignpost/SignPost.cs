using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignPost : MonoBehaviour
{
	public TextMesh textbox1;
	public TextMesh shadow1;
	public TextMesh textbox2;
	public TextMesh shadow2;
	public TextMesh textbox3;
	public TextMesh shadow3;
	public string text1 = "Your signpost stuff here";
	public string text2 = "Line 2";
	public string text3 = "Line 3";

	// Use this for initialization
	void Start ()
	{
		textbox1.text = text1;
		shadow1.text = text1;
		textbox2.text = text2;
		shadow2.text = text2;
		textbox3.text = text3;
		shadow3.text = text3;

		textbox1.gameObject.SetActive(false);
		shadow1.gameObject.SetActive(false);
		textbox2.gameObject.SetActive(false);
		shadow2.gameObject.SetActive(false);
		textbox3.gameObject.SetActive(false);
		shadow3.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			textbox1.gameObject.SetActive(true);
			shadow1.gameObject.SetActive(true);
			textbox2.gameObject.SetActive(true);
			shadow2.gameObject.SetActive(true);
			textbox3.gameObject.SetActive(true);
			shadow3.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			textbox1.gameObject.SetActive(false);
			shadow1.gameObject.SetActive(false);
			textbox2.gameObject.SetActive(false);
			shadow2.gameObject.SetActive(false);
			textbox3.gameObject.SetActive(false);
			shadow3.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
