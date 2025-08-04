using UnityEngine;

public class Node : MonoBehaviour
{

    // Color to change to when the mouse hovers over the GameObject
    public Color hoverColor;
    public Vector3 positionOffset;

    // Renderer component to change the color of the GameObject
    private Renderer rend;

    // Color to reset to when the mouse exits the GameObject
    private Color startColor;

    // Reference to the turret GameObject that can be placed on this node
    private GameObject turret;

    // This function is called when the script instance is being loaded
    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    // This function is called when the mouse button is pressed over the collider of this GameObject
    void OnMouseDown ()
    {
        // If a turret is already placed on this node, do nothing
        if (turret != null)
        {
            Debug.Log("Can't place turret here!");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    // This function is called when the mouse exits the collider of this GameObject
    void OnMouseEnter ()
    {
        rend.material.color = hoverColor;
    }

    // This function is called when the mouse exits the collider of this GameObject
    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }

}
