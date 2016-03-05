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

			while (!tetromino.hasLanded) {
				yield return new WaitForSeconds (1f);

				tetromino.MoveDown ();
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
