using UnityEngine;
using System.Collections;


public class CollectablesManager : MonoBehaviour {


	private bool noObstaclesMode;
	private bool noObstaclesModeActive;
	private float activeLengthCounter;

	[SerializeField]
	private PlatformGenerator platformGenerator;

	[SerializeField]
	private GameplayController gameplayController;

	private float obstaclesRate;

	private ObstaclesDestroy[] obstaclesList;


	void Update () {

		if(noObstaclesModeActive){
			activeLengthCounter -= Time.deltaTime;


			if(gameplayController.collectablesPowerReset){
				activeLengthCounter = 0;
				gameplayController.collectablesPowerReset = false;
			}

			//Stop spawning obstacles
			if(noObstaclesMode){
				platformGenerator.randomObstaclesSpawn = 0f;
			}

			//Start spawning obstalces
			if(activeLengthCounter <= 0){
				platformGenerator.randomObstaclesSpawn = obstaclesRate;
				noObstaclesModeActive = false;
			}
		}
	}

	//Destroying obstacles
	public void ActivateCollectablePower(bool safe, float time){

		noObstaclesMode = safe;
		activeLengthCounter = time;

		obstaclesRate = platformGenerator.randomObstaclesSpawn;

		if(noObstaclesMode){

			obstaclesList = FindObjectsOfType<ObstaclesDestroy>();
			for(int i = 0; i < obstaclesList.Length; i++){

				obstaclesList[i].gameObject.SetActive(false);
			}
		}

		noObstaclesModeActive = true;
	}	
}
