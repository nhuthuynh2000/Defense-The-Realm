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
    TextMeshPro coordinationText;
    Vector2Int coordinates = new Vector2Int();
    Waypoints waypoint;
    // Start is called before the first frame update
    void Awake()
    {
        coordinationText = GetComponentInChildren<TextMeshPro>();
        waypoint = GetComponent<Waypoints>();
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
        if (!waypoint.IsPlaceable)
        {
            coordinationText.color = blockedColor;
        }
        else
        {
            coordinationText.color = defaultColor;
        }
    }

    private void UpdateCoordinationText()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        coordinationText.text = $"{coordinates.x},{coordinates.y}";
    }
    private void UpdateObjectName()
    {
        transform.gameObject.name = coordinates.ToString();
    }
}
