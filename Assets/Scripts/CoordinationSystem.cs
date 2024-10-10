using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinationSystem : MonoBehaviour
{
    [SerializeField] Color32 defaultColor = Color.white;
    [SerializeField] Color32 blockedColor = Color.gray;
    [SerializeField] Color32 exploredColor = Color.red;
    [SerializeField] Color32 pathColor = Color.blue;
    TextMeshPro coordinationText;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    // Start is called before the first frame update
    void Awake()
    {
        coordinationText = GetComponentInChildren<TextMeshPro>();
        gridManager = FindAnyObjectByType<GridManager>();
        UpdateCoordinationText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateCoordinationText();
            UpdateObjectName();
        }
        ColorCoordinates();
        ToggleText();
    }

    private void ToggleText()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            coordinationText.enabled = !coordinationText.enabled;
        }
    }

    private void ColorCoordinates()
    {
        if (gridManager == null) { return; }
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;
        if (!node.isWalkable)
        {
            coordinationText.color = blockedColor;
        }
        else if (node.isExplored)
        {
            coordinationText.color = exploredColor;
        }
        else if (node.isPath)
        {
            coordinationText.color = pathColor;
        }
        else
        {
            coordinationText.color = defaultColor;
        }
    }

    private void UpdateCoordinationText()
    {
        if (gridManager == null) return;
        coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);
        coordinationText.text = $"{coordinates.x},{coordinates.y}";
    }
    private void UpdateObjectName()
    {
        transform.gameObject.name = coordinates.ToString();
    }
}
