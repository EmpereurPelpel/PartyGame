using UnityEngine;

public class Graph : MonoBehaviour
{
    private void Start()
    {
        InitAllNextNodes();
    }

    private void InitAllNextNodes()
    {
        Node previousNode = null;
        foreach (Transform nodeTransform in transform)
        {
            Node currentNode = nodeTransform.GetComponent<Node>();
            if (previousNode != null)
                previousNode.SetNextNode(currentNode);
            previousNode = currentNode;
        }
        // Returns the first matching Node so it's the first one
        previousNode.SetNextNode(GetComponentInChildren<Node>());
    }
}
