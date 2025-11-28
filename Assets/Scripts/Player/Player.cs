using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public HealthBar healthBar;
    public LevelManager levelManager;
    public AudioSource heartbeatAudioSource;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            healthBar.SetHealth(health);
            heartbeatAudioSource.Stop();
            levelManager.GameOver();

        }else if(health <= 30 && !heartbeatAudioSource.isPlaying)
        {
            heartbeatAudioSource.Play();
        }
        healthBar.SetHealth(health);
    }
}
