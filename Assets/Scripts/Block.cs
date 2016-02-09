using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

	protected PlayArea playArea;


	void Start ()
	{
		playArea = transform.parent.gameObject.GetComponent<PlayArea> ();
		StartCoroutine ("GravityEffectForShow");
	}


	protected IEnumerator  GravityEffectForShow()
	{
		Vector3 pos = transform.position;
		float bottom = playArea.transform.position.y;

		while(true)
		{
			pos.y -= GetComponent<SpriteRenderer>().bounds.size.y;

			if (pos.y < bottom) {
				Destroy (gameObject);
			}

			transform.position = pos;

			yield return new WaitForSeconds (1);
		}
	}

}
