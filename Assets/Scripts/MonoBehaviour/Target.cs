using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Target neighbor;
    public Target Neighbor { get { return neighbor; } set { neighbor = value; } }
}
