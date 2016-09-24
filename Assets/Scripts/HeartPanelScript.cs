using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPanelScript : MonoBehaviour {

	public Sprite Heart;
	public GameObject GameManager;

	public void GetHeart(int id){
		GameObject Box = new GameObject();

		switch (id) {
		case 1:
			Box = transform.FindChild ("heartbox_1").gameObject;
			GameManager.GetComponent<GameManagerScript> ().heart_1 = true;
			break;
		case 2:
			Box = transform.FindChild ("heartbox_2").gameObject;
			GameManager.GetComponent<GameManagerScript> ().heart_2 = true;
			break;
		case 3:
			Box = transform.FindChild ("heartbox_3").gameObject;
			GameManager.GetComponent<GameManagerScript> ().heart_3 = true;
			break;
		}

		Box.GetComponent<Image> ().sprite = Heart;

	}
}
