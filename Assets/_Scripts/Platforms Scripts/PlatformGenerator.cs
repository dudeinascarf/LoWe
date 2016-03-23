using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {


	[SerializeField]
	private GameObject[] platforms;

	[SerializeField]
	private Transform generationPoint;

	private float distanceBetween;
	private float[] platformsWidths;
	private int platformSelector;

	[SerializeField]
	private float distMin;
	[SerializeField]
	private float distMax;

	//Platforms Height
	[SerializeField]
	private Transform maxHeightPoint;
	private float minHeight;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;

	//...........................................................//
	//Reference to CollectablesGenerator script
	private CollectablesGenerator collectablesGenerator;
	//Random Collectables Spawn Chance
	private float randomCollectablesSpawn = 15f;

	//...........................................................//
	//Reference to obstacle prefab
	[SerializeField]
	private GameObject[] obstacles;
	//Obstacles variety
	private int obstacleSelector;
	//Random Obstacles Spawn Chance
	public float randomObstaclesSpawn = 100f;

	//...........................................................//
	//PowerUp height
	private float powerUpHeight = 5f;
	//Reference to PowerUp prefab
	[SerializeField] 
	private GameObject[] powerUp;
	//PowerUps variety
	private int powerUpSelector;
	//Random PowerUp Spawn Chance
	private float randomPowerUpSpawn = 20f;


	void Start () {

		platformsWidths = new float[platforms.Length];

		for(int i = 0; i < platforms.Length; i++){
			platformsWidths[i] = platforms[i].GetComponent<BoxCollider2D>().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
		collectablesGenerator = FindObjectOfType<CollectablesGenerator>();
	}

	void Update () {

		//Spawning platforms
		if(transform.position.x < generationPoint.position.x){

			distanceBetween = Random.Range(distMin, distMax);
			platformSelector = Random.Range(0, platforms.Length);
			heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

			if(heightChange > maxHeight){
				heightChange = maxHeight;
			}
			else if(heightChange < minHeight){
				heightChange = minHeight;
			}

			//Spawning powerUps
			if(Random.Range(0f, 100f) < randomPowerUpSpawn){

				powerUpSelector = Random.Range(0, powerUp.Length);
				GameObject newPowerUp = ObjectController.Instantiate(powerUp[powerUpSelector], transform.position);
				newPowerUp.transform.position = transform.position + new Vector3(distanceBetween 
				/ 2f, Random.Range(powerUpHeight / 2f, powerUpHeight), 0f);

				newPowerUp.SetActive(true);
			}
		
			transform.position = new Vector3(transform.position.x + (platformsWidths[platformSelector] / 2) 
			+ distanceBetween, heightChange, transform.position.z);


			ObjectController.Instantiate(platforms[platformSelector], transform.position);

			//Spawning coins
			if(Random.Range(0f, 100f) < randomCollectablesSpawn){

				collectablesGenerator.SpawnCollectables(new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z));
			}

			//Spawning obstacles
			if(Random.Range(0f, 100f) < randomObstaclesSpawn){

				obstacleSelector = Random.Range(0, obstacles.Length);
				GameObject newObstacle = ObjectController.Instantiate(obstacles[obstacleSelector], transform.position);
				float obstacleXPosition = Random.Range(-platformsWidths[platformSelector] / 3f, platformsWidths[platformSelector] / 3f);
				Vector3 obstaclePosition = new Vector3(obstacleXPosition, 2.5f, 0f);

				newObstacle.transform.position = transform.position + obstaclePosition;
				newObstacle.transform.rotation = transform.rotation;
				newObstacle.SetActive(true);
			}

			transform.position = new Vector3(transform.position.x + (platformsWidths[platformSelector] / 2), 
			transform.position.y, transform.position.z);


		}
	}
}
