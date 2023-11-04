using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletDestoy = 3f;

    public void FireBullet()
    {
        GameObject spawnBullet = Instantiate(bullet, spawnPoint.position,  spawnPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnBullet, bulletDestoy);
    }
}
