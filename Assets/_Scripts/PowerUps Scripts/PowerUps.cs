using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {

	//Can Jump infinite times
	public bool infiniteJumps;

	//Can destroy obstacles
	public bool invincibility;

	//PowerUps time
	public float jumpLength;
	public float godLength;

	private AudioSource godPowerSound;

	private PowerUpController powerUpController;


	void Start(){

		powerUpController = FindObjectOfType<PowerUpController>();
		godPowerSound = GameObject.Find("GodPowerSound").GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){

			powerUpController.ActivatePowerUp(infiniteJumps, invincibility, jumpLength, godLength);
			godPowerSound.Play();
			ObjectController.Destroy(gameObject);
		}
		if(other.tag == "Destroyer"){
			ObjectController.Destroy(gameObject);
		}
	}

}
