using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float brightness;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color.RGBToHSV(spriteRenderer.color, out _, out _, out brightness);
    }

    // Update is called once per frame
    void Update()
    {
        if (Dance.epicDancing)
        {
            spriteRenderer.color = Color.black;
            return;
        }

        SetBrightness(brightness);
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetBrightness(Mathf.Clamp(brightness - 0.1f, 0f, 1f));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetBrightness(Mathf.Clamp(brightness + 0.1f, 0f, 1f));
        }
    }

    void SetBrightness(float b)
    {
        brightness = b;
        float h, s;
        Color.RGBToHSV(spriteRenderer.color, out h, out s, out _);
        spriteRenderer.color = Color.HSVToRGB(h, s, brightness);
    }
}
