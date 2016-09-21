using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public int donuts = 0;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		canvas.GetComponent<Text> ().text = donuts.ToString ();
	}
}
