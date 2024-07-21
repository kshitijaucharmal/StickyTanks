using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerMovement player1;
    [SerializeField] private PlayerMovement player2;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject endScreenCanvas;
    [SerializeField] private ResponsiveCamera resCam;

    [SerializeField] private Color p1WinColor;
    [SerializeField] private Color p2WinColor;
    [SerializeField] private TMP_Text winText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        endScreenCanvas.SetActive(false);
        Time.timeScale = 0;
        resCam.gamePaused = true;
    }

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        endScreenCanvas.SetActive(false);
        Time.timeScale = 1;
        resCam.gamePaused = false;
    }
    public void EndGame(int p)
    {
        mainMenuCanvas.SetActive(false);
        endScreenCanvas.SetActive(true);

        winText.color = p == 1 ? p1WinColor : p2WinColor;

        Time.timeScale = 0;
        resCam.gamePaused = true;
    }

    public void RestartGame()
    {
        Start();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckWinner()
    {
        if (player1.health.isDead && !player2.health.isDead)
        {
            Debug.Log("Player 2 Won");
            EndGame(2);
        }
        else if (player2.health.isDead && !player1.health.isDead)
        {
            Debug.Log("Player 1 Won");
            EndGame(1);
        }
        else if (player2.health.isDead && player1.health.isDead)
        {
            EndGame(3);
            Debug.Log("Tie");
        }
    }
}
