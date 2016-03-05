using UnityEngine;
using System.Collections;

public class Tetromino
{
	public enum Types { O, T, I, S, Z, J, L, Count }

	public Types type;
	public int spriteType;
	public Vector2[] blocks;
	public PlayArea playArea;
	public bool hasLanded;


	public static Tetromino CreateRandomTetromino()
	{
		int typeInt = Random.Range (0, (int)Types.Count);

		switch(typeInt) {
		case 0: return new Tetromino_O();
		case 1: return new Tetromino_T();
		case 2: return new Tetromino_I();
		case 3: return new Tetromino_S();
		case 4: return new Tetromino_Z();
		case 5: return new Tetromino_J();
		case 6: return new Tetromino_L();
		default:
			throw new UnityException ("Tetromino.SpawnRandomTetromino() : Random.Range returned a number out of range...");
		}
	}


	public void Spawn (PlayArea playArea)
	{
		hasLanded = false;
		this.playArea = playArea;
		Vector2 spawnPoint = new Vector2 (PlayArea.spawnX, PlayArea.NumberOfRows - 1);

		for (int i = 0; i < blocks.Length; i++) {
			blocks[i] = spawnPoint - blocks[i];

			playArea.SetBlockType (Mathf.FloorToInt(blocks[i].x), Mathf.FloorToInt(blocks[i].y), spriteType);
		}
	}


	protected void RemoveFromArea()
	{
		for (int n = 0; n < blocks.Length; n++) {
			playArea.SetBlockType (blocks [n], 0);
		}
	}


	protected void Redraw()
	{
		for (int n = 0; n < blocks.Length; n++) {
			playArea.SetBlockType (blocks [n], spriteType);
		}
	}


	// This assumes that the block is not on the board. If it is, it will collide with itself.
	public bool canMoveLeft()
	{
		int x, y;

		for (int n = 0; n < blocks.Length; n++) {
			x = Mathf.RoundToInt (blocks [n].x);
			y = Mathf.RoundToInt (blocks [n].y);

			if (x == 0) {
				return false;
			}

			if (playArea.GetBlockType (x-1, y) != 0)
				return false;
		}

		return true;
	}
	public void MoveLeft()
	{
		RemoveFromArea ();

		if (canMoveLeft ()) {
			for (int n = 0; n < blocks.Length; n++) {
				blocks [n].x--;
			}
		}

		Redraw ();
	}


	// This assumes that the block is not on the board. If it is, it will collide with itself.
	public bool canMoveRight()
	{
		int x, y;

		for (int n = 0; n < blocks.Length; n++) {
			x = Mathf.RoundToInt (blocks [n].x);
			y = Mathf.RoundToInt (blocks [n].y);

			if (x == PlayArea.BlocksPerRow-1) {
				return false;
			}

			if (playArea.GetBlockType (x+1, y) != 0)
				return false;
		}

		return true;
	}
	public void MoveRight()
	{
		RemoveFromArea ();

		if (canMoveRight ()) {
			for (int n = 0; n < blocks.Length; n++) {
				blocks [n].x++;
			}
		}

		Redraw ();
	}


	// This assumes that the block is not on the board. If it is, it will collide with itself.
	public bool canMoveDown()
	{
		int x, y;

		for (int n = 0; n < blocks.Length; n++) {
			x = Mathf.RoundToInt (blocks [n].x);
			y = Mathf.RoundToInt (blocks [n].y);

			if (y == 0) {
				return false;
			}

			if (playArea.GetBlockType (x, y-1) != 0)
				return false;
		}

		return true;
	}
	public void MoveDown()
	{
		RemoveFromArea ();

		if (canMoveDown ()) {
			for (int n = 0; n < blocks.Length; n++) {
				blocks [n].y--;
			}
		} else {
			hasLanded = true;
		}

		Redraw ();
	}


	public bool canRotateLeft()
	{
		Vector2 test;

		for (int n = 0; n < blocks.Length; n++) {

			test = Quaternion.AngleAxis (90, Vector3.forward) * (blocks [n] - blocks [0]);
			test.x = Mathf.RoundToInt (test.x) + blocks [0].x;
			test.y = Mathf.RoundToInt (test.y) + blocks [0].y;


			if (test.x < 0) {
				return false;
			}
			if (test.x > PlayArea.BlocksPerRow - 1) {
				return false;
			}

			if (test.y < 0) {
				return false;
			}
			if (test.y > PlayArea.NumberOfRows - 1) {
				return false;
			}

			if (playArea.GetBlockType (Mathf.FloorToInt (test.x), Mathf.FloorToInt (test.y)) != 0) {
				return false;
			}

		}

		return true;
	}
	public void RotateLeft()
	{
		RemoveFromArea ();

		if (canRotateLeft ()) {
			for (int n = 1; n < blocks.Length; n++) {
				blocks [n] = Quaternion.AngleAxis (90, Vector3.forward) * (blocks [n] - blocks [0]);
				blocks [n].x = Mathf.RoundToInt (blocks[n].x) + blocks [0].x;
				blocks [n].y = Mathf.RoundToInt (blocks[n].y) + blocks [0].y;
			}
		}

		Redraw ();
	}

	public bool canRotateRight()
	{
		Vector2 test;

		for (int n = 0; n < blocks.Length; n++) {

			test = Quaternion.AngleAxis (-90, Vector3.forward) * (blocks [n] - blocks [0]);
			test.x = Mathf.RoundToInt (test.x) + blocks [0].x;
			test.y = Mathf.RoundToInt (test.y) + blocks [0].y;

			// TODO: Refactor these IFs into isBlockValid() and use it in all the can() functions in here

			if (test.x < 0) {
				return false;
			}
			if (test.x > PlayArea.BlocksPerRow - 1) {
				return false;
			}

			if (test.y < 0) {
				return false;
			}
			if (test.y > PlayArea.NumberOfRows - 1) {
				return false;
			}

			if (playArea.GetBlockType (Mathf.FloorToInt (test.x), Mathf.FloorToInt (test.y)) != 0) {
				return false;
			}

		}

		return true;
	}
	public void RotateRight()
	{
		RemoveFromArea ();

		if (canRotateRight ()) {
			for (int n = 1; n < blocks.Length; n++) {
				blocks [n] = Quaternion.AngleAxis (-90, Vector3.forward) * (blocks [n] - blocks [0]);
				blocks [n].x = Mathf.RoundToInt (blocks[n].x) + blocks [0].x;
				blocks [n].y = Mathf.RoundToInt (blocks[n].y) + blocks [0].y;
			}
		}

		Redraw ();
	}


}
