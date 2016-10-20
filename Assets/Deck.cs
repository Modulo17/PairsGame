using UnityEngine;
using System.Collections;
using System.Collections.Generic;		//Need to include this to use Lists

public class Deck : MonoBehaviour {


	public	GameObject	mCardPrefab;		//Link to prefab for Card-Master Object

	private	Sprite		mBackSprite;		//These are loaded from resources
	private	Sprite[]	mFrontSprites;

	private	List<Card> mDealerCards;		//List of cards the dealer holds

	void Start () {
		//For Resources.Load work the sprites must be in a Resources folder inside Assets
		mFrontSprites = Resources.LoadAll<Sprite> ("CardDeckSpriteSheet");		//Load all the sprites in the sheet into an array
		mBackSprite = Resources.Load<Sprite> ("MedResBack");		//Load the single back sprite
		mDealerCards=new List<Card>();
																			//NB: Even though it a 2D game I am incrementing Z as it will enuring the cards display correctly
																			//as the last dealt card will be furthest from the camera, making it look like they were spread out by a dealer
		MakeCustomDeck ();																	

		PositionCards ();

	}


	private	void	MakeDeck() {
		for (int tCardID = 0; tCardID < mFrontSprites.Length; tCardID++) {
			Card	tCard = MakeCard (tCardID);				//Make a new card from prefab
			mDealerCards.Add (tCard);					//Add card to dealer list
		}
	}

	private	void	MakeCustomDeck() {
		for (int tCardID = 0; tCardID < 10; tCardID+=2) {
			Card	tCard = MakeCard (tCardID);				//Make a new card from prefab
			mDealerCards.Add (tCard);					//Add card to dealer list
		}
	}


	private void	PositionCards() {			//Position cards in Gameworld accoring to position in Deck
		Vector3	tDealPosition = Vector3.zero;
		Vector3	tDealPositionOffset = new Vector3 (0.11f, 0f, 0.01f);		//Each card will be drawn with this relative offset, so it looks like they are spread on the table
		foreach(Card tCard in mDealerCards) {
			tCard.transform.localPosition = tDealPosition;	//Assign position
			tDealPosition += tDealPositionOffset;		//Step position along
		}
	}

	public	void	Shuffle() {
		List<Card>	tPreShuffleCards=mDealerCards;	//Old Dealer List of cards
		mDealerCards=new List<Card>();		//Dealer gets new list
		while (tPreShuffleCards.Count > 0) {
			Card tCard = tPreShuffleCards [Random.Range (0, tPreShuffleCards.Count)];
			tPreShuffleCards.Remove (tCard);
			mDealerCards.Add (tCard);
		}
		PositionCards ();
	}

	private	Card	MakeCard(int vCardID) {
		if (vCardID < mFrontSprites.Length) {		//Make sure card is valid, i.e. that we have a sprite for its front
			GameObject	tCardGO = GameObject.Instantiate (mCardPrefab);		//Make new GameObject from linked prefab
			tCardGO.transform.SetParent (transform);						//Make the dealer GO the parent, super handy as I can move all the cards by moving the dealer
			Card	tCard = tCardGO.GetComponent<Card> ();					//Get reference to card script
			tCard.Init (vCardID, mFrontSprites [vCardID], mBackSprite);		//Tell card to initalise itself
			return	tCard;
		}
		Debug.Log (string.Format("Invalid card ID {0}", vCardID));			//Error, CardID not valid
		return	null;
	}
}
