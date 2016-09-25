using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPanelScript : MonoBehaviour {

	public Sprite Heart;
	public GameObject GameManager;

	public void GetHeart(int id){

		switch (id) {
		case 1:
			transform.FindChild ("heartbox_1").gameObject.GetComponent<Image> ().sprite = Heart;
			GameManager.GetComponent<GameManagerScript> ().heart_1 = true;
			break;
		case 2:
			transform.FindChild ("heartbox_2").gameObject.GetComponent<Image> ().sprite = Heart;
			GameManager.GetComponent<GameManagerScript> ().heart_2 = true;
			break;
		case 3:
			transform.FindChild ("heartbox_3").gameObject.GetComponent<Image> ().sprite = Heart;
			GameManager.GetComponent<GameManagerScript> ().heart_3 = true;
			break;
		}


	}
}
