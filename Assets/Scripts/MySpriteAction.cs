using UnityEngine;
using System.Collections;

public class MySpriteAction : MonoBehaviour
{
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
		}

		if (isDown) {
			GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			GetComponent<BoxCollider2D> ().enabled = true;

			if (Input.GetButtonDown ("Jump") && Mathf.Abs (distanceFromGround.distance - characterHeightOffset) <= 0.08f) {
				rig2d.velocity = new Vector2 (rig2d.velocity.x, 5);
			}
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
		case "candy":
			gameManager.GetComponent<GameManagerScript> ().candy++;
			Destroy (other.gameObject);
			break;
		}
	}
}
