using UnityEngine;
using System.Collections;

public class FromPointToPointMooving : MonoBehaviour {

	//Moving Obstacles

	public Vector2 speed = Vector2.zero;

	[SerializeField]
	private Rigidbody2D myRigidbody;

	private bool facingRight = true;

   
	void FixedUpdate(){
		myRigidbody.velocity = speed;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Walls"){
			Flip();
			speed = -speed;
		}
	}

	//Change gameObject scale
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
