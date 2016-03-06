using UnityEngine;
using System.Collections;


public class TetrominoController : MonoBehaviour
{

	public float inputCooldown = 0.1f;
	protected float lastInputTime = 0;

	protected Tetromino tetromino = null;
	protected PlayArea playArea;
	protected bool dropping = false;


	Tetromino SpawnNewTetromino()
	{
		Tetromino t = Tetromino.CreateRandomTetromino();
		t.Spawn (playArea);
		if (t.hasLanded) {
			return null;
		}

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
		bool lost = false;

		Debug.Log ("Loop started");

		// TODO: Play start sound
		// TODO: Start music

		yield return new WaitForSeconds (1f);

		while (true) {
			tetromino = SpawnNewTetromino ();

			if (tetromino == null) {
				lost = true;
				break;
			}

			while (!tetromino.hasLanded) {
				yield return new WaitForSeconds (1f);

				tetromino.MoveDown ();
			}

			// TODO: Play block stop sound

			playArea.ClearFullLines ();

			// TODO: if (Options.tetriFast == false)
			yield return new WaitForSeconds (1f);
		}

		// TODO: Stop music

		if (lost) {
			// TODO: Play the "You Lost" sound/song
			Debug.Log ("You lost!");
		}
		else {
			// TODO: Play the "You Win!" sound/song
			Debug.Log ("You win!");
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
			tetromino.MoveDown ();
		}
		
		if (Input.GetAxis("Horizontal") < 0) {
			lastInputTime = Time.time;
			tetromino.MoveLeft ();
		}
		else if (Input.GetAxis("Horizontal") > 0) {
			lastInputTime = Time.time;
			tetromino.MoveRight ();
		}

		if (Input.GetAxis("Vertical") < 0) {
			lastInputTime = Time.time;
			tetromino.MoveDown ();
		}

		if (Input.GetButtonDown("Submit")) {
			lastInputTime = Time.time;
			dropping = true;
		}

		if (Input.GetButtonDown("Fire1")) {
			lastInputTime = Time.time;
			tetromino.RotateLeft ();
		}

		if (Input.GetButtonDown("Fire2")) {
			lastInputTime = Time.time;
			tetromino.RotateRight ();
		}

	}

}
