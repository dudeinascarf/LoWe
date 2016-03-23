using UnityEngine;
using System.Collections;


public class CollectablesController : MonoBehaviour {


	private GameplayController gameplayController;

	//Amount of cins to give for one picked coin
	private int scoreToGive = 1;
	private AudioSource collectablesSound;


	void Start(){

		gameplayController = FindObjectOfType<GameplayController>();
		collectablesSound = GameObject.Find("CollectablesSound").GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other){

		//if player collider
		if(other.tag == "Player"){
			gameplayController.collectableScore += scoreToGive;
			collectablesSound.Play();
			PlayerPrefs.SetInt("Collectable", gameplayController.collectableScore);
			ObjectController.Destroy(gameObject);
		}

		//if object pooler destroyer
		else if(other.tag == "Destroyer"){
			ObjectController.Destroy(gameObject);
		}
	}
}
