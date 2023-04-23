using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    Dance dance;
    SpriteRenderer spriteRenderer;
    Sprite animalSprite;
    [SerializeField] Sprite doge;

    // Start is called before the first frame update
    void Start()
    {
        dance = GetComponent<Dance>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animalSprite = spriteRenderer.sprite;

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
    }

    void DoDanceMove()
    {
        StartCoroutine(dance.DoDanceMove(Random.Range(0, Dance.DanceMoveCount)));
    }
}
