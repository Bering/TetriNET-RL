using UnityEngine;
using System.Collections;


public class TetrominoController : MonoBehaviour
{

	[SerializeField]
	protected float inputCooldown = 0.1f;
	[SerializeField]
	protected float lastInputTime = 0;


	protected Tetromino tetromino = null;
	protected PlayArea playArea;
	protected bool dropping = false;
	protected bool looping = false;


	Tetromino SpawnNewTetromino()
	{
		Tetromino t = Tetromino.CreateRandomTetromino();
		t.Spawn (playArea);
		dropping = false;
		lastInputTime = 0;

		return t;
	}


	void Awake()
	{
		playArea = GetComponent<PlayArea> ();
	}


	void Start()
	{
		StartCoroutine ("Loop");
	}


	protected IEnumerator Loop()
	{
		looping = true;
		Debug.Log ("Loop started");
		// TODO: Play start sound
		// TODO: Start soundtrack
		yield return new WaitForSeconds (1f);

		while (looping) {
			tetromino = SpawnNewTetromino ();

			while (tetromino != null) {
				yield return new WaitForSeconds (1f);

				// TODO: MoveDown has as a side-effect of nullifying tetromino if it cannot move down. How about calling it TryMoveDown() or something...
				if (tetromino != null) {
					MoveDown ();
				}
			}

			// TODO: Play block stop sound

			// TODO: if (Options.tetriFast == false)
			yield return new WaitForSeconds (1f);
		}
	}


	void Update ()
	{
		if (tetromino == null) {
			return;
		}

		if (Time.time < lastInputTime + inputCooldown) {
			return;
		}

		if (dropping == true) {
			MoveDown ();
		}
		
		if (Input.GetAxis("Horizontal") < 0) {
			lastInputTime = Time.time;
			MoveLeft ();
		}
		else if (Input.GetAxis("Horizontal") > 0) {
			lastInputTime = Time.time;
			MoveRight ();
		}

		if (Input.GetAxis("Vertical") < 0) {
			lastInputTime = Time.time;
			MoveDown ();
		}

		if (Input.GetButtonDown("Submit")) {
			lastInputTime = Time.time;
			dropping = true;
		}

		if (Input.GetButtonDown("Fire1")) {
			lastInputTime = Time.time;
			RotateLeft ();
		}

		if (Input.GetButtonDown("Fire2")) {
			lastInputTime = Time.time;
			RotateRight ();
		}

	}


	protected void RemoveFromArea()
	{
		for (int n = 0; n < tetromino.blocks.Length; n++) {
			tetromino.playArea.SetBlockType (tetromino.blocks [n], 0);
		}
	}


	protected void Redraw()
	{
		for (int n = 0; n < tetromino.blocks.Length; n++) {
			tetromino.playArea.SetBlockType (tetromino.blocks [n], tetromino.spriteType);
		}
	}

	
	// This assumes that the block is not on the board. If it is, it will collide with itself.
	protected bool canMoveLeft()
	{
		int x, y;

		for (int n = 0; n < tetromino.blocks.Length; n++) {
			x = Mathf.RoundToInt (tetromino.blocks [n].x);
			y = Mathf.RoundToInt (tetromino.blocks [n].y);

			if (x == 0) {
				return false;
			}

			if (tetromino.playArea.GetBlockType (x-1, y) != 0)
				return false;
		}

		return true;
	}
	protected void MoveLeft()
	{
		RemoveFromArea ();

		if (canMoveLeft ()) {
			for (int n = 0; n < tetromino.blocks.Length; n++) {
				tetromino.blocks [n].x--;
			}
		}

		Redraw ();
	}


	// This assumes that the block is not on the board. If it is, it will collide with itself.
	protected bool canMoveRight()
	{
		int x, y;

		for (int n = 0; n < tetromino.blocks.Length; n++) {
			x = Mathf.RoundToInt (tetromino.blocks [n].x);
			y = Mathf.RoundToInt (tetromino.blocks [n].y);

			if (x == PlayArea.BlocksPerRow-1) {
				return false;
			}

			if (tetromino.playArea.GetBlockType (x+1, y) != 0)
				return false;
		}

		return true;
	}
	protected void MoveRight()
	{
		RemoveFromArea ();

		if (canMoveRight ()) {
			for (int n = 0; n < tetromino.blocks.Length; n++) {
				tetromino.blocks [n].x++;
			}
		}

		Redraw ();
	}


	// This assumes that the block is not on the board. If it is, it will collide with itself.
	protected bool canMoveDown()
	{
		int x, y;

		for (int n = 0; n < tetromino.blocks.Length; n++) {
			x = Mathf.RoundToInt (tetromino.blocks [n].x);
			y = Mathf.RoundToInt (tetromino.blocks [n].y);

			if (y == 0) {
				return false;
			}

			if (tetromino.playArea.GetBlockType (x, y-1) != 0)
				return false;
		}

		return true;
	}
	protected void MoveDown()
	{
		RemoveFromArea ();

		if (canMoveDown ()) {
			for (int n = 0; n < tetromino.blocks.Length; n++) {
				tetromino.blocks [n].y--;
			}

			Redraw ();
		}

		else {
			Redraw ();
			tetromino = null;
		}

	}


	protected bool canRotateLeft()
	{
		Vector2 test;
		
		for (int n = 0; n < tetromino.blocks.Length; n++) {

			test = Quaternion.AngleAxis (90, Vector3.forward) * (tetromino.blocks [n] - tetromino.blocks [0]);
			test.x = Mathf.RoundToInt (test.x) + tetromino.blocks [0].x;
			test.y = Mathf.RoundToInt (test.y) + tetromino.blocks [0].y;


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
	protected void RotateLeft()
	{
		RemoveFromArea ();

		if (canRotateLeft ()) {
			for (int n = 1; n < tetromino.blocks.Length; n++) {
				tetromino.blocks [n] = Quaternion.AngleAxis (90, Vector3.forward) * (tetromino.blocks [n] - tetromino.blocks [0]);
				tetromino.blocks [n].x = Mathf.RoundToInt (tetromino.blocks[n].x) + tetromino.blocks [0].x;
				tetromino.blocks [n].y = Mathf.RoundToInt (tetromino.blocks[n].y) + tetromino.blocks [0].y;
			}
		}

		Redraw ();
	}

	protected bool canRotateRight()
	{
		Vector2 test;

		for (int n = 0; n < tetromino.blocks.Length; n++) {

			test = Quaternion.AngleAxis (-90, Vector3.forward) * (tetromino.blocks [n] - tetromino.blocks [0]);
			test.x = Mathf.RoundToInt (test.x) + tetromino.blocks [0].x;
			test.y = Mathf.RoundToInt (test.y) + tetromino.blocks [0].y;


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
	protected void RotateRight()
	{
		RemoveFromArea ();

		if (canRotateRight ()) {
			for (int n = 1; n < tetromino.blocks.Length; n++) {
				tetromino.blocks [n] = Quaternion.AngleAxis (-90, Vector3.forward) * (tetromino.blocks [n] - tetromino.blocks [0]);
				tetromino.blocks [n].x = Mathf.RoundToInt (tetromino.blocks[n].x) + tetromino.blocks [0].x;
				tetromino.blocks [n].y = Mathf.RoundToInt (tetromino.blocks[n].y) + tetromino.blocks [0].y;
			}
		}

		Redraw ();
	}


}
