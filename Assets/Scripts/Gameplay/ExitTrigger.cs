using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.Victory();
        }
    }
}
