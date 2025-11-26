using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
