using UnityEngine;
using System.Collections;

public class PowerUpReserTrigger : MonoBehaviour {

	
	//Check for powerUps and gym button


	[SerializeField]
	private GameplayController gameplayController;

	[SerializeField]
	private CharacterAnimationController characterAnimController;


	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){
			gameplayController.jumpReset = false;
			gameplayController.godReset = false;
			gameplayController.collectablesPowerReset = false;
			characterAnimController.grounded = true;

		}
	}
}
