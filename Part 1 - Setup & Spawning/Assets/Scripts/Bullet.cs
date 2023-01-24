using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float speed;

    private void Start()
    {
        Destroy(gameObject, 10f); //to destroy any stray bullets that didn't collide with anything
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.takeDamage(damage);

            Destroy(gameObject);
        }
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }

    public void setSpeed(float spd)
    {
        speed = spd;
    }

    private void Update()
    {
        transform.position += transform.right * 0.01f;
    }
}
