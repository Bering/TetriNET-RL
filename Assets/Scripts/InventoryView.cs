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
		inventorySlotViews = new SpriteRenderer[18];
	}


	void Start () {
		SpawnRenderers ();
	}


	void SpawnRenderers()
	{
		GameObject go = null;

		for (int i = 0; i < inventory.inventorySlots.Length; i++) {

			go = Instantiate(new GameObject(), new Vector3 (FirstX + (i * Spread), FirstY), Quaternion.identity) as GameObject;
			go.name = "Inventory Slot #" + i;
			go.transform.SetParent(transform, false);

			inventorySlotViews[i] = go.AddComponent<SpriteRenderer> ();
		}
	}


	public void UpdateRenderers()
	{
		for (int i = 0; i < inventory.inventorySlots.Length; i++) {
			// TODO Move the sprites lists into a Theme object
			inventorySlotViews [i].sprite = GameObject.Find ("PlayArea (Player 1)").GetComponent<PlayAreaView> ().blocksSprites[inventory.inventorySlots [i]];
		}
	}


}
