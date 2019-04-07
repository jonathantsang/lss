using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

	class MoneyMaker {
		public int level; // Ex 4

		public int price; // Ex 10
		public int production; // Ex 100

		// Scale factors on the next levels (float, round int down for each)
		public float priceScale; // 1.1, 10 -> 11
		public float productionScale; // 1.1 100 -> 110

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
		}
	}

	// Store data on the money makers
	MoneyMaker[] moneyMakers;

	// Exclusives
	int exclusives;
	int exclusivesWorth; // upgraded later

	void Awake(){
		moneyMakers = new MoneyMaker[6];
		// Init each item
		for(int i = 0; i < moneyMakers.Length; i++){
			moneyMakers [i] = new MoneyMaker ();
		}

		// initialize the money makers
		initializeMoneyMakers();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initializeMoneyMakers(){
		moneyMakers [0].level = 0;
		moneyMakers [0].price = 0;
		moneyMakers [0].production = 0;
		moneyMakers [0].priceScale = 1.1f;
		moneyMakers [0].productionScale = 1.1f;

		moneyMakers [1].level = 0;
		moneyMakers [1].price = 10;
		moneyMakers [1].production = 10;
		moneyMakers [1].priceScale = 1.1f;
		moneyMakers [1].productionScale = 1.1f;

		moneyMakers [2].level = 0;
		moneyMakers [2].price = 100;
		moneyMakers [2].production = 100;
		moneyMakers [2].priceScale = 1.1f;
		moneyMakers [2].productionScale = 1.1f;

		moneyMakers [3].level = 0;
		moneyMakers [3].price = 1000;
		moneyMakers [3].production = 1000;
		moneyMakers [3].priceScale = 1.1f;
		moneyMakers [3].productionScale = 1.1f;

		moneyMakers [4].level = 0;
		moneyMakers [4].price = 10000;
		moneyMakers [4].production = 10000;
		moneyMakers [4].priceScale = 1.1f;
		moneyMakers [4].productionScale = 1.1f;

		moneyMakers [5].level = 0;
		moneyMakers [5].price = 100000;
		moneyMakers [5].production = 100000;
		moneyMakers [5].priceScale = 1.1f;
		moneyMakers [5].productionScale = 1.1f;
	}

	// Getters
	public int getMoneyMakerLevel(int i){
		return moneyMakers [i].level;
	}

	public int getMoneyMakerPrice(int i){
		return moneyMakers [i].price;
	}

	public int getMoneyMakerProduction(int i){
		return moneyMakers [i].production;
	}

	public float getMoneyMakerPriceScale(int i){
		return moneyMakers [i].priceScale;
	}

	public float getMoneyMakerProductionScale(int i){
		return moneyMakers [i].productionScale;
	}
}
