using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node NextNode { get; private set; }

    public void SetNextNode(Node nextNode)
    {
        NextNode = nextNode;
    }
}
