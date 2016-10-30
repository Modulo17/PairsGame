using UnityEngine;
using System.Collections;
using System.Collections.Generic;		//Need to include this to use Lists

public class Deck : MonoBehaviour {


	public	GameObject	mCardPrefab;		//Link to prefab for Card-Master Object

	private	Sprite		mBackSprite;		//These are loaded from resources
	private	Sprite[]	mFrontSprites;

	private	List<Card> mDealerCards;		//List of cards the dealer holds

	private	bool	mAllShowing;

	void Awake () {
		
		//For Resources.Load work the sprites must be in a Resources folder inside Assets
		//Load all the sprites in the sheet into an array
		mFrontSprites = Resources.LoadAll<Sprite> ("CardDeckSpriteSheet");
		//Load the single back sprite
		mBackSprite = Resources.Load<Sprite> ("MedResBack");
		mDealerCards=new List<Card>();
	}


	public	int		Count {		//Get count of dealer cards
		get {
			return	mDealerCards.Count;
		}
	}

	public	void	MakeDeck() {
		mAllShowing = false;
		for (int tCardID = 0; tCardID < mFrontSprites.Length; tCardID++) {			//make a 52 card deck
			Card	tCard = MakeCard (tCardID);				//Make a new card from prefab
			tCard.ShowCard=mAllShowing;
			tCard.CardHidden = true;
			mDealerCards.Add (tCard);					//Add card to dealer list
		}
	}

	private	void	MakeCustomDeck() {					//Used for debug to make special decks
		for (int tCardID = 0; tCardID < 4; tCardID++) {
			Card	tCard = MakeCard (tCardID*13);				//Make a new card from prefab
			mDealerCards.Add (tCard);					//Add card to dealer list
		}
	}


	public	void	Flip() {
		mAllShowing = !mAllShowing;
		foreach (Card tCard in mDealerCards) {
			tCard.ShowCard = mAllShowing;
		}
	}

	private	Card	MakeCard(int vCardID) {
		if (vCardID < mFrontSprites.Length) {		//Make sure card is valid, i.e. that we have a sprite for its front
			GameObject	tCardGO = GameObject.Instantiate (mCardPrefab);		//Make new GameObject from linked prefab
			tCardGO.transform.SetParent (transform);						//Make the dealer GO the parent
																			//super handy as I can move all the cards by moving the dealer
			Card	tCard = tCardGO.GetComponent<Card> ();					//Get reference to card script
			tCard.Init (vCardID, mFrontSprites [vCardID], mBackSprite);		//Tell card to initalise itself
			return	tCard;													//Return reference to card
		}
		Debug.Log (string.Format("Invalid card ID {0}", vCardID));			//Error, CardID not valid
		return	null;
	}

	//Deals a card from the Deck
	//Card wont show as its set to hidden
	//All cards positioned to middle of table
	//If no more cards, null returned
	public	Card	DealCard() {
		if (mDealerCards.Count > 0) {	//if we have cards left
			Card tCard = mDealerCards [0];	//Get First Card
			mDealerCards.Remove(tCard);
			tCard.CardHidden = true;
			tCard.transform.SetParent (null);		//Unparent
			tCard.transform.position=Vector3.zero;		//Center of screen
			return tCard;
		}
		return	null;		//No more cards
	}

	//Shuffle cards in Deck, must be used before dealing them
	public	void	Shuffle() {
		List<Card>	tPreShuffleCards=mDealerCards;	//Old Dealer List of cards, keep to for shuffle
		mDealerCards=new List<Card>();		//Dealer gets new list
		while (tPreShuffleCards.Count > 0) {
			Card tCard = tPreShuffleCards [Random.Range (0, tPreShuffleCards.Count)];		//Pick random card
			tPreShuffleCards.Remove (tCard);												//Remove from temp Deck
			mDealerCards.Add (tCard);														//Place in new Dealer deck
		}
	}


}
