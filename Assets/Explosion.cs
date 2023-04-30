using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        var crl = spriteRenderer.color;
        if (crl.a > 0)
        {
            crl.a -= 0.01f;
            spriteRenderer.color = crl;
        }
        if (crl.a == 0)
        { Destroy(this.gameObject); }
    }
}
