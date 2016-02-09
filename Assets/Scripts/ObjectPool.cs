using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class ObjectPool
{
	protected GameObject prefab;
	protected List<GameObject> pool;

	public int poolSize;
	public bool preloadAmount;
	public bool isOverflowAllowed;


	public ObjectPool(int poolSize, bool isOverflowAllowed)
	{
		pool = new List<GameObject> ();
	}

	public GameObject Spawn()
	{
		return Spawn (Vector3.zero, Quaternion.identity);
	}

	public GameObject Spawn(Vector3 position)
	{
		return Spawn (position, Quaternion.identity);
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation) {

		// Return a free object from the pool if there is one
		GameObject go = FindFree ();
		if (go != null) {
			go.transform.position = position;
			go.transform.rotation = rotation;
			go.SetActive (true);
			return go;
		}

		if (pool.Count >= poolSize && !isOverflowAllowed) {
			Debug.LogException(new UnityException("Pool full!"));
		}

		return AddToPool (position, rotation);
	}


	protected GameObject FindFree()
	{
		foreach(GameObject go in pool) {
			if (!go.activeSelf) {
				return go;
			}
		}

		return null;
	}


	protected GameObject AddToPool(Vector3 position, Quaternion rotation)
	{
		GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;
		pool.Add(go);
		return go;
	}


	static void DeSpawn(GameObject go)
	{
		go.SetActive (false);
	}


}
