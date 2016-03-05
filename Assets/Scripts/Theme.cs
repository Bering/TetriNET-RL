using UnityEngine;
using System.Collections.Generic;

public class Theme : MonoBehaviour
{
	static public Theme current;

	public List<Sprite> blocksSprites16;
	public List<Sprite> blocksSprites8;

	public Sprite playAreaBackground16;
	public Sprite playAreaBackground8;


	void Awake()
	{
		current = this;
	}


}

