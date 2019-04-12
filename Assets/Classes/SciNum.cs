using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciNum {

	int originalNumber = 0;
	string converted;

	public SciNum(int num = 0){
		originalNumber = num;
		if (num <= 9999999) {
			converted = num.ToString ();
		} else {
			converted = num.ToString ();
			string firstpart = converted.Substring (0, 3);
			firstpart = firstpart.Substring (0, 1) + "." + firstpart.Substring (1, 2);
			string end = "E";
			int e = converted.Length - 1; // 1 for the scientific notation
			end += e.ToString();
			converted = firstpart + end;
		}
	}

	public string getNum(){
		return converted;
	}

	public int getOriginalNum(){
		return originalNumber;
	}
}
