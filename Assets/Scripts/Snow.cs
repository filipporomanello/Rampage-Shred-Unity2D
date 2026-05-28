using UnityEngine;

// Plays snow particles while the object is in contact with the floor.
public class Snow : MonoBehaviour
{
    [SerializeField] ParticleSystem snowParticles;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Start snow effect when touching the floor.
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Play();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Stop snow effect when leaving the floor.
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Stop();
        }
    }
}
