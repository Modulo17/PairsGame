using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCastDemo : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Ray tRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//Make a ray from screen position into the game scene
		RaycastHit2D	tHit = Physics2D.Raycast (tRay.origin,tRay.direction);		//Cast ray, if it hits a game object we will know, NB only first collision is reported
		if (tHit.collider != null) {								//If the ray hit a collider it would be stored here
			Card tCS = tHit.collider.gameObject.GetComponent<Card> ();		//Get a refernece to the Card component on the hit game object
			DrawCrossHairs (tHit.point);								//Show crosshairs
			Debug.DrawLine(tRay.origin,tHit.point,Color.green);			//Draw Line to where it hit
			tCS.GetComponent<PopScale> ().DoPopScale ();				//Make it pop up (scale) to show it is hit
			if (Input.GetMouseButtonDown (0)) {							//If we have a mouse click flip it
				tCS.ShowCard = !tCS.ShowCard;
			}
		} else {
			Debug.DrawRay(tRay.origin,tRay.direction*30.0f,Color.yellow,0.25f,true);		//Draw a ray to show where its going
		}
	}


	void	DrawCrossHairs (Vector3 vLocation) {
		Vector3	tA = new Vector3 (vLocation.x - 0.5f, vLocation.y - 0.5f, 0.0f);		//Top left
		Vector3	tB = new Vector3 (vLocation.x + 0.5f, vLocation.y - 0.5f, 0.0f);		//Bottom left
		Vector3	tC = new Vector3 (vLocation.x + 0.5f, vLocation.y + 0.5f, 0.0f);		//To right
		Vector3	tD = new Vector3 (vLocation.x - 0.5f, vLocation.y + 0.5f, 0.0f);		//Bottom right
		Debug.DrawLine (tA,tC, Color.red);												//Draw crosshair
		Debug.DrawLine (tB,tD, Color.red);
	}
}