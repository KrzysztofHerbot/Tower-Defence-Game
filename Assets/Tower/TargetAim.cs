using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] public float range = 15f;
    [SerializeField] ParticleSystem projectiles;
    Transform target;

    void Update()
    {
        if(target == null)
        {
            FindClosestTarget();
        }
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance <maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    void AimWeapon()
    {
        if (target == null)
        {
            Attack(false);
        }
        else
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);
            if (targetDistance < range)
            {
                Attack(true);
            }
            else { Attack(false); target = null; }
        }
    }

    void Attack(bool isActive)
    {
        var emmisionModule = projectiles.emission;
        emmisionModule.enabled = isActive;
       
    }
}
