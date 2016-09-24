using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public int donuts = 0;
	public int candy=0;
	public int pudding=0;
	public GameObject donutstext;
	public GameObject candytext;
	public GameObject puddingtext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		donutstext.GetComponent<Text> ().text = "x" + donuts.ToString ();
		candytext.GetComponent<Text> ().text = "x" + candy.ToString ();
		puddingtext.GetComponent<Text> ().text = "x" + pudding.ToString ();
	}
}
