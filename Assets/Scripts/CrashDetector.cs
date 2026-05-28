using UnityEngine;
using UnityEngine.SceneManagement;

// Detects a crash with the floor and resets the scene.
public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 1f;
    [SerializeField] ParticleSystem crashParticles;

    PlayerController playerController;

    void Start()
    {
        // Cache the player controller to disable input on crash.
        playerController = FindFirstObjectByType<PlayerController>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only treat floor collisions as crashes.
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            playerController.DisableControls();
            crashParticles.Play();
            Invoke("ReloadScene", delayBeforeReload);
        }
    }

    void ReloadScene()
    {
        // Reload the first scene after the crash delay.
        SceneManager.LoadScene(0);
    }
}
