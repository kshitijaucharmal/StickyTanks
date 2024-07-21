using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private PlayerMovement player1script, player2script;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        player1script = player1.GetComponent<PlayerMovement>();
        player2script = player2.GetComponent<PlayerMovement>();
    }

    public void CheckWinner()
    {
        if (player1script.health.isDead && !player2script.health.isDead)
        {
            Debug.Log("Player 2 Won");
        }
        else if (player2script.health.isDead && !player1script.health.isDead)
        {
            Debug.Log("Player 1 Won");
        }
        else if (player2script.health.isDead && player1script.health.isDead)
        {
            Debug.Log("Tie");
        }
    }
}
