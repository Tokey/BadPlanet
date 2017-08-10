using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public int score;

    [SerializeField]
    private Text scoreUI;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private float gameTimer;

    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private GameObject blasterUI;

    [SerializeField]
    private GameObject fireBtn;

    [SerializeField]
    private Text coinTxt;

    [SerializeField]
    private int requiredCrystals;
    private int collectedCrystals;

    [SerializeField]
    private GameObject alienShip;

    private int collectedCoins;

    private bool blasterCollected;

    [SerializeField]
    private ShipUI sui;

    [SerializeField]
    private BarScript timerBar;

    [SerializeField]
    private GameObject gameOverScreen;


    [SerializeField]
    private GameObject levelCompleteScreen;

    [SerializeField]
    private Player player;

    
    private string highScoreSaver;


    [SerializeField]
    private Text lCHSText;
    [SerializeField]
    private Text lCSCText;



    public bool BlasterCollected
    {
        get
        {

            return blasterCollected;

        }
        set
        {   
            blasterCollected = value;
            blasterUI.SetActive(blasterCollected);
            fireBtn.SetActive(blasterCollected);
        }
    }

    public int CollectedCrystals
    {
        get
        {
            
            return collectedCrystals;
            
        }
        set
        {   if(collectedCrystals <= requiredCrystals)
                collectedCrystals = value;
        }
    }

    private bool completeLevel;
    public bool CompleteLevel
    {
        get
        {

            return completeLevel;

        }
        set
        {
            completeLevel = value;

            if (completeLevel)
            {
                
                GameManager.Instance.score += (int)gameTimer * 10;
                if (score >= PlayerPrefs.GetInt(highScoreSaver, 0))
                    PlayerPrefs.SetInt(highScoreSaver,score);
                lCHSText.text=PlayerPrefs.GetInt(highScoreSaver, 0).ToString();
                lCSCText.text = score.ToString();
                levelCompleteScreen.SetActive(true);
                Time.timeScale = 0;

            }
                

        }
    }


    public static GameManager Instance
    {
        get
        {

            if (instance == null) {

                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

     
    }

    public GameObject CoinPrefab
    {
        get
        {
            return coinPrefab;
        }

    }

    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }

        set
        {

            coinTxt.text = value.ToString();
            this.collectedCoins = value;
            PlayerPrefs.SetInt("Gems", collectedCoins);
        }
    }

    public void CollectCrystal()
    {
        collectedCrystals++;
        sui.Value = collectedCrystals;
        if (collectedCrystals==requiredCrystals)
        {
            alienShip.SetActive(true);
        }
        score += 100;
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
     void Start()
    {
        sui.MaxValue = requiredCrystals;
        sui.Value = 0;
        timerBar.MaxValue = gameTimer;
        score = 0;
        collectedCoins = PlayerPrefs.GetInt("Gems",0);
        coinTxt.text = collectedCoins.ToString();
        highScoreSaver = "LevelHS" + SceneManager.GetActiveScene().buildIndex;
        string locktext = "LevelLock" + SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(locktext, 10);
    }

     void Update()
    {
        UpdateScore();
        if (gameTimer>0)
        gameTimer -=Time.deltaTime;
        timerBar.Value = gameTimer;
        if (gameTimer <= 0)
        {
            gameTimer = 0;
            bomb.SetActive(true);
        }

        if (player.IsDead == true)
        { gameOverScreen.SetActive(true);
             
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void OnApplicationPause(bool pause)
    {
        pause = true;
    }

    private void UpdateScore()
    {
        scoreUI.text = "Score : "+score.ToString();
    }

}
