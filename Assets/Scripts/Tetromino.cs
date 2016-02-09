using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tetromino : MonoBehaviour
{

	public enum Type { square, t, line, s, z,  l, j }

	protected float h, v;

	Vector3 targetPos;


	void Start ()
	{
		targetPos = transform.position;
	}
	

	void Update ()
	{
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		if (h < 0) {
			MoveLeft ();
		}
		else if (h > 0) {
			MoveRight ();
		}

		if (v < 0) {
			MoveDown ();
		}
		else if (v > 0) {
			RotateRight();
		}

	}


	protected void MoveLeft()
	{
		targetPos.x = 1f;
	}


	protected void MoveRight()
	{
		targetPos.x += 1f;
	}


	protected void MoveDown()
	{
		targetPos.y += 1f;
	}


	protected void RotateRight()
	{
		
	}
	
	
	protected void RotateLeft()
	{
		
	}


	protected void Dropdown()
	{
		// TODO: call whereToDropDown() on each block in the tetromino
		// TODO: use the highest value as target
		targetPos.y = whereToDropDown();
	}


	protected float whereToDropDown()
	{
		for (float y = transform.position.y; y > 0; y--) {
			/*if (!PlayArea.isPositionEmpty(transform.position.x, y)) {
				return y;
			}*/
		}

		return 0;
	}

}

