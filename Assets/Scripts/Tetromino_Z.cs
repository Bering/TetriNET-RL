using UnityEngine;

public class Tetromino_Z : Tetromino
{
	public Tetromino_Z()
	{
		spriteType = 5;
		type = Tetromino.Types.Z;
		blocks = new Vector2[] {
			new Vector2(0,0),
			new Vector2(-1,0),
			new Vector2(0,1),
			new Vector2(1,1)
		};
	}
}
