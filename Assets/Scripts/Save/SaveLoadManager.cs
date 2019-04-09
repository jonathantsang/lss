using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SaveData(Dictionary<int,int> collected, Dictionary<int,int> recipesunlocked, List<int> stats, List<int> shopunlocked, int currency, int numopened, int howmanyperpack){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream (Application.persistentDataPath + "/data.sav", FileMode.Create);

		SaveData data = new SaveData (collected, recipesunlocked, stats, shopunlocked, currency, numopened, howmanyperpack);
		bf.Serialize (stream, data);
		stream.Close ();
	}

	public static SaveData LoadData(){
		if (File.Exists (Application.persistentDataPath + "/data.sav")) {
			Debug.Log ("loaded existing data");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/data.sav", FileMode.Open);

			SaveData data = bf.Deserialize (stream) as SaveData;

			stream.Close ();
			return data;
		} else {
			Debug.Log ("create new");
			Debug.LogError ("File does not exist");
			return new global::SaveData (new Dictionary<int, int>() , new Dictionary<int, int>(), new List<int>(), new List<int>(), 0, 0, 0);
		}
	}
}

[System.Serializable]
public class SaveData {

	public int NumberOpened = 0; // How many crates opened
	public int Currency = 10; // How much currency they have
	public int OpenCount = 1; // How many to open at once

	// public int[] Inventory;
	public int[] Collected;
	public int[] RecipesUnlocked;
	public int[] Stats;
	public int[] ShopUnlocked;

	public SaveData(Dictionary<int,int> collected, Dictionary<int,int> recipesunlocked, List<int> stats, List<int> shopunlocked, int currency, int numopened, int numperpack){
		Collected = new int[50];
		// make collected
		// first set all to -1, (meaning it has not been seen), 0 means it has been seen just 0 amount
		for(int i = 0; i < Collected.Length; i++){
			Collected [i] = -1;
		}
		foreach (int key in collected.Keys)
		{
			// Debug.Log (key);
			// Stores the collected value at the index
			Collected [key] = collected [key];
		}
		RecipesUnlocked = new int[50];
		// make recipesunlocked
		// first set all to -1, (meaning it has not been seen), 0 means it has been seen just 0 amount
		for(int i = 0; i < RecipesUnlocked.Length; i++){
			RecipesUnlocked [i] = -1;
		}
		foreach (int key in recipesunlocked.Keys)
		{
			// Debug.Log (key);
			// Stores the collected value at the index
			RecipesUnlocked [key] = recipesunlocked [key];
		}
		Stats = new int[20];
		for (int i = 0; i < stats.Count; i++) {
			Stats [i] = stats [i];
		}

		ShopUnlocked = new int[30];
		for (int i = 0; i < shopunlocked.Count; i++) {
			ShopUnlocked [i] = shopunlocked [i];
		}

		NumberOpened = numopened;
		//Debug.Log (currency + " is currency");
		Currency = currency;
		OpenCount = numperpack;
	}
}