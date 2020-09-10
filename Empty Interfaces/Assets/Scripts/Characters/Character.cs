﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool isReadyToShoot = false;
    public float timeBetweenShots;
    public int maxHP;
    public int hp;
    protected float lowCoolDown;
    protected float highCoolDown;
    protected float coolDownCopy;
    protected bool isShooting = true;
    public float randomModifier;
    protected static int kills;

    // Start is called before the first frame update
    public virtual void Start()
    {
        kills = 0;
        hp = maxHP;
        coolDownCopy = timeBetweenShots;
        Debug.Log("START VALUE OF COPY = " + coolDownCopy);
    }

    protected void CheckIfAlive()
    {
        // if character is dead
        if (hp <= 0)
        {
            // destroys the character
            Destroy(gameObject);
            kills++;
        }
    }

    public int GetKills()
    {
        return kills;
    }

    protected virtual void ShootReadyCheck()
    {
        timeBetweenShots -= Time.deltaTime; // timer is decreased

        // generates a random cooldown timer based on the random modifier
        lowCoolDown = coolDownCopy - (randomModifier * coolDownCopy);
        highCoolDown = coolDownCopy + (randomModifier * coolDownCopy);

        // timer cant go below 0
        if (timeBetweenShots <= 0)
        {
            timeBetweenShots = 0;
        }

        // if timer is 0
        if (timeBetweenShots <= 0)
        {
            timeBetweenShots = Random.Range(lowCoolDown, highCoolDown);
            // character shoots
            Shoot();
        }
    }

    protected void Shoot()
    {
        isReadyToShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if character collides with a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            // destroys projectile
            Destroy(other.gameObject);
            // damages character
            hp--;
        }
    }

}
