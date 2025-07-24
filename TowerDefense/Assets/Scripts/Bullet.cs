using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target; // The target the bullet will aim at

    public float speed = 70f; // Speed of the bullet

    public GameObject impactEffect; // Effect to instantiate when the bullet hits the target

    public void Seek (Transform _target)
    {
        target = _target; // Set the target for the bullet
    }

    // This method is called when the bullet hits the target
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject); // Destroy the bullet if there is no target
            return;
        }

        Vector3 dir = target.position - transform.position; // Calculate the direction to the target
        float distanceThisFrame = speed * Time.deltaTime; // Calculate the distance the bullet will travel this frame

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(); // If the target is within range, hit the target
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); // Move the bullet towards the target


    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); // Instantiate the impact effect at the bullet's position
        Destroy(effectIns, 2f); // Destroy the impact effect after 2 seconds

        Destroy(target.gameObject); // Destroy the target (enemy) that was hit
        Destroy(gameObject); // Destroy the bullet after hitting the target
    }
}
