using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] Dance dance;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(DoDanceMove), Random.Range(0f, 2f), 2f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DoDanceMove()
    {
        StartCoroutine(dance.DoDanceMove(Random.Range(0, Dance.DanceMoveCount)));
    }
}
