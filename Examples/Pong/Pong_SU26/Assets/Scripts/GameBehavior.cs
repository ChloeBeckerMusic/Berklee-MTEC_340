using System;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    // Both instance and access point
    public static GameBehavior Instance;
    
    [SerializeField] Player[] _players = new Player[2];
    [SerializeField] private int _targetScore = 3;
    
    [SerializeField] private GameObject _ballPrefab;

    private void Awake()
    {
        // Software Design Patterns
        // Singleton Pattern: Enforces that there is only ever one class
        // throughout the whole execution of the program
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        foreach (Player p in _players)
        {
            p.Score = 0;
        }
        
        SpawnBall();
    }
    
    private void SpawnBall()
    {
        Instantiate(_ballPrefab);
    }

    public void Score(int playerNum)
    {
        _players[playerNum - 1].Score++;

        CheckWinner();
    }

    private void CheckWinner()
    {
        foreach (Player p in _players)
        {
            if (p.Score >= _targetScore)
            {
                ResetGame();
                return;
            }
        }
        
        SpawnBall();
    }
}
