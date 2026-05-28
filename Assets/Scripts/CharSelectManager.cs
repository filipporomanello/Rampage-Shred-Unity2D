using UnityEngine;

// Handles the character selection screen and starts the run.
public class CharSelectManager : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject dinoSprite;
    [SerializeField] GameObject frogSprite;
    void Start()
    {
        // Pause gameplay while the player chooses a character.
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
    }

    void BeginGame()
    {
        // Resume gameplay and hide the selection UI.
        Time.timeScale = 1f;
        scoreCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChooseDino()
    {
        // Enable the selected character and begin.
        dinoSprite.SetActive(true);
        BeginGame();
    }

    public void ChooseFrog()
    {
        // Enable the selected character and begin.
        frogSprite.SetActive(true);
        BeginGame();
    }
}
