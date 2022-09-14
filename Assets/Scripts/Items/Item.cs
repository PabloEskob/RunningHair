using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Destoy()
    {
        Destroy(gameObject);
        StartParticles();
    }

    private void StartParticles()
    {
        var partical = Instantiate(_particleSystem, gameObject.transform.position, Quaternion.identity);
        partical.Play();
    }
}