using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;

    public Vector3 offset;

    private GameObject turret;

    private Renderer rend;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation) as GameObject;
        //Build a turret
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        rend.material.color = startColor;
    }
}

