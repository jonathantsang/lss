using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciNum {

	long originalNumber = 0;
	string converted;

	public SciNum(long num = 0){
		if (num > long.MaxValue) {
			originalNumber = long.MaxValue; // temp fix
		} else {
			originalNumber = num;
		}
		if (num <= 9999999) {
			converted = num.ToString ();
		} else {
			// 10000000
			converted = num.ToString ();
			// Could also use 88A 99AAA, etc. base 26
			int leng = 4;

			string firstpart = converted.Substring (0, leng); // length 5 sci num
			firstpart = firstpart.Substring (0, 1) + "." + firstpart.Substring (1);
			string end = "E";
			int e = converted.Length - 1; // 1 for the scientific notation
			end += e.ToString();
			converted = firstpart + end;
		}
	}

	public string getNum(){
		return converted;
	}

	public long getOriginalNum(){
		return originalNumber;
	}
}
