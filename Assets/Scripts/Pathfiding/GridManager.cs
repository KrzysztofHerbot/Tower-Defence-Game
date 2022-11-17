using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize; // (x,y) coordinates of how big is the grid
    [Tooltip("World Grid Size should match UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    //creates a dictionary with VectorInt(x,y) coordinates tyoe and Node type
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); 
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } } //gives access to dictionary with other scripts

    private void Awake()
    {
        CreateGrid();
    }

    //Returns Node type of said 'coordinates' of ot exists in dictionary
    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
        
    }
    //changes isWalkable flag in the node
    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    //loop through every node in our grid, set the ConnectedTo flag to null,
    //isExplored flag to false, isPathFlag to false
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }


    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;

    }


    //creates entries in the dictionary for the said GridSize
    //for every coordinate it creates an entry of said coordinates
    //and using constructor it adds coordinates value and whether
    //it is walkable
    void CreateGrid() 
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
