using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public HealthBar healthBar;
    public LevelManager levelManager;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            healthBar.SetHealth(health);

            levelManager.GameOver();
        }
        healthBar.SetHealth(health);
    }
}
