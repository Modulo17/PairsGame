using UnityEngine;
using System.Collections;

public class AutoSize : MonoBehaviour {


	//Scale a sprite to cover the whole screen

	SpriteRenderer	mSR;

	Vector2	mSize;

	Vector3	mPosition;

	// Use this for initialization
	void Start () {
		mSR = GetComponent<SpriteRenderer> ();	//Get SR component
		mSize=new Vector2 (mSR.bounds.extents.x, mSR.bounds.extents.y);
		mPosition = new Vector3(0,0,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		float	tHeight = Camera.main.orthographicSize;
		float	tWidth = tHeight * Camera.main.aspect;
		transform.position = mPosition;
		transform.localScale = new Vector2 (tWidth/ mSize.x , tHeight/ mSize.y );
	}
}
