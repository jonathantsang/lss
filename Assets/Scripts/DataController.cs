﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MoneyMaker {
	public long level; // ex. 4

	public long price; // ex. 10
	public long production; // ex. 100

	// Scale factors on the next levels (float, round int based on mathf)
	public float priceScale; // ex. 1.1, 10 -> 11
	public float productionScale; // ex. 1.1 100 -> 110

	public int waitTime; // ex. 4 is 4 seconds to wait for the button

	public bool mutex; // ex. true, taken, false, available

	// Might want scales to be bounded to be more dynamic
	// Levels 0 - 100: scale 1.2, 1.5
	// Levels 100 - 1000: scale 2.0, 1.2
	// etc.

	public MoneyMaker(){
		level = 0;
		price = 1;
		production = 1;
		priceScale = 1.1f;
		productionScale = 1.1f;
		waitTime = 5;
		mutex = false;
	}
}

[System.Serializable]
public class Upgrade {

	// Name of it, for debugging
	public string name;

	// Cost of upgrade
	public long cost;

	// If the upgrade has been bought
	public int bought;

	// Short description of achievement
	public string description;

	public Upgrade(){
		name = "unamed";
		cost = 1;
		bought = 0;
		description = "Description";
	}
}

public class DataController : MonoBehaviour {

	// Singleton
	public static DataController instance = null;

	// Stats
	long money = 0;

	// Store data on the money makers
	MoneyMaker[] moneyMakers;

	// Store data on the upgrades
	Upgrade[] upgrades;

	// Misc stats
	long[] miscStats;
	// [0] total taps
	// [1] time date stamp 

	// Exclusives
	long exclusives;
	int exclusivesWorth; // upgraded later

	void Awake(){
		//Check if instance already exists
		if (instance == null)
			instance = this;
		else if (instance != this) {
			// Check which has FRESHER data
			// race in checking who is the singleton, but only one CORRECT singleton
			// For now, use the fact if it is not already instantiated fields
			if (moneyMakers == null || upgrades == null) {
				Destroy(gameObject);
			}
		}
		DontDestroyOnLoad(this.gameObject);

		if (moneyMakers == null) {
			moneyMakers = new MoneyMaker[6];
			// Init each item
			for(int i = 0; i < moneyMakers.Length; i++){
				moneyMakers [i] = new MoneyMaker ();
			}

			// Initialize the money makers
			initializeMoneyMakers ();
		}

		if (upgrades == null) {
			upgrades = new Upgrade[6];
			// Init each item
			for(int i = 0; i < upgrades.Length; i++){
				upgrades [i] = new Upgrade ();
			}

			// initialize the upgrades
			initializeUpgrades ();
		}


		if (miscStats == null) {
			// Misc stats/achievements
			// 0 total clicks
			// 1 time
			miscStats = new long[10];
			miscStats [1] = System.DateTime.Now.ToFileTime ();
		}
	}

	// Use this for initialization
	void Start () {
		// money = 100000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initializeMoneyMakers(){
		// this is called at each scene change, load from dataController values instead
		moneyMakers [0].level = 1;
		moneyMakers [0].price = 1;
		moneyMakers [0].production = 1;
		moneyMakers [0].priceScale = 1.1f;
		moneyMakers [0].productionScale = 1.1f;
		moneyMakers [0].waitTime = 5;
		moneyMakers [0].mutex = false;

		moneyMakers [1].level = 0;
		moneyMakers [1].price = 10;
		moneyMakers [1].production = 10;
		moneyMakers [1].priceScale = 1.1f;
		moneyMakers [1].productionScale = 1.1f;
		moneyMakers [1].waitTime = 5;
		moneyMakers [1].mutex = false;

		moneyMakers [2].level = 0;
		moneyMakers [2].price = 100;
		moneyMakers [2].production = 100;
		moneyMakers [2].priceScale = 1.1f;
		moneyMakers [2].productionScale = 1.1f;
		moneyMakers [2].waitTime = 5;
		moneyMakers [2].mutex = false;

		moneyMakers [3].level = 0;
		moneyMakers [3].price = 1000;
		moneyMakers [3].production = 1000;
		moneyMakers [3].priceScale = 1.1f;
		moneyMakers [3].productionScale = 1.1f;
		moneyMakers [3].waitTime = 5;
		moneyMakers [3].mutex = false;

		moneyMakers [4].level = 0;
		moneyMakers [4].price = 10000;
		moneyMakers [4].production = 10000;
		moneyMakers [4].priceScale = 1.1f;
		moneyMakers [4].productionScale = 1.1f;
		moneyMakers [4].waitTime = 5;
		moneyMakers [4].mutex = false;

		moneyMakers [5].level = 0;
		moneyMakers [5].price = 100000;
		moneyMakers [5].production = 100000;
		moneyMakers [5].priceScale = 1.1f;
		moneyMakers [5].productionScale = 1.1f;
		moneyMakers [5].waitTime = 5;
		moneyMakers [5].mutex = false;
	}

	void initializeUpgrades(){
		// For now interns
		upgrades[0].name = "Intern #1";
		upgrades[0].cost = 10000;

		upgrades[1].name = "Intern #2";
		upgrades[1].cost = 100000;

		upgrades[2].name = "Intern #3";
		upgrades[2].cost = 10000000;

		upgrades[3].name = "Intern #4";
		upgrades[3].cost = 1000000000;

		upgrades[4].name = "Intern #5";
		upgrades[4].cost = 100000000000;

		upgrades[5].name = "Intern #6";
		upgrades[5].cost = 100000000000000;
	}

	// Load/Save
	public void loadInventory(long m, MoneyMaker[] mms, Upgrade[] us, long[] ms){
		money = m;
		// Money Makers Load
		moneyMakers = new MoneyMaker[6];

		if (mms == null) {
			for (int i = 0; i < 6; i++) {
				moneyMakers [i] = new MoneyMaker ();
			}
			moneyMakers [0].level = 1;
			initializeMoneyMakers ();
		} else {
			for (int i = 0; i < mms.Length; i++) {
				moneyMakers [i] = new MoneyMaker ();
				// Copy data over if not null
				moneyMakers[i].level = mms[i].level;
				moneyMakers[i].price = mms[i].price;
				moneyMakers[i].production = mms[i].production;
				moneyMakers[i].priceScale = mms[i].priceScale;
				moneyMakers[i].productionScale = mms[i].productionScale;
				moneyMakers[i].waitTime = mms[i].waitTime;
				// For now we assume it didn't stop half-way through exit, all mutex are fre
				moneyMakers[i].mutex = false; // mms[i].mutex;
			}
			if (moneyMakers [0].level == 0) {
				moneyMakers [0].level = 1;
			}
		}

		upgrades = new Upgrade[6];
		// Upgrades load
		if (us == null){
			for (int i = 0; i < 6; i++) {
				upgrades [i] = new Upgrade ();
			}
			initializeUpgrades ();
		}

		miscStats = new long[10];
		// Misc Stats load (FOR NOW)
		if (ms == null) {
			for (int i = 0; i < 10; i++) {
				miscStats [i] = 0;
			}
		} else {
			DateTime res = System.DateTime.FromFileTime (ms [1]);
			/*print ("Previous time was " + res.ToFileTime());
			print ("Now it is " + System.DateTime.Now.ToFileTime ());
			print ("Before: " + res.ToLongDateString ());
			print ("Now: " + System.DateTime.Now.ToLongDateString ());
			*/
		}


		// 1 time/date
		DateTime currentTime = System.DateTime.Now;
		miscStats [1] = currentTime.ToFileTime ();
		print (miscStats [1]);
	}

	// Public Operations on Money
	public void increaseMoney(long amount){
		money += amount;
	}

	public void decreaseMoney(long amount){
		money -= amount;
	}

	public void upgradeMoneyMaker(int id){
		moneyMakers [id].level += 1;
		moneyMakers [id].price = Mathf.RoundToInt(moneyMakers [id].price * moneyMakers [id].priceScale)+1; // minimum 1 upgrade
		moneyMakers [id].production = Mathf.RoundToInt(moneyMakers [id].production * moneyMakers [id].productionScale)+1; // minimum 1 upgrade
		// Do not edit scales for now
	}

	// Public Getters

	// Full Getters for Save
	public MoneyMaker[] getMoneyMakers(){
		return moneyMakers;
	}

	public Upgrade[] getUpgrades(){
		return upgrades;
	}

	public long[] getMiscStats(){
		return miscStats;
	}

	public long getTotalClicks(){
		return miscStats [0];
	}

	public long getTotalMoney(){
		return money;
	}

	// Money Makers
	public long getMoneyMakerLevel(int i){
		return moneyMakers [i].level;
	}

	public long getMoneyMakerPrice(int i){
		return moneyMakers [i].price;
	}

	public long getMoneyMakerProduction(int i){
		return moneyMakers [i].production;
	}

	public float getMoneyMakerPriceScale(int i){
		return moneyMakers [i].priceScale;
	}

	public float getMoneyMakerProductionScale(int i){
		return moneyMakers [i].productionScale;
	}

	public int getMoneyMakerWaitTime(int i){
		return moneyMakers [i].waitTime;
	}

	public bool getMoneyMakerMutex(int i){
		return moneyMakers [i].mutex;
	}

	// Upgrades
	public string getUpgradeName(int i){
		return upgrades [i].name;
	}

	public long getUpgradeCost(int i){
		return upgrades [i].cost;
	}

	public string getUpgradeDescription(int i){
		return upgrades [i].description;
	}

	public int getUpgradeBought(int i){
		return upgrades [i].bought;
	}

	// MiscStats
	public long getMiscStat(int i){
		return miscStats [i];
	}

	// Public Setters
	public void setMoneyMakerMutex(int i, bool value){
		moneyMakers [i].mutex = value;
	}

	public void increaseTotalClicks(){
		miscStats [0]++;
	}
}
