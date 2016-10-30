using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PrintInstructions : MonoBehaviour {

	public	Text	Console;

	private	Deck	mDeck;	

	// Use this for initialization
	void Start () {
		Console.text = "Ready>";
		mDeck = GetComponent<Deck> ();
	}


	//Print Text to UI
	public	void	AddText(string vText) {
		Console.text += vText + "\n";
	}

	//Clear Current text
	public void ClearText() {
		Console.text = "";
	}
}
