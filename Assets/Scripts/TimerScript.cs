using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public int minite = 0;
	float secondF = 0;
	public int second = 0;
	int oldsecond = 0;

	void Update () {
		if (Time.timeScale > 0) {
			secondF += Time.deltaTime;
			minite = Mathf.FloorToInt (secondF / 60);
			second = Mathf.FloorToInt (secondF - minite * 60);
			if (second != oldsecond) {
				gameObject.GetComponent<Text>().text = minite.ToString ("00") + ":" + second.ToString ("00");
			}
			oldsecond = second;
		}
	
	}
}
