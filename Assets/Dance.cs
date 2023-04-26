using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dance : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int DanceMoveCount { get => danceMoves.Length; }
    public static int EpicDanceMoveCount { get => epicDanceMoves.Length; }

    public bool dancing = false;
    public static bool epicDancing = false;

    public IEnumerator DoDanceMove(bool epic, int index)
    {
        if (dancing) yield break; // Animal may not dance two moves at the same time
        if (epicDancing) yield break; // No dances may occur while player epic-dances

        dancing = true;

        if (epic) epicDancing = true;

        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;
        Vector3 oldScale = transform.localScale;

        yield return StartCoroutine(epic ? epicDanceMoves[index] : danceMoves[index]);

        transform.position = oldPosition;
        transform.rotation = oldRotation;
        transform.localScale = oldScale;

        if (epic) epicDancing = false;

        dancing = false;
    }

    // https://easings.net/#easeInOutBack
    float EaseInOutBack(float x)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;

        return x < 0.5
          ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
          : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }

    // REGISTER DANCE MOVES HERE
    static readonly string[] danceMoves =
    {
        nameof(Spin),
    };

    static readonly string[] epicDanceMoves =
    {
        nameof(Whoosh),
    };

    IEnumerator Spin()
    {
        float t = 0;
        while (t < 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 360 * EaseInOutBack(t));
            float scale = 1 - 0.1f * Mathf.Sin(t * Mathf.PI);
            transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
            t += Time.deltaTime;
        }
    }

    bool IsWithinScreen(Vector3 pos, float margin)
    {
        Vector3 spos = cam.WorldToScreenPoint(pos);
        return spos.x >= -margin && spos.x <= Screen.width + margin
            && spos.y >= -margin && spos.y <= Screen.height + margin;
    }

    IEnumerator Whoosh()
    {
        Vector3 startPos = transform.position;

        Vector3 d = new(0, -1);

        for (int i = 0; i <= 40; i++)
        {

            while (IsWithinScreen(transform.position, 200))
            {
                float speed = Mathf.Pow(i + 5, 1.5f) / 10;
                transform.position += speed * d * 0.1f;
                Vector3 squeeze = new Vector3(Mathf.Pow(d.x == 0 ? 0.25f : 4f, i / 40f), Mathf.Pow(d.y == 0 ? 0.35f : 4f, i / 40f));
                transform.localScale = new Vector3(squeeze.x, squeeze.y, 1);
                yield return null;
            }

            d = new Vector3[]
            {
                new(0, 1),
                new(1, 0),
                new(0, -1),
                new(-1, 0),
            }[Random.Range(0, 4)];

            transform.position = d.x == 0 ? cam.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), -d.y / 2 + 0.5f - 0.2f * d.y, 10))
                : cam.ViewportToWorldPoint(new Vector3(-d.x / 2 + 0.5f - 0.2f * d.x, Random.Range(0.1f, 0.9f), 10));
        }

        transform.position = startPos;
        float t = 0.001f;
        while (t < 1)
        {
            transform.localScale = new Vector3(1 / t, t, 1);
            yield return null;
            t += Time.deltaTime * 2;
        }
    }
}
