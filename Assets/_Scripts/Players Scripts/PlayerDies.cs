using UnityEngine;
using System.Collections;

public class PlayerDies : MonoBehaviour {


	public delegate void EndGame();
	public static event EndGame endGame;

	//PowerUp
	public bool canDestroyObstacles;

	[SerializeField]
	private AudioSource deathSound;

	[SerializeField]
	private AudioSource eatObstacles;


	void Start(){
		deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
		eatObstacles = GameObject.Find("EatObstacles").GetComponent<AudioSource>();
	}
	
	public void PlayerDied(){

		if(endGame != null){
			endGame();
		}
		gameObject.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D target){

		//if obstacle and powerUp is Off
		if(target.gameObject.tag == "Obstacle" && canDestroyObstacles == false){
			deathSound.Play();
			PlayerDied();

		}
		//if obstacle and powerUp is On
		else if(target.gameObject.tag == "Obstacle" && canDestroyObstacles == true){
			eatObstacles.Play();
			ObjectController.Destroy(target.gameObject);
		}
	}

	//Fall off ground
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Destroyer" || other.tag == "PlayerD"){
			deathSound.Play();
			PlayerDied();
		}
	}

	public void RestartGame(){
		canDestroyObstacles = false;
	}
}
