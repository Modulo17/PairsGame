using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCastDemo : MonoBehaviour {

	private	Text		mDebugText;


	void Start() {
	}

	// Update is called once per frame
	void Update () {
		Ray tRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//Make a ray from screen position into the game scene
		RaycastHit2D	tHit = Physics2D.Raycast (tRay.origin,tRay.direction);		//Cast ray, if it hits a game object we will know, NB only first collision is reported
		if (tHit.collider != null) {
			Vector3	tA = new Vector3 (tHit.point.x - 0.5f, tHit.point.y - 0.5f, 0.0f);
			Vector3	tB = new Vector3 (tHit.point.x + 0.5f, tHit.point.y - 0.5f, 0.0f);
			Vector3	tC = new Vector3 (tHit.point.x + 0.5f, tHit.point.y + 0.5f, 0.0f);
			Vector3	tD = new Vector3 (tHit.point.x - 0.5f, tHit.point.y + 0.5f, 0.0f);
			Debug.DrawLine (tA,tC, Color.red);
			Debug.DrawLine (tB,tD, Color.red);
			Card tCS = tHit.collider.gameObject.GetComponent<Card> ();
			PopScale tPS = tCS.GetComponent<PopScale> ();
			Debug.DrawLine(tRay.origin,tHit.point,Color.green);
			tPS.DoPopScale ();
			if (Input.GetMouseButtonDown (0)) {
				tCS.ShowCard = !tCS.ShowCard;
			}
			//tCS.Hover ();
		} else {
			Debug.DrawRay(tRay.origin,tRay.direction*30.0f,Color.yellow,0.25f,true);
		}
	}
}