 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D Prefab;
    [SerializeField] float Orb_Damage;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifetime;
    PlayerMovement playermovement;
    float lastTimeFired = 0;

    // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Vector3 direction = (mousePosition - transform.position).normalized;
    // float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    // transform.rotation = Quaternion.Euler(0, 0, rotation - 90f);
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire"))
        {
            float cd = 1f / fireRate;

            if (lastTimeFired + cd <= Time.time)
            {
                lastTimeFired = Time.time;
                Rigidbody2D bullet = Instantiate(Prefab, playermovement.MuzzlePoint.position, playermovement.MuzzlePoint.rotation);
                bullet.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
                Destroy(bullet.gameObject, bulletLifetime);

            }
        }
    }
}
