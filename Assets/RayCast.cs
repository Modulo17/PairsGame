using UnityEngine;
using System.Collections;


//This script is attached to the Main Camera
//The solution file contains some ideas
//on variable names and comments I would use
//to describe how it works


//RayCast class is used to find which, if any, GameObject in the scene was clicked on
//It starts by generating a ray from the mouse position ScreenPointToRay from the main (and only) camera in the scene
//The ray is cast and if an GameObject (which must have a collider for this to work) is found this is acted on

public class RayCast : MonoBehaviour {




	//This is called every update, right now not an issue as code only runs if there was a mousedown event
	//However beware excessive processing during Update() can really slow the game down
	void Update () {
		if (Input.GetMouseButtonDown (0)) {			//Only run raycast if mouse down has occured since last Update
			Ray tMousePointerRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//Make a ray from the mouse pointer using the main camera
			RaycastHit2D	tMousePointerRayHit = Physics2D.Raycast (tMousePointerRay.origin,tMousePointerRay.direction);	//Cast the ray into the gameworld from the mouse position, pointing along direction (into the screen)
			if (tMousePointerRayHit.collider != null) {											//If collider is null we did not hit anything, otherise it wil be a reference the collider on the GameObject we hit
				GameObject tObjectHit = tMousePointerRayHit.collider.gameObject;				//At this point we know we have hit a collider on a GameObject, so get the game object
				Debug.Log (string.Format("Ray Cast Hit {0}",tObjectHit.name));					//Show what we have hit in the console Window, handy for Debugging
				Card tCardHit=tObjectHit.GetComponent<Card>();									//Get the Card Script object, if it does not have one, if this returns null its either not a card, or we forgot to attach the Card Script it
				if (tCardHit != null) {															//Ensure the object we hit is a Card, as we may have other items in the scene too later	
					tCardHit.ShowCard = !tCardHit.ShowCard;										//Flip the card by changing its state directly
																								//to ensure safer operation it may be better to add a card method to do this rather than set the variable here
				} else {
					Debug.Log (string.Format("WARNING: Ray Cast Hit Object {0} does not have a Card component",tObjectHit.GetType().Name));		//Perhaps we forgot to add one?
				}
			}
		}
	}
}
