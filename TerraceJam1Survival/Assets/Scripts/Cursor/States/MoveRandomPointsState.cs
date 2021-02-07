﻿using System;
using UnityEngine;

public class MoveRandomPointsState : MonoBehaviour, IEnemyCursorState
{
    public Action OnComplete { get; set; }

    [SerializeField] private HelperValues.RandomSpeed randomSpeed;
    [SerializeField] private HelperValues.RandomCount randomCount;
    [SerializeField] private float spawnRate = 0.3f;
    [SerializeField] private float maxDistanceFromPlayer = 5;

    private int countRemaining;

    public float GetSpawnRate() => spawnRate;

    public void Act() { }

    private void MoveToPointRecusive()
    {
        if(countRemaining <= 0)
        {
            OnComplete();

            return;
        }

        countRemaining--;

        Vector2 playerPosition = GetTargetAroundPlayer();

        float time = Vector2.Distance(transform.position, playerPosition) / randomSpeed.randomSpeed;

        transform.LeanMove(playerPosition, time).setEaseInOutBack().setOnComplete(MoveToPointRecusive);
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsState");

        countRemaining = randomCount.randomCount;

        MoveToPointRecusive();
    }

    private Vector2 GetTargetAroundPlayer()
    {
        Vector2 playerPosition = Player.GetInstance().transform.position;

        Vector2 randomPosition = playerPosition + UnityEngine.Random.insideUnitCircle * maxDistanceFromPlayer;

        return randomPosition;
    }
}
