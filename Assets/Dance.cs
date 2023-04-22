using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DanceMove
{
    Spin,
}

public class Dance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int DanceMoveCount { get => danceMoves.Length; }

    bool dancing = false;

    public IEnumerator DoDanceMove(int index)
    {
        if (dancing) yield break; // Animal may not dance two moves at the same time

        dancing = true;
        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;
        Vector3 oldScale = transform.localScale;

        yield return StartCoroutine(danceMoves[index]);

        transform.position = oldPosition;
        transform.rotation = oldRotation;
        transform.localScale = oldScale;
        dancing = false;
    }

    // REGISTER DANCE MOVES HERE
    static readonly string[] danceMoves =
    {
        nameof(Spin),
    };

    IEnumerator Spin()
    {
        float t = 0;
        while (t < 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 360 * t);
            float scale = 1 - 0.2f * Mathf.Sin(t * Mathf.PI);
            transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
            t += Time.deltaTime;
        }
    }
}
