using UnityEngine;
using System.Collections;

public class ManualCard : MonoBehaviour {

	public	Sprite	Front;		//Allow front sprite to be selected in IDE
	public	Sprite	Back;		//Allow back sprite to be selected in IDE
	public	int 	CardID;		//Card ID
	public	bool	Shown;		//Are we showing it?

	private	SpriteRenderer	mSR;		//Cache SpriteRenderer for faster access

	// Use this for initialization
	void Start () {
		mSR = GetComponent<SpriteRenderer> ();		//Get SR component
	}
	// Update is called once per frame, decide which side of the card to show
	void Update () {
		if (Shown) {				//Show front?
			mSR.sprite = Front;		//Link to frotn sprite
		} else {
			mSR.sprite = Back;		//Link to back sprite
		}
	}
}
