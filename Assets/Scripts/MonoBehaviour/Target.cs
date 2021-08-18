using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public enum TargetType
    {
        Default,
        OneTime,
        ThreeTime
    }

    public TargetType targetType = TargetType.Default;

    private Target neighbor;
    public Target Neighbor { get { return neighbor; } set { neighbor = value; } }

    private int index;
    public int Index { get { return index; } set { index = value; } }


    public void Init(Target neighbor, int index)
    {
        this.neighbor = neighbor;
        this.index = index;
    }

}
