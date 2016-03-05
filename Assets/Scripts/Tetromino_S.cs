using UnityEngine;

public class Tetromino_S : Tetromino
{
	public Tetromino_S()
	{
		spriteType = 1;
		type = Tetromino.Types.S;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(-1,1),
			new Vector2(0,1)
		};
	}
}
