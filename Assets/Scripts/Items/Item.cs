using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Destroy()
    {
        Destroy(gameObject);
        StartParticles();
    }

    private void StartParticles()
    {
        var particle = Instantiate(_particleSystem, gameObject.transform.position, Quaternion.identity);
        particle.Play();
    }
}