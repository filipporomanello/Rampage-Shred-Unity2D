using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] ParticleSystem snowParticles;

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Play();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            snowParticles.Stop();
        }
    }
}
