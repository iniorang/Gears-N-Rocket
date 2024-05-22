using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypoinNode : MonoBehaviour
{
    public List<Transform> node;

    private void OnDrawGizmos()
    {
        Transform[] path = GetComponentsInChildren<Transform>();
        node = new List<Transform>();
        for (int i = 1; i < path.Length; i++)
        {
            node.Add(path[i]);
        }
        
        for (int i = 0; i < node.Count; i++)
        {
            Vector3 currentNode = node[i].position;
            Vector3 prevNode = Vector3.zero;

            if (i != 0) prevNode = node[i - 1].position;
            else if (i == 0) prevNode = node[node.Count - 1].position;

            Gizmos.DrawLine(prevNode, currentNode);
            Gizmos.DrawSphere(currentNode,1f);
        }
    }
}
