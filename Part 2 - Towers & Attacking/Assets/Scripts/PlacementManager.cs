using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject basicTowerObject;

    private GameObject dummyPlacement;

    private GameObject hoverTile;

    public Camera cam;

    public LayerMask mask;
    public LayerMask towerMask;

    public bool isBuilding;

    private bool hoverOverPath;

    public void Start()
    {
        StartBuilding();
    }

    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GetCurrentHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0,0), 0.1f, mask, -100, 100);

        if(hit.collider != null)
        {
            //checking if the object detected is a maptile
            if(MapGenerator.mapTiles.Contains(hit.collider.gameObject))
            {
                //checking if the maptile detected is NOT a pathtile
                if(!MapGenerator.pathTiles.Contains(hit.collider.gameObject))
                {
                    hoverTile = hit.collider.gameObject;
                    hoverOverPath = false;
                }
                else
                    hoverOverPath = true;
            }
        }
    }

    public bool CheckForTower()
    {
        bool towerOnSlot = false;

        Vector2 mousePosition = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0,0), 0.1f, towerMask, -100, 100);

        if(hit.collider != null)
            towerOnSlot = true;

        return towerOnSlot;
    }

    public void PlaceBuilding()
    {
        if(hoverTile != null)
        {
            if(CheckForTower() == false && !hoverOverPath)
            {
                GameObject newTowerObject = Instantiate(basicTowerObject);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;

                EndBuilding();
            }
        }
    }

    public void StartBuilding()
    {
        isBuilding = true;

        dummyPlacement = Instantiate(basicTowerObject);

        if(dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }
        if(dummyPlacement.GetComponent<BarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BarrelRotation>());
        }
    }

    public void EndBuilding()
    {
        isBuilding = false;

        if(dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    public void Update()
    {
        if(isBuilding)
        {
            if(dummyPlacement != null)
            {
                GetCurrentHoverTile();

                if(hoverTile != null)
                    dummyPlacement.transform.position = hoverTile.transform.position;
            }
        }

        if(Input.GetButtonDown("Fire1"))
            PlaceBuilding();
    }
}
