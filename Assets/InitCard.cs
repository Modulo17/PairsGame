using UnityEngine;
using System.Collections;

public class InitCard : MonoBehaviour {

	public	Sprite	FrontSprite;
	public	Sprite	BackSprite;


	// Use this for initialization
	void Start () {
		Card	tCard = GetComponent<Card> ();
		tCard.Init(0,FrontSprite,BackSprite);
	}
}
