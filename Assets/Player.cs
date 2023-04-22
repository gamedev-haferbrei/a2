using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Dance dance;

    // Start is called before the first frame update
    void Start()
    {

    }

    static readonly KeyCode[] NUMBER_KEYS =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
    };

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Dance.DanceMoveCount; i++)
        {
            if (Input.GetKeyDown(NUMBER_KEYS[i]))
            {
                StartCoroutine(dance.DoDanceMove(i));
            }
        }
    }
}
