using UnityEngine;
using System.Collections;


public class CharacterAnimationController : MonoBehaviour {

	//Player start speed
	public float speed = 7f;

	//Player max speed
	private float maxSpeed = 22f;

	public float accelerationTime = 0.001f;

	//Player jump force
	private float jumpForce = 12f;

	[SerializeField]
	private Rigidbody2D rb2d;

	public Animator anim;

	[SerializeField]
	private ParticleSystem ps;

	//Does player on ground
	public bool grounded = false;

	[SerializeField]
	private Transform groundCheck;

	//Ground check radius
	private float groundRadius = 0.3f;

	[SerializeField]
	private LayerMask whatIsGround;

	//is double jump active
	private bool doubleJump;

	private AudioSource jumpSound;


	void Start(){

		anim = transform.gameObject.GetComponent<Animator>();
		jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
	}

	void Update(){

		speed += accelerationTime;
		if(speed > maxSpeed){
			speed = maxSpeed;
		}

		//Player grounded and jump only once
		if((grounded || !doubleJump) && Input.GetMouseButtonDown(0)){
			anim.SetBool("Ground", false);
			jumpSound.Play();

			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

			if(!grounded){
				doubleJump = true;
			}
		}
	}

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat("vSpeed", rb2d.velocity.y);

		rb2d.velocity = new Vector2(speed, rb2d.velocity.y);


		//activate deactivate particles
		if(grounded){
			doubleJump = false;
			ps.gameObject.SetActive(true);
		}
		if(grounded == false){
			ps.gameObject.SetActive(false);
		}
	}
}
