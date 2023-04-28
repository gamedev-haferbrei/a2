using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    Dance dance;
    SpriteRenderer spriteRenderer;
    Sprite animalSprite;
    [SerializeField] Sprite doge;

    float rainbowRandomOffset;

    // Start is called before the first frame update
    void Start()
    {
        dance = GetComponent<Dance>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animalSprite = spriteRenderer.sprite;

        rainbowRandomOffset = Random.Range(0f, 1f);

        InvokeRepeating(nameof(DoDanceMove), Random.Range(0f, 2f), 2f);
    }

    public void ToggleDoge()
    {
        if (spriteRenderer.sprite == doge)
        {
            spriteRenderer.sprite = animalSprite;
        }
        else
        {
            spriteRenderer.sprite = doge;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheatCodes.rainbowActive)
        {
            spriteRenderer.color = Color.HSVToRGB((rainbowRandomOffset + Time.time / 4f) % 1f, 0.5f, 1f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void SquidGameDie()
    {
        spriteRenderer.enabled = false;
    }
    public void SquidGameUndie()
    {
        spriteRenderer.enabled = true;
    }

    void DoDanceMove()
    {
        if (CheatCodes.squidGameActive && CheatCodes.squidGameRed)
        {
            Invoke(nameof(SquidGameDie), 1f);
        }
        StartCoroutine(dance.DoDanceMove(false, Random.Range(0, Dance.DanceMoveCount)));
    }
}
