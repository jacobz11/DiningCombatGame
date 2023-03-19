using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{

    public Transform seeker;
    public GameObject[] fruits;
    //Grid3D grid;

    //void Awake()
    //{
    //    //grid = GetComponent<Grid3D>();
    //}

    //void Update()
    //{
    //    // Find the nearest fruit and move to it
    //    Transform target = GetNearestFruit();
    //    if (target != null)
    //    {
    //        FindPath(seeker.position, target.position);
    //    }
    //}

    //void FindPath(Vector3 startPos, Vector3 targetPos)
    //{
    //    Node3D startNode = grid.NodeFromWorldPoint(startPos);
    //    Node3D targetNode = grid.NodeFromWorldPoint(targetPos);

    //    List<Node3D> openSet = new List<Node3D>();
    //    HashSet<Node3D> closedSet = new HashSet<Node3D>();
    //    openSet.Add(startNode);

    //    while (openSet.Count > 0)
    //    {
    //        Node3D currentNode = openSet[0];
    //        for (int i = 1; i < openSet.Count; i++)
    //        {
    //            if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
    //            {
    //                currentNode = openSet[i];
    //            }
    //        }

    //        openSet.Remove(currentNode);
    //        closedSet.Add(currentNode);

    //        if (currentNode == targetNode)
    //        {
    //            RetracePath(startNode, targetNode);
    //            return;
    //        }

    //        foreach (Node3D neighbor in grid.GetNeighbors(currentNode))
    //        {
    //            if (!neighbor.walkable || closedSet.Contains(neighbor))
    //            {
    //                continue;
    //            }

    //            int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
    //            if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
    //            {
    //                neighbor.gCost = newMovementCostToNeighbor;
    //                neighbor.hCost = GetDistance(neighbor, targetNode);
    //                neighbor.parent = currentNode;

    //                if (!openSet.Contains(neighbor))
    //                {
    //                    openSet.Add(neighbor);
    //                }
    //            }
    //        }
    //    }
    //}

    //void RetracePath(Node3D startNode, Node3D endNode)
    //{
    //    List<Node3D> path = new List<Node3D>();
    //    Node3D currentNode = endNode;

    //    while (currentNode != startNode)
    //    {
    //        path.Add(currentNode);
    //        currentNode = currentNode.parent;
    //    }

    //    path.Reverse();

    //    // Move character along the path to reach the fruit
    //    foreach (Node3D node in path)
    //    {
    //        // Move the character to the center of the node
    //        Vector3 nodePosition = grid.WorldPointFromNode(node);
    //        seeker.position = nodePosition;

    //        // Check if there is a fruit in the current node
    //        Collider[] colliders = Physics.OverlapSphere(nodePosition, grid.nodeRadius, LayerMask.GetMask("Fruits"));
    //        if (colliders.Length > 0)
    //        {
    //            foreach (Collider collider in colliders)
    //            {
    //                // Collect the fruit
    //                Destroy(collider.gameObject);
    //            }
    //        }

    //        // Wait for a short period before moving to the next node
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    //Transform GetNearestFruit()
    //{
    //    float minDistance = float.MaxValue;
    //    Transform nearestFruit = null;
    //    foreach (GameObject fruit in fruits)
    //    {
    //        float distance = Vector3.Distance(seeker.position, fruit.transform.position);
    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            nearestFruit = fruit.transform;
    //        }
    //        return nearestFruit;
    //    }

    //    int GetDistance(Node3D nodeA, Node3D nodeB)
    //    {
    //        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    //        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
    //        int distZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);

    //        if (distX > distZ)
    //        {
    //            return 14 * distZ + 10 * (distX - distZ) + 10 * distY;
    //        }
    //        return 14 * distX + 10 * (distZ - distX) + 10 * distY;
    //    }

    //}
}


