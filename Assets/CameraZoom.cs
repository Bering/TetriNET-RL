using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	SpriteRenderer r;

	void Start ()
	{
		r = GetComponent<SpriteRenderer> ();
	}


	void Update()
	{
		if(r.isVisible) {
			Destroy (this.gameObject);
		}

		Camera.main.orthographicSize += 0.1f;
	}

}
