using UnityEngine;
using System.Collections;

public class PopScale : MonoBehaviour {

	private	bool	mLock=false;			//Ensure this cannot start again until the last one completes 

	public	bool	DoPopScale(float vSize=1.2f) {		//Helper method to call Coroutine
		if (!mLock) {		//Only allow it to run if not already running
			StartCoroutine(DoPopScaleCoroutine(vSize));
			return	true;
		}
		return	false;
	}

	//A co-routine to make a smooth move between From.transform.position TO To.transform.position, taking TimeTaken seconds
	private	IEnumerator	DoPopScaleCoroutine(float vSize) {
		float	tCurrentTime = 0.0f;
		Vector3 tNormalScale= transform.localScale;
		Vector3 tPopScale = transform.localScale*vSize;
		mLock = true;
		while (tCurrentTime<1.0f) {								//Run this over 1 second
			transform.localScale = Vector3.Lerp (tPopScale
				,tNormalScale
				,tCurrentTime); //mCurrentTime increases each iteration
			//we want it to take TimeTaken seconds, so we divide by this
			//to get a value between 0.0-1.0 for the Lerp
			tCurrentTime += Time.deltaTime;		//Time.deltaTime is the time since Iteration was last called by Unity, by adding
												//it to our current time we know how far along we should be
			yield	return	null;			//Return once per iteration, until we have arrived
		}
		transform.localScale = tNormalScale;		//Make sure scale is set back to what it was, as Lerp may slightly undershoot
		mLock = false;					//Its finished so signal it can run again
	}
}
