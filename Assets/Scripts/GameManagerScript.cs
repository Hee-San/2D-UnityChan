using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public int donuts = 0;
	public int candy = 0;
	public int pudding = 0;
	public bool heart_1, heart_2, heart_3 ;
	public GameObject donutstext;
	public GameObject candytext;
	public GameObject puddingtext;

	void Awake(){
		heart_1= false;
		heart_2= false;
		heart_3= false;
	}

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
