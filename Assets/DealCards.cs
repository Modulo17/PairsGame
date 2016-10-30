using UnityEngine;
using System.Collections;
using System.Collections.Generic;		//Need this for List<Type>

public class DealCards : MonoBehaviour {


	//You should only be enditng this file
	//feel free to review the others, however only make changes if 100 sure
	//Comment all your additonal code


	public	GameObject	DeckGO;		//Linked in IDE


	Deck	mDeck;					//Reference to Deck Object
	PrintInstructions	mPrint;		//Reference to Print which prints on UI Text

	List<Card>	mPlayer1Cards;		//Player 1 list

	// Use this for initialization
	void Start () {
		mDeck = DeckGO.GetComponent<Deck> ();				//Get Ref to Deck so we can talk to it
		mPrint = GetComponent<PrintInstructions> ();		//Get PrintInstructions to Deck so we can talk to it
		mPlayer1Cards = new List<Card> ();					//Initialise the list
		mDeck.MakeDeck ();									//Make a Deck
		mPrint.ClearText();									//Use Print to print to UI Canvas
		mPrint.AddText(string.Format("Made Deck with {0} cards", mDeck.Count));	//Some Debug
	}

	//Modify this code as per workshop instructions

	public void Deal5Cards() {
		Card tCard=mDeck.DealCard ();
		if (tCard != null) {			//Will be null if not more cards
			tCard.CardHidden = false;		//Show card
			tCard.transform.SetParent (transform);		//Parent it to this GO, makes Hierachy neater
			tCard.transform.position=Vector2.zero;		//Put at orgin, if more than 1 card would need to calculate display position
			mPrint.ClearText();									//Use Print to print to UI Canvas
			mPrint.AddText(string.Format("{0} cards left", mDeck.Count));	//Some Debug
		}
	}



	void Update () {
		if (Input.GetMouseButtonDown (0)) {			//Only run raycast if mouse down has occured since last Update
			Ray tMousePointerRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//Make a ray from the mouse pointer using the main camera
			RaycastHit2D	tMousePointerRayHit = Physics2D.Raycast (tMousePointerRay.origin,tMousePointerRay.direction);	//Cast the ray into the gameworld from the mouse position, pointing along direction (into the screen)
			if (tMousePointerRayHit.collider != null) {											//If collider is null we did not hit anything, otherise it wil be a reference the collider on the GameObject we hit
				GameObject tObjectHit = tMousePointerRayHit.collider.gameObject;				//At this point we know we have hit a collider on a GameObject, so get the game object
				Debug.Log (string.Format("Ray Cast Hit {0}",tObjectHit.name));					//Show what we have hit in the console Window, handy for Debugging
				Card tCardHit=tObjectHit.GetComponent<Card>();									//Get the Card Script object, if it does not have one, if this returns null its either not a card, or we forgot to attach the Card Script it
				if (tCardHit != null) {															//Ensure the object we hit is a Card, as we may have other items in the scene too later	

					///This section is hit if a card has been clicked on
					/// Modify according to workshop instructions
					tCardHit.ShowCard = !tCardHit.ShowCard;										//Flip the card by changing its state directly



				} else {
					Debug.Log (string.Format("WARNING: Ray Cast Hit Object {0} does not have a Card component",tObjectHit.GetType().Name));		//Perhaps we forgot to add one?
				}
			}
		}
	}

}
