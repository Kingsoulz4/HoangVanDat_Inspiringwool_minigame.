using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private BasketController m_currentBasket;
    [SerializeField] private List<WallController> m_listWall;
    [SerializeField] private Button m_buttonReplay;
    [SerializeField] private Button m_buttonReplayOnGameOver;
    [SerializeField] private GameObject m_gameOverPopup;
    [SerializeField] private Text m_textScore;
    [SerializeField] private GameObject m_heartContainer;
    [SerializeField] private Text m_textHighScore;
    [SerializeField] private Text m_textYourScore;

    public GameObject m_pusherPoint;

    private int heart = 3;
    private int score = 0;
    private string highScoreKey = "HighScore";

    public BasketController CurrentBasket
    {
        get => m_currentBasket; 
    }

    float woolForce = 10f;
    public bool IsFalling { get; set; }
    public bool IsFlying { get; set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        IsFalling = false;
        IsFlying = false;
        LeanTween.delayedCall(1f, () =>
        {
            m_currentBasket.SetWoolActive(true);
        });
        AddListenerToControllers();
    }

    void AddListenerToControllers()
    {
        m_buttonReplay.onClick.AddListener(ReplayGame);
        m_buttonReplayOnGameOver.onClick.AddListener(ReplayGame);
    }

    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.touchCount == 1 )&& !IsFlying)
        {
            IsFlying = true;
            PushCurrentWool();
        }    
    }

    private void PushCurrentWool()
    {
        //Rigidbody2D rb = m_currentWool.GetComponent<Rigidbody2D>();
        //rb.bodyType = RigidbodyType2D.Dynamic;
        //rb.velocity = Vector2.zero;
        //rb.AddForce(Vector2.up * woolForce, ForceMode2D.Impulse);
        m_currentBasket.PushWool();
    }

    public void SetCurrentBasket(BasketController basketController)
    {
        m_currentBasket = basketController;
        m_currentBasket.SetWoolActive(true);
    }

    public void PullDownWalls()
    {
        foreach(var wall in m_listWall)
        {
            LeanTween.moveY(wall.gameObject, wall.transform.position.y - 3.59f, 0.5f);
        }
    }    

    private void ReplayGame()
    {
        SceneManager.LoadScene(AllSceneName.GamePlayScene);
    }

    private void ShowGameOver()
    {
        var highScore = PlayerPrefs.GetInt(highScoreKey);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
        }
        m_textHighScore.text = PlayerPrefs.GetInt(highScoreKey).ToString();
        m_textYourScore.text = score.ToString();
        m_gameOverPopup.gameObject.SetActive(true); 
    }

    public void SubtractHeart()
    {
        heart--;
        m_heartContainer.transform.GetChild(heart).gameObject.SetActive(false);
        UpdateUI();
    }
    private void UpdateUI()
    {
        
        if (heart == 0)
        {
            ShowGameOver();
        }
    }

    public void UpdateScore()
    {
        score += 10;
        m_textScore.text = score.ToString();
    }

    private void OnDisable()
    {
        LeanTween.cancelAll();
    }

}
