using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPreFab;

    public AudioSource shootSound;

    public float bulletForce = 20f;

    public bool canShoot = true;
    public LevelManager levelManager;
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {
        if (!canShoot || levelManager.isPaused || levelManager.isGameOver) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shootSound.Play();
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
