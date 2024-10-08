using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Waypoints> path = new List<Waypoints>();
    [SerializeField][Range(0.1f, 5f)] float speed = 1f;
    [SerializeField][Range(5, 25)] int rewardGold = 25;
    [SerializeField][Range(5, 15)] int penaltyGold = 15;

    Bank bank;

    public int RewardGold { get => rewardGold; }
    public int PenaltyGold { get => penaltyGold; }

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ResetToStart();
        StartCoroutine(FollowPath());
    }
    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    private void ResetToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FindPath()
    {
        path.Clear();
        GameObject waypoints = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in waypoints.transform)
        {
            path.Add(waypoint.GetComponent<Waypoints>());
        }
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoints waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.gameObject.transform.position;
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
