using UnityEngine;

public class Tetromino_I : Tetromino
{
	public Tetromino_I()
	{
		spriteType = 1;
		type = Tetromino.Types.I;
		blocks = new Vector2[] {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(0,2),
			new Vector2(0,3)
		};
	}
}
