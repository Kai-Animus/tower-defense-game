using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton instance of BuildManager
    public static BuildManager instance;

    void Awake()
    {
        // Ensure that there is only one instance of BuildManager in the scene
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in the scene!");
            return;
        }
        // Set the instance to this BuildManager
        instance = this;
    }

    // Prefab for the standard turret that can be built
    public GameObject standardTurretPrefab;

    void Start()
    {
        // Initialize the turret to build with the standard turret prefab
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild ()
    {
        // If no turret is selected to build, return null
        return turretToBuild;
    }

}
