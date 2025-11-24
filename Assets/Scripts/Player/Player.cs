using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // TODO: die
        }
    }
}
