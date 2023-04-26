using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomColor();
        InvokeRepeating(nameof(SetRandomColor), Random.Range(0f, 1f), 1f);
    }

    void SetRandomColor()
    {
        if (Dance.epicDancing || CheatCodes.squidGameActive) return;
        spriteRenderer.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheatCodes.squidGameActive) spriteRenderer.color = CheatCodes.squidGameRed ? Color.red : Color.green;
    }
}
