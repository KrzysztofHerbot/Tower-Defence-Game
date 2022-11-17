using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();

    //reference to see whether the node has been explored
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

   Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid; //assignes public dictionary to local
            startNode = grid[startCoordinates]; //old code: new Node(startCoordinates, true);
            destinationNode = grid[destinationCoordinates]; //old code: new Node(destinationCoordinates, true);

        }
        //Creates Start and End nodes and tell whether walkable or not

    }
    void Start()
    {
        startNode = gridManager.Grid[startCoordinates]; //old code: new Node(startCoordinates, true);
        destinationNode = gridManager.Grid[destinationCoordinates]; //old code: new Node(destinationCoordinates, true);

        GetNewPath(); //it is fixed now that we fixe script execution order

        //Invoke("GetNewPath", 0.1f); //delays pathfinding so Tiles have time to be blocked in their
        //own script, otherwise pathfinding takes place before tiles are blocked
    }

   /* void StartPathfinding()
    {
        BreadthFirstSearch();
        BuildPath();
    } */

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    //checks for neighbored coordinates
    void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            //adds directions(0,1) to the current Node so we get neighbours coordinates
            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;

            //it checks if the neighbor was already added to the grid
            if(grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(grid[neighbourCoords]);
            }
        }

        foreach (Node neighbour in neighbours)
        {
            //We check it neighbour was already reached and is walkable
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                //adds the node to which the neighbor is connected to(currentSearchNode)
                neighbour.connectedTo = currentSearchNode;
                //adds the unexplored neighbor to the 'reached' and frontier queue
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }

    }
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        // We are adding starting node to the begining of the queue
        //than we add the node of said coordinates to the reached dictionary
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        //we are looking through our neighbors and exploring them
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            //stops the loop
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true; 
        while(currentNode.connectedTo !=null) // && currentNode.coordinates != startCoordinates)  - useless part of code, start doesnt have connectedTo Node
        {
            //adds the connected node to the list
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse(); //reverses path so it goes from the start coordinates
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            //if the path is shorter than 1 tile than it means
            //getNewpath didn't found a path to destination and returned empty path list
            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }

        return false;
    }

    public void NotifyReceiver()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
