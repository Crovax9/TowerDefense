using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Turret1");
        buildManager.SetTurretToBuild(buildManager.turretPrefab);
    }

    public void PurchaseMissileTurret()
    {
        Debug.Log("MissileTurret");
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
