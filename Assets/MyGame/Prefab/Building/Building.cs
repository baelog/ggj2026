using System;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Building : MonoBehaviour
{
    public int cost;
    public int type;
    public int placeTerrain;
    public GameObject factory;

    public void createBuilding(Vector3 position)
    {
        GameObject newfactory = Instantiate(factory, position, Quaternion.identity);
        newfactory.transform.Find("Out").Rotate(0, 0, 60 * GameManager.Instance.rotation);
    }
}
