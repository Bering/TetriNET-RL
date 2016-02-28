using UnityEngine;
using System.Collections;

public class TetrominoController : MonoBehaviour
{

	public Tetromino tetromino;


	void Spawn(PlayArea playArea)
	{
		tetromino = Tetromino.SpawnRandomTetromino(playArea);
	}


	void Start()
	{
		//Spawn (GameObject.FindObjectOfType<PlayArea> ());
	}

	/*
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
*/

	protected void MoveLeft()
	{
	}


	protected void MoveRight()
	{
	}


	protected void MoveDown()
	{
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
