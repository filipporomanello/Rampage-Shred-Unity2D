using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 1f;
    [SerializeField] ParticleSystem crashParticles;
    PlayerController playerController;
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
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
        SceneManager.LoadScene(0);
    }
}
