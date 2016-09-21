using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpriteActionKari : MonoBehaviour {
	static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashIsCrouch = Animator.StringToHash ("IsCrouch");	//しゃがむ

	static int hashDamage = Animator.StringToHash ("Damage");
	static int hashIsDead = Animator.StringToHash ("IsDead");

	[SerializeField] private float characterHeightOffset = 0.2f;
	[SerializeField] LayerMask groundMask;

	[SerializeField, HideInInspector] Animator animator;
	[SerializeField, HideInInspector]SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector]Rigidbody2D rig2d;

	private AudioSource audioSource;

	public int hp = 4;

	public float axis;
	public float speed;

	public GameObject gameManager;
	public AudioClip get;

	//public GameObject donuts;
	//public float donutsVelo;
	//public Vector2 donutspPos;

	//private int flipx = 1;



	void Awake ()
	{
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rig2d = GetComponent<Rigidbody2D> ();
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void Update ()
	{
		axis = Input.GetAxisRaw ("Horizontal");
		bool isDown = Input.GetAxisRaw ("Vertical") < 0 && axis == 0;	//Vertical入力が負か否か

		var distanceFromGround = Physics2D.BoxCast (transform.position, new Vector2(0.18f,0.18f), 0, Vector3.down, 1, groundMask);	//position地点からdown方向に向かって最大1までReyを飛ばす、groundMaskに当たるかどうか
		//var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 1, groundMask);	//position地点からdown方向に向かって最大1までReyを飛ばす、groundMaskに当たるかどうか

		// update animator parameters
		animator.SetBool (hashIsCrouch, isDown);	//isDownをhashIsCrouchに代入（Bool）
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);	//めり込んでたら99を返す(いしのなかにいる)、そうでなければ地面との距離-キャラ高さ
		//Debug.Log(hashGroundDistance);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (axis)); //速さは速度の絶対値

		rig2d.velocity = new Vector2 (axis*speed, rig2d.velocity.y);

		// flip sprite
		if (axis != 0){	//Horizontal入力がちょっとでもあるとき
			spriteRenderer.flipX = axis < 0;	//Renderer flipXがtrueなら通常,falseのとき反転

			/*switch (axis < 0) {
			case true:
				flipx = -1;
				break;
			case false:
				flipx = 1;
				break;
			}*/
		}

		if (isDown) {
			GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			GetComponent<BoxCollider2D> ().enabled = true;

			if (Input.GetButtonDown ("Jump") && Mathf.Abs (distanceFromGround.distance - characterHeightOffset) <= 0.08f) {
				rig2d.velocity = new Vector2 (rig2d.velocity.x, 5);
			}

			//ActionDonuts (transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		switch (other.gameObject.tag) {
		case "donuts":
			gameManager.GetComponent<GameManagerScript> ().donuts++;
			Destroy (other.gameObject);
			audioSource.clip = get;
			audioSource.Play ();
			break;
		}
	}


	/*void ActionDonuts(Vector2 XY){
		if (gameManager.GetComponent<GameManagerScript> ().donuts < 1){
			return;
		}

		if (Input.GetButtonDown ("Fire1")) {
		
			GameObject Donut = Instantiate (donuts, new Vector3(XY.x+flipx*donutspPos.x, XY.y+donutspPos.y, 1), Quaternion.identity) as GameObject;
			Donut.GetComponent<Rigidbody2D> ().velocity = new Vector2(flipx*donutsVelo, 0);

			gameManager.GetComponent<GameManagerScript> ().donuts--;
		}
	}*/
}
