﻿using NaughtyAttributes;
using System;
using UnityEngine;

public class DestroyPlatformState : MonoBehaviour, IEnemyCursorState
{
    public Action onComplete { get; set; }

    [SerializeField] [Tag] private string platformTag;
    [SerializeField] private CursorHelper.RandomSpeed randomSpeed;
    [SerializeField] private float spawnRate = 0.3f;

    private Transform targetTransform;
    private bool pickedUp;

    public float GetSpawnRate() => spawnRate;

    public void Act()
    {
        if (pickedUp)
        {
            targetTransform.transform.position = transform.position;
        }
    }

    public void Enter()
    {
        //Debug.Log("MoveRandomPointsState");

        GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);

        int randomPlatformIndex = UnityEngine.Random.Range(0, platforms.Length);

        targetTransform = platforms[randomPlatformIndex].transform;

        float time = randomSpeed.GetTime(transform.position, targetTransform.transform.position);

        transform.LeanMove(targetTransform.position, time).setEaseInOutBack().setOnComplete(() =>
        {
            Destroy(targetTransform.gameObject);

            if (onComplete != null)
            {
                onComplete();
            }

            //pickedUp = true;
            //Vector2 playerPosition = CursorHelper.GetPositionAroundPlayer(maxDistanceFromPlayer);

            //transform.LeanMove(playerPosition, time).setEaseInOutBack().setOnComplete(() =>
            //{
            //    pickedUp = false;
            //    targetTransform.GetComponent<Renderer>().material.SetColor("_Color", new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f)));

            //    if (onComplete != null)
            //    {
            //        onComplete();
            //    }
            //});
        });
    }
}
