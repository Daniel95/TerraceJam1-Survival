﻿using System;
using UnityEngine;

public class MoveRandomDirectionState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private CursorHelper.RandomTime randomTime;
    [SerializeField] private float spawnRate;

    public float GetSpawnRate() => spawnRate;

    public void Act() { }

    public void Enter()
    {
        //Debug.Log("MoveRandomDirectionState");

        var randomDirection = UnityEngine.Random.insideUnitCircle.normalized;

        Vector3 randomPosition = randomDirection * randomTime.randomTime;

        float time = randomSpeed.GetTime(transform.position, randomPosition);
        transform.LeanMove(randomPosition, time).setEaseInOutBack().setOnComplete(() =>
        {
            if (onComplete != null)
            {
                onComplete();
            }
        });

    }
}
