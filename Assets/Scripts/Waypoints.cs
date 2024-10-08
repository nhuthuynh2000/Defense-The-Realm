using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get => isPlaceable; }

    private void OnMouseDown()
    {
        if (IsPlaceable)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
