using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour {


	//Can Jump infinite times
	private bool infiniteJumps;

	//Can destroy obstacles
	private bool invincibility;

	//PowerUp active time
	public bool jumpActive;
	public bool godActive;

	private float jumpsTimeCounter;
	private float godTimeCounter;

	[SerializeField]
	private CharacterAnimationController characterAnimationController;

	[SerializeField]
	private PlayerDies playerDies;

	[SerializeField]
	private GameplayController gameplayController;

	//For infinite jumps
	private bool isGrounded;

	//For invincibility
	private bool canDestroyObstacles;


	void Update(){

		//Infinite Jumps PowerUp
		if(jumpActive){

			jumpsTimeCounter -= Time.deltaTime;

			if(gameplayController.jumpReset){
				jumpsTimeCounter = 0;
				gameplayController.jumpReset = false;
			}
			characterAnimationController.grounded = true;

			characterAnimationController.anim.runtimeAnimatorController = Resources.Load("CharacterControllerThin") as RuntimeAnimatorController;	
				
			if(jumpsTimeCounter <= 0){
				characterAnimationController.grounded = false;
				if(characterAnimationController.grounded == false){
					characterAnimationController.anim.runtimeAnimatorController = Resources.Load("CharacterController") as RuntimeAnimatorController;
				}
				jumpActive = false;
			}
		}

		//Invincibility PowerUp
		if(godActive){

			godTimeCounter -= Time.deltaTime;

			if(gameplayController.godReset){
				godTimeCounter = 0;
				gameplayController.godReset = false;
			}
			playerDies.canDestroyObstacles = true;
			characterAnimationController.anim.runtimeAnimatorController = Resources.Load("CharacterControllerMuscle") as RuntimeAnimatorController;

			if(godTimeCounter <= 0){
				playerDies.canDestroyObstacles = false;
				if(playerDies.canDestroyObstacles == false){
					characterAnimationController.anim.runtimeAnimatorController = Resources.Load("CharacterController") as RuntimeAnimatorController;
				}
				godActive = false;
			}
		}
		
	}

	public void ActivatePowerUp(bool jumps, bool god, float jumpsTime, float godTime){

		infiniteJumps = jumps;
		invincibility = god;

		jumpsTimeCounter = jumpsTime;
		godTimeCounter = godTime;

		isGrounded = characterAnimationController.grounded;
		canDestroyObstacles = playerDies.canDestroyObstacles;

		jumpActive = true;
		godActive = true;
	}	
}
