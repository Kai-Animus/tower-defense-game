using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    // Turret that can detect and aim at enemies within a specified range
    private Transform target; // The target the turret will aim at

    [Header("Attributes")]

    // Range within which the turret can detect targets
    public float range = 15f; // Range within which the turret can detect targets

    // Fire rate and damage variables
    public float fireRate = 1f; // Rate of fire for the turret
    private float fireCountdown = 0f; // Countdown timer for firing
    //public float damage = 10f; // Damage dealt by the turret

    [Header("Unity Setup Fields")]

    // Tag to identify enemies
    public string enemyTag = "Enemy"; // Tag to identify enemies

    // The part of the turret that will rotate to face the target
    public Transform PartToRotate; // The part of the turret that will rotate to face the target
    public float turnSpeed = 10f; // Speed at which the turret rotates towards the target

    public GameObject bulletPrefab; // Prefab for the bullet that the turret will fire
    public Transform firePoint; // The point from which the turret will fire bullets

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

        // Check if the turret can fire
        if (fireCountdown <= 0f)
        {
            // Fire at the target
            Fire();
            fireCountdown = 1f / fireRate; // Reset the fire countdown based on the fire rate
        }

        // If the turret cannot fire, decrease the fire countdown
        fireCountdown -= Time.deltaTime; // Decrease the fire countdown by the time since the last frame

    }

    // Method to handle firing at the target
    void Fire()
    {

        GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, firePoint.position, firePoint.rotation); // Instantiate the bullet prefab at the fire point's position and rotation
        Bullet bullet = bulletGO.GetComponent<Bullet>(); // Get the Bullet component from the instantiated bullet


        if (bullet != null)
        {
            bullet.Seek(target); // Call the Seek method on the bullet to set its target
        }
        else
        {
            Debug.LogError("Bullet script not found on the bullet prefab!"); // Log an error if the Bullet script is not found
        }
    }

    // Draw a wire sphere in the editor to visualize the turret's range
    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the turret's range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
