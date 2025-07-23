using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

    private Transform target; // The target the turret will aim at
    public float range = 15f; // Range within which the turret can detect targets
    
    public float turnSpeed = 10f; // Speed at which the turret rotates
    //public float fireRate = 1f; // Rate of fire for the turret
    //public float damage = 10f; // Damage dealt by the turret

    public string enemyTag = "Enemy"; // Tag to identify enemies
    public Transform PartToRotate; // The part of the turret that will rotate to face the target

    // Start is called once before the first execution of Update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Update the target every X amount of seconds
    }

    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Find all enemies with the specified tag
        float closestDistance = Mathf.Infinity; // Initialize the closest distance to infinity
        GameObject closestEnemy = null; // Initialize the closest enemy to null

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance) 
            { 
                closestDistance = distanceToEnemy; // Update the closest distance
                closestEnemy = enemy; // Update the closest enemy
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        { 
            target = closestEnemy.transform; // Set the target to the closest enemy's transform
        }

        else
        {
            target = null; // If no enemy is within range, set target to null
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return; // If there is a target, do nothing


        // Rotate the turret towards the target
        Vector3 dir = target.position - transform.position; // Calculate the direction to the target
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // Smoothly rotate towards the target
        PartToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f); // Smoothly rotate the turret towards the target

    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the turret's range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
