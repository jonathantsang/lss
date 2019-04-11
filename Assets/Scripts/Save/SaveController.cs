using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

	public static SaveController instance;

	// Dep
	DataController dataController;

	float counter = 0;
	float limit = 2.5f;

	// Use this for initialization
	void Start () {
		// Singleton Behaviour
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
		DontDestroyOnLoad(gameObject);

		dataController = GameObject.FindGameObjectWithTag ("DataController").GetComponent<DataController> ();
		//SU = GameObject.FindGameObjectWithTag ("ShopUnlocked").GetComponent<ShopUnlocked> ();
		Load ();
	}
	
	// Update is called once per frame
	void Update () {
		// Spinlock lol not really
		/*
		if (!IC) {
			IC = GameObject.FindGameObjectWithTag ("InventoryController").GetComponent<InventoryController> ();
		}
		if (!SU) {
			SU = GameObject.FindGameObjectWithTag ("ShopUnlocked").GetComponent<ShopUnlocked> ();
		}
		*/
		if (counter >= limit) {
			Save ();
			counter = 0;
		}
		counter += Time.deltaTime;
	}

	public void Save(){
		// print ("saved");
		// save hardcoded value
		/*
		if (IC && SU) {
			// print ("saved");
			SaveLoadManager.SaveData (IC.getCollected (), IC.getRecipesUnlocked(), IC.getStats(), SU.getShopUnlockedList(), IC.getCurrency(), IC.getTotalOpened(), IC.getHowManyToOpen());
		} else {
			print ("save failed due to linking");
		}
		*/
		if (dataController != null) {
			print ("saving");
			SaveLoadManager.SaveData (dataController.getTotalMoney (), dataController.getMoneyMakers (), dataController.getUpgrades (), dataController.getMiscStats ());
		}

	}

	public void Load(){
		print ("load");
		SaveData loadedStats = SaveLoadManager.LoadData ();

		// Load from stats
		// print(loadedStats.Currency);
		// print (loadedStats.Collected);
		// IC.LoadInventory (loadedStats.Collected, loadedStats.RecipesUnlocked, loadedStats.Stats, loadedStats.ShopUnlocked, loadedStats.Currency);
		dataController.loadInventory(loadedStats.money, loadedStats.moneyMakers, loadedStats.upgrades, loadedStats.miscStats);

		// Loads data
		// print(loadedStats);
	}
}
