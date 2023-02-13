using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps; //added in //PART 2

public class TowerCreator : MonoBehaviour
{
    //for the towers we create
    public Transform createTowerRoot; //PART 2

    //list of towers (prefabs) that will instantiate
    public List<GameObject> towerPrefabs; //PART 1
    
    //id of tower to spawn
    int towerID = -1; //-1 means there's no tower selected //PART 1
    
    //it's in the name
    public Tilemap placeableArea; //PART 2

    void Update(){ //PART 2
        if(CanCreate())
            DetectCreatePoint();
    }

    bool CanCreate(){ //PART 2
        if(towerID == -1)
            return false;
        else
            return true;
    }

    void DetectCreatePoint(){ //PART 2
        if(Input.GetMouseButtonDown(0)){ //detect when mouse is clicked
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get the world space position of the mouse

            var cellPos = placeableArea.WorldToCell(mousePos); //get position of cell in tilemap

            var cellPosCentered = placeableArea.GetCellCenterWorld(cellPos); //get the middle of the cell we're clicking in

            if(placeableArea.GetColliderType(cellPos) == Tile.ColliderType.Sprite){
                CreateTower(cellPosCentered); //spawn the tower
                placeableArea.SetColliderType(cellPos, Tile.ColliderType.None); //disable the collider
            }
        }
    }

    void CreateTower(Vector3 position){ //PART 2
        GameObject tower = Instantiate(towerPrefabs[towerID], createTowerRoot); //make the tower
        tower.transform.position = position; //make sure the tower is in the right spot
        DeselectTower(); //get out of tower selecting mode
    }

    public void SelectTower(int id){ //PART 1
        towerID = id;
    }

    public void DeselectTower(){ //PART 1
        towerID = -1;
    }

}
