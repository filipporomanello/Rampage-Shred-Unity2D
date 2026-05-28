using UnityEngine;
using UnityEngine.SceneManagement;

// Triggers the finish effect and reloads the scene when the player reaches the end.
public class FinishLine : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 1f;
    [SerializeField] ParticleSystem finishParticles;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only react to the player crossing the finish line.
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex)
        {
            finishParticles.Play();
            Invoke("ReloadScene", delayBeforeReload);
        }
    }

    void ReloadScene()
    {
        // Reload the first scene after the finish delay.
        SceneManager.LoadScene(0);
    }
}
