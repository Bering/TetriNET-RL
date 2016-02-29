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

	
	protected void RotateRight()
	{
	}


	protected void RotateLeft()
	{
	}


}
