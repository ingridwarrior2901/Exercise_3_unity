using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float duration = 1.0f;
    public float currentSecond;
    private float deltaTime;
    private const int damage = 10;

    void Update()
    {
        deltaTime += Time.deltaTime;
        currentSecond = deltaTime % 60;

        if (currentSecond > duration)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable damagableObject = collision.collider.gameObject.GetComponent<IDamageable>();
        if (damagableObject != null)
        {
            damagableObject.OnDamage(damage);
            Destroy(gameObject);
        }
    }
}
