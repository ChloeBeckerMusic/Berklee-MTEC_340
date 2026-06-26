using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Backing variable
    private int _score;

    public int Score
    {
        // Shorthand notation for implementation below
        get => _score;

        // get
        // {
        //     return _score;
        // }

        set
        {
            _score = value;
            _scoreText.text = Score.ToString();
        }
    }
    
    [SerializeField] private TMP_Text _scoreText;
}
