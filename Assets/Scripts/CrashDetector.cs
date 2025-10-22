using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            Invoke("ReloadScene", delayBeforeReload);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
