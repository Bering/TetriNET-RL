using UnityEngine;

public class Tetromino_J : Tetromino
{
	public Tetromino_J()
	{
		spriteType = 3;
		type = Tetromino.Types.J;
		blocks = new Vector2[] {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(0,2),
			new Vector2(-1,2)
		};
	}
}
