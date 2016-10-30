using UnityEngine;
using System.Collections;



//This script is attached to the Card Game object
//The solution file contains some ideas
//on variable names and comments I would use
//to describe how it works


//Card Class
//Used to define the behaviour of a single card
//Assumes a card can display one of 2 sprites so it can either appear turned over (showing its back) or its Rank/Suit



public class Card : MonoBehaviour {

	//Text which describes each Suit
	//Static as all cards share these names
	//Stored as an array
	private	static	string[] sSuiteName = {
		"Clubs"			
		,"Diamonds"
		,"Hearts"
		,"Spades"
	};
	//Text which describes each Rank
	//Static as all cards share these names
	//Stored as an array
	private	static string[] sRankName = {
		"Ace"
		, "Two"
		, "Three"
		, "Four"
		, "Five"
		, "Six"
		, "Seven"
		, "Eight"
		, "Nine"
		, "Ten"
		, "Jack"
		, "Queen"
		, "King"
	};

		//Return Suite name, clamp to Array size
	public	string	SuitName {
		get	{
			return	sSuiteName[Suit]; //Uses Suit getter
		}
	}
	//Return Rank name, clamp to Array size
	public	string	RankName {
		get	{
			return	sRankName[Rank]; //Uses Rank getter
		}
	}

	//This will work out the Rank number
	//check https://msdn.microsoft.com/en-us/library/h6zfzfy7(v=vs.100).aspx
	//for how modulo works
	public	int	Rank {
		get	{
			return	CardID % sRankName.Length; //Use Length of array to clamp
		}
	}
	//This will work out the suit and also ensure it does not exceed the range 0-3
	public	int	Suit {
		get	{
			return	(CardID/sRankName.Length) % sSuiteName.Length;
		}
	}

	//Make a CardID given a Suit and Rank
	//Values clamped with Suit Array size (typically 4) 
	//and Rank Array size (typically 13) if full deck
	public	int	MakeCardID(int vSuit, int vRank) {
		//Work out where Cards of that Suit would start (Suit*13)
		int	tCardID = ((vSuit % sSuiteName.Length) * RankName.Length);
		//Add in clamped Rank
		tCardID += vRank % RankName.Length;
		return	tCardID;
	}

	private	Sprite	FrontSprite;	//Sprite to use for the front of the card, now assigned by code so hide it from Inspector
	private	Sprite	BackSprite;		//Sprite to use for the back of the card,  now assigned by code so hide it from Inspector

	public	bool	ShowCard;		//To Flip the card (show its value) set the ShowCard State true, also works in IDE via inspector
									//Potential improvement, add a setter method so external code does not need to access Card variables directly
									//Would also allow for the possibilty to not use update to check card each render, as setter could be used to update 
									//SR.sprite when it updates the state variable

	private	SpriteRenderer	SR;		//Copy of reference to SpriteRenderer stored here,for rapid access later 
	
	private	int	CardID;				//Unique ID of this card from 0-51 i.e. 4 Suits x 13 Ranks


	//Awake() Unity calls this before Start() see https://unity3d.com/learn/tutorials/topics/scripting/awake-and-start
	void	Awake() {
		SR = GetComponent<SpriteRenderer> ();	//Grab Copy of sprite renderer component
		CardID=-1;			//This is an invalid CardID, we can use this to spot if Card has not been Intialised in Update
	}

	public	void	Init(int vCardID,Sprite vFrontSprite, Sprite vBackSprite) {
		ShowCard=false;					//Show back of card when it starts up
		CardID=vCardID;					//Remember Card ID, it is used to calcualte Suit & Rank
		FrontSprite = vFrontSprite;		//Assign Front & Back sprites so card shows
		BackSprite = vBackSprite;
		name = ToString ();				//Name it so it shows with name in Hierarchy, handy for debug
		SetCardSprite();
	}
	private	void	SetCardSprite() {		//Moved this to a method as its also used in Init()
		if (ShowCard) {						//Check which side of the card to show
			SR.sprite = FrontSprite;		//Show the front, if card does not show in game ensure FrontSprite is set in inspector.
		} else {
			SR.sprite = BackSprite;			//Show the back, if card does not show in game ensure BackSprite is set in inspector.
		}
	}

	public	bool	CardHidden {
		get {
			return	!gameObject.activeInHierarchy;
		}
		set {
			gameObject.SetActive(!value);
		}
	}

	//Update() Unity calls this before every render see see https://unity3d.com/learn/tutorials/topics/scripting/awake-and-start
	void Update () {
		if (CardID >= 0) {		//If Card was not initalised this will be -1, handy check as otherwise it simply wont show
			SetCardSprite();		//Pick correct sprite to show
		} else {
			Debug.Log ("Error Card has not been Initalised");			//In case I forget to initialise the card
		}
	}


	public override string ToString ()	{				//This will override the ToSting() method check https://msdn.microsoft.com/en-us/library/ms173154.aspx
		return string.Format ("{0} of {1}", SuitName, RankName);		//To provide a usefull description of the card, rather than the default one provided by the base class
	}
}
