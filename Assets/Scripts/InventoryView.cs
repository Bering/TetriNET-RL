using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Inventory))]
public class InventoryView : MonoBehaviour {


	protected Inventory inventory;
	protected SpriteRenderer[] inventorySlotViews;


	public float FirstX, FirstY, Spread;


	void Awake()
	{
		inventory = GetComponent<Inventory> ();
	}


	void Start () {
		SpawnRenderers ();
	}


	void SpawnRenderers()
	{
		inventorySlotViews = new SpriteRenderer[18];

		GameObject go = null;

		for (int i = 0; i < inventory.inventorySlots.Length; i++) {

			go = new GameObject("Inventory Slot #" + i);
			go.transform.SetParent(transform);
			go.transform.position = transform.position + new Vector3 (FirstX + (i * Spread), FirstY);
			go.transform.rotation = Quaternion.identity;

			inventorySlotViews[i] = go.AddComponent<SpriteRenderer> ();
		}
	}


	public void UpdateRenderers()
	{
		for (int i = 0; i < inventory.inventorySlots.Length; i++) {
			inventorySlotViews [i].sprite = Theme.current.blocksSprites16[inventory.inventorySlots [i]];
		}
	}


}
