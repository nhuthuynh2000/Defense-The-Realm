using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5f)] float speed = 1f;
    [SerializeField][Range(5, 25)] int rewardGold = 25;
    [SerializeField][Range(5, 15)] int penaltyGold = 15;


    List<Node> path = new List<Node>();
    Bank bank;

    GridManager gridManager;
    Pathfinder pathfinder;

    public int RewardGold { get => rewardGold; }
    public int PenaltyGold { get => penaltyGold; }

    // Start is called before the first frame update
    void OnEnable()
    {
        ResetToStart();
        RecalculatePath(true);
    }
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    private void ResetToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        StealGold(penaltyGold);
        gameObject.SetActive(false);
    }
    public void GiveGold(int amount)
    {
        if (bank == null) return;
        bank.Deposit(amount);
    }
    public void StealGold(int amount)
    {
        if (bank == null) return;
        bank.Withdraw(amount);
    }

}
