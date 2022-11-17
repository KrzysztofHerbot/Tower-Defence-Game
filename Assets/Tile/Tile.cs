using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile: MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;
    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    public bool GetIsPlaceable()
    {
        return isPlaceable;
    }

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager!=null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates) != null && coordinates != pathfinder.StartCoordinates)
        {
            if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
            {
                bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
                //Instantiate(towerPrefab, transform.position, Quaternion.identity);
                //isPlaceable = !isPlaced;
                if (isSuccessful)
                {
                    gridManager.BlockNode(coordinates);
                }
                pathfinder.NotifyReceiver();
            }
        }
    }

}
