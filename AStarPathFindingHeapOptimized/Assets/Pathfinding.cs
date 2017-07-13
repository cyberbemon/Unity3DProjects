using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour {

    public Transform seeker, target;
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            FindPath(seeker.position, target.position);
        }
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        //  Convert worldpositions to Nodes 
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        /*
         * openSet - the set of nodes to be evaluated
         * closedSet  - the set of nodes already evaluated
         * add the start node to OpenSet
         * loop
         *  current = node in openSet with the lowest f_cost
         *  remove current from openSet
         *  add current to closedSet
         *  if current is the target node - path has been found 
         *      return
         *  foreach neighbour of the current node
         *      if neighbour is not traversable or neighbour is in closedSet
         *          skip to the next neighbour
         *      
         *      if new path to neighbour is shorter or neighbour is not in openSet
         *          set f_cost of neighbour
         *          set parent of neighbour to current
         *          if neighbour is not in openSet
         *              add neighbour to openSet
         *           
         */
        while(openSet.Count > 0)
        {
            Node currnetNode = openSet.RemoveFirst();
            
            closedSet.Add(currnetNode);

            if (currnetNode == targetNode)
            {
                sw.Stop();
                print("Path Found: " + sw.ElapsedMilliseconds + "ms");
                reTracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currnetNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currnetNode.gCost + getDistance(currnetNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currnetNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    void reTracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currnetNode = endNode;

        while(currnetNode != startNode)
        {
            path.Add(currnetNode);
            currnetNode = currnetNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }
    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY- distanceX);
        }
    }
}
