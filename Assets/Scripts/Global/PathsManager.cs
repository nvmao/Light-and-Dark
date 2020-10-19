using UnityEngine;
using System.Collections;

public class PathsManager : MonoBehaviour
{
    [SerializeField] Transform[] paths;

    public int length()
    {
        return paths.Length;
    }

    public Transform getPath(int index)
    {
        return paths[index];
    }

}
