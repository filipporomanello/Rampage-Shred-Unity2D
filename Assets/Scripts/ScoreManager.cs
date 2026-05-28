using TMPro;
using UnityEngine;

// Tracks and displays the player's score.
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    public void AddScore(int additionalScore)
    {
        // Update the cached score and refresh the UI label.
        score += additionalScore;
        scoreText.text = "Score: " + score;
    }
}
