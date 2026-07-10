using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    // Both instance and access point
    public static GameBehavior Instance;

    private Utilities.GameState _state;
    public Utilities.GameState State
    {
        get => _state;

        set
        {
            _state = value;
            
            _message.enabled = State == Utilities.GameState.Pause;
        }
    }
    
    [SerializeField] private TMP_Text _message;
    
    [SerializeField] Player[] _players = new Player[2];
    [SerializeField] private int _targetScore = 3;
    
    [SerializeField] private GameObject _ballPrefab;

    private float _durationBetweenPoints = 1.0f;

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

        // Set initial state
        State = Utilities.GameState.Play;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Ternary operator
            State = State == Utilities.GameState.Play ?     // Condition
                Utilities.GameState.Pause :                 // Passing
                Utilities.GameState.Play;                   // Failing
        }
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
        
        // Apply a delay when a player scores to give respite
        Invoke(nameof(SpawnBall), _durationBetweenPoints);
    }
}
