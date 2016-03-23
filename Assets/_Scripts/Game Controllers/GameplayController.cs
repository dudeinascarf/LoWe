using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using System.Collections.Generic;


public class GameplayController : MonoBehaviour {


	public static GameplayController instance;

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private GameObject mainPlatform;

	[SerializeField]
	private GameObject platformGenerationPoint;

	[SerializeField]
	private GameObject mainCamera;
	
	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject mainMenuPanel;

	[SerializeField]
	private Button restartGameButton;

	[SerializeField]
	private Button rateUsButton;

	[SerializeField]
	private Button doubleCollectablesButton;

	// Score and High Score
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text highScoreText;
	public static float score;
	public float highScore;

	//Collectables
	[SerializeField]
	private Text collectablesTextGame;

	[SerializeField]
	private Text doubleCollectablesButtonText;

	[SerializeField]
	private Sprite[] collectablesSprites;

	public int collectableScore;

	public delegate void TurnOff();
	public static event TurnOff turnOff;

	//Reseting PowerUps if Died
	public bool jumpReset;
	public bool godReset;
	public bool collectablesPowerReset;

	[SerializeField]
	private GameObject[] panels;

	[SerializeField]
	private GameObject collectablePanel;

	private int adCounter;

	private Button circularButton;

	[SerializeField]
	private CanvasGroup circleButton;

	[SerializeField]
	private Button facebookButton;

	[SerializeField]
	private Sprite[] facebookSprites;

	[SerializeField]
	private GameObject infoPanel;

	[SerializeField]
	private Image infoImage;

	[SerializeField]
	private Sprite[] infoSprites;

	private int infoIndex;

	[SerializeField]
	private Text notificationText;

	private bool canTouchFacebookButton;

	[SerializeField]
	private Animator whiteScreenAnim;

	[SerializeField]
	private GameObject mainMenuBackground;

	[SerializeField]
	private ParticleSystem ps;

	//Player start position
	[SerializeField]
	private GameObject shelf;


	void Awake(){
		MakeInstance();
	}

	void Start(){

		mainMenuBackground.SetActive(true);
		canTouchFacebookButton = true;
		infoPanel.SetActive(false);
		adCounter = 0;
		scoreText.gameObject.SetActive(false);

		//Make gym button invisible at start
		circularButton = GameObject.Find("CircularButton").GetComponent<Button>();
		circleButton.alpha = 0f;

		//Inicializing background and setting speed to zero
		BackgroundLooper backgroundLooper = GameObject.Find("Background").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper1 = GameObject.Find("Midground_1").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper2 = GameObject.Find("Midground_2").GetComponent<BackgroundLooper>();
		backgroundLooper.speed = Vector2.zero;
		midgroundLooper1.speed = Vector2.zero;
		midgroundLooper2.speed = Vector2.zero;

		//Player and camera speed zero
		mainCamera.GetComponent<CameraMooving>().cameraSpeed = 0f;
		mainCamera.GetComponent<CameraMooving>().accelerationTime = 0f;
		player.GetComponent<CharacterAnimationController>().speed = 0f;
		player.GetComponent<CharacterAnimationController>().accelerationTime = 0f;
		
		if(PlayerPrefs.HasKey("HighScore")){
			highScore = PlayerPrefs.GetFloat("HighScore");
		}
		if(!PlayerPrefs.HasKey("Collectable")){
			PlayerPrefs.SetInt("Collectable", 0);
		}
		else{
			collectableScore = PlayerPrefs.GetInt("Collectable");
		}
	}

	void MakeInstance(){
		if(instance == null){
			instance = this;
		}
	}

	void Update(){

		if(score > highScore){
			highScore = score;
			PlayerPrefs.SetFloat("HighScore", highScore);
		}

		//Distance player have ran
		score = player.transform.localPosition.x + 6f;
		scoreText.text = Mathf.Round(score).ToString();

		collectablesTextGame.text = collectableScore.ToString();

		//if coins not enough button not active
		if(collectableScore < 30){
			circularButton.interactable = false;
		}
		if(collectableScore >= 30){
			circularButton.interactable = true;
		}
	}

	void OnEnable(){
		PlayerDies.endGame += PlayerDiedEndTheGame;
	}
	void OnDisable(){
		PlayerDies.endGame -= PlayerDiedEndTheGame;
	}

	//Player died
	void PlayerDiedEndTheGame(){
				
		highScoreText.text = "" + Mathf.Round(highScore);
		restartGameButton.onClick.RemoveAllListeners();
		restartGameButton.onClick.AddListener (() => ResumeGame());
		mainCamera.GetComponent<CameraMooving>().cameraSpeed = 0f;
		player.GetComponent<CharacterAnimationController>().speed = 0f;
		BackgroundLooper backgroundLooper = GameObject.Find("Background").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper1 = GameObject.Find("Midground_1").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper2 = GameObject.Find("Midground_2").GetComponent<BackgroundLooper>();
		backgroundLooper.speed = Vector2.zero;
		midgroundLooper1.speed = Vector2.zero;
		midgroundLooper2.speed = Vector2.zero;
		circularButton.GetComponent<CircularButton>().counter = 5f;
		collectablesPowerReset = true;

		//Taking 5% of all earned coins
		collectableScore -= collectableScore * 5/100;

		circleButton.alpha = 0f;
	
		PlayerPrefs.SetInt("Collectable", collectableScore);

		//Showing add button in gameOver menu
		adCounter++;
		if(adCounter == 5){
			collectablePanel.SetActive(true);
			doubleCollectablesButton.image.sprite = collectablesSprites[1];
			doubleCollectablesButtonText.text = "";
			adCounter = 0;
		}
		else{
			collectablePanel.SetActive(false);
		}

		ps.transform.position = player.transform.position;
		ps.gameObject.SetActive(true);

		//Setting timeScale zero after 1 second
		StartCoroutine(StopGameTime());
	}

	IEnumerator StopGameTime(){
		yield return new WaitForSeconds(1f);
		Time.timeScale = 0f;
		pausePanel.SetActive(true);
		ps.gameObject.SetActive(false);
	}


	public void ResumeGame(){

		pausePanel.SetActive(false);
		Time.timeScale = 1f;
	}

	public void RestartGame(){

		BackgroundLooper backgroundLooper = GameObject.Find("Background").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper1 = GameObject.Find("Midground_1").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper2 = GameObject.Find("Midground_2").GetComponent<BackgroundLooper>();
		backgroundLooper.speed = new Vector2(0.05f, 0);
		midgroundLooper1.speed = new Vector2(0.1f, 0);
		midgroundLooper2.speed = new Vector2(0.2f, 0);

		pausePanel.SetActive(false);
		score = 0;
		scoreText.text = Mathf.Round(score).ToString();
		player.transform.position = new Vector3(-5, -0.6f, -3);
		mainPlatform.transform.position = new Vector3(1.5f, -6, -3);
		platformGenerationPoint.transform.position = new Vector3(14, -6, -3);
		mainCamera.transform.position = new Vector3(0, 0, -10);
		if(turnOff != null){
			turnOff();
		}
		player.SetActive(true);
		player.GetComponent<CharacterAnimationController>().speed = 7f;
		mainPlatform.SetActive(true);
		mainCamera.GetComponent<CameraMooving>().cameraSpeed = 7f;
		platformGenerationPoint.SetActive(true);
		mainCamera.SetActive(true);
		Time.timeScale = 1f;
		jumpReset = true;
		godReset = true;
		shelf.SetActive(false);

		circleButton.alpha = 1f;

		doubleCollectablesButton.interactable = true;

		//Changing gameOver panel siblings 
		for(int i = 0; i < panels.Length; i++){
			panels[i].transform.SetSiblingIndex(Random.Range(0, panels.Length));
		}
	}

	public void StartGame(){

		BackgroundLooper backgroundLooper = GameObject.Find("Background").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper1 = GameObject.Find("Midground_1").GetComponent<BackgroundLooper>();
		BackgroundLooper midgroundLooper2 = GameObject.Find("Midground_2").GetComponent<BackgroundLooper>();
		backgroundLooper.speed = new Vector2(0.05f, 0);
		midgroundLooper1.speed = new Vector2(0.1f, 0);
		midgroundLooper2.speed = new Vector2(0.2f, 0);

		mainMenuPanel.SetActive(false);
		player.GetComponent<CharacterAnimationController>().speed = 7f;
		player.GetComponent<CharacterAnimationController>().accelerationTime = 0.001f;
		mainCamera.GetComponent<CameraMooving>().cameraSpeed = 7f;
		mainCamera.GetComponent<CameraMooving>().accelerationTime = 0.001f;
		jumpReset = true;
		godReset = true;
		shelf.SetActive(false);

		whiteScreenAnim.Play("Flash");
		mainMenuBackground.SetActive(false);
		scoreText.gameObject.SetActive(true);
		circleButton.alpha = 1f;

	}

	public void MainMenu(){

		pausePanel.SetActive(false);
		mainMenuPanel.SetActive(true);
		score = 0;
		scoreText.text = Mathf.Round(score).ToString();
		player.transform.position = new Vector3(-5, -0.6f, -3);
		mainPlatform.transform.position = new Vector3(1.5f, -6, -3);
		platformGenerationPoint.transform.position = new Vector3(14, -6, -3);
		mainCamera.transform.position = new Vector3(0, 0, -10);
		if(turnOff != null){
			turnOff();
		}
		player.SetActive(true);
		player.GetComponent<CharacterAnimationController>().speed = 0f;
		player.GetComponent<CharacterAnimationController>().accelerationTime = 0f;
		mainPlatform.SetActive(true);
		mainCamera.GetComponent<CameraMooving>().cameraSpeed = 0f;
		mainCamera.GetComponent<CameraMooving>().accelerationTime = 0f;
		platformGenerationPoint.SetActive(true);
		mainCamera.SetActive(true);
		Time.timeScale = 1f;
		jumpReset = true;
		godReset = true;
		shelf.SetActive(true);

		doubleCollectablesButton.interactable = true;
	
		mainMenuBackground.SetActive(true);
		scoreText.gameObject.SetActive(false);
		circleButton.alpha = 0f;


		for(int i = 0; i < panels.Length; i++){
			panels[i].transform.SetSiblingIndex(Random.Range(0, panels.Length));
		}

	}
	/*
	//Show ad and give certain ammount of coins
	public void DoubleCollectables(){
		
		if(Advertisement.IsReady("rewardedVideo")){
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
			doubleCollectablesButton.interactable = false;
			doubleCollectablesButton.image.sprite = collectablesSprites[0];

		}
	}

	public void HandleShowResult(ShowResult result){
		switch(result){
			case ShowResult.Finished:
				Debug.Log ("The ad was successfully shown.");
				collectableScore += 20;
				doubleCollectablesButtonText.text = "20";
				PlayerPrefs.SetInt("Collectable", collectableScore);
				break;
			case ShowResult.Skipped:
				Debug.Log ("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.Log ("The ad failed to be shown.");
				break;
		}
	}
	*/

	public void FacebookButton(){
		if(SocialMediaController.instance.IsLogedIn()){
			SocialMediaController.instance.LogOut();
		} else {
			SocialMediaController.instance.LogIn();
		}
	}

	void FacebookStatus(string status){

		switch (status){

		case "Login Finished":
			facebookButton.image.sprite = facebookSprites[0];
			notificationText.text = "Connected";
			break;

		case "Logout Finished":
			facebookButton.image.sprite = facebookSprites[1];
			notificationText.text = "Disconnected";
			break;

		case "Login Canceled":
			facebookButton.image.sprite = facebookSprites[1];
			notificationText.text = "Login Canceled";
			break;

		case "Login Failed":
			facebookButton.image.sprite = facebookSprites[1];
			notificationText.text = "Login Failed";
			break;

		case "Logout Failed":
			facebookButton.image.sprite = facebookSprites[0];
			notificationText.text = "Logout Failed";
			break;

		}

		infoPanel.SetActive(true);
		infoPanel.SetActive(false);
		canTouchFacebookButton = true;
	}

	public void Notification(string status){
		if(canTouchFacebookButton){
			FacebookStatus(status);
			canTouchFacebookButton = false;
		}
	}

	public void PostSucceded(){
		notificationText.text = "Thank You For Sharing Us On Facebook!";
	}

	public void ShareUsOnFacebook(){
		if(SocialMediaController.instance.IsLogedIn()){
			SocialMediaController.instance.ShareUsOnFacebook();
		} else {
			Notification("Please Connect In Order To Post");
		}
	}	

	public void RateUs(){
		SocialMediaController.instance.RateOurApp();
	}

}















