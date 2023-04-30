using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomHUD : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject explosionPrefab;
    SpriteRenderer spriteRenderer;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDoom & Input.GetMouseButtonDown(0))
        {
            Instantiate(explosionPrefab, TargetArea(), Quaternion.identity);
        }
    }
    Vector3 TargetArea()
    {
        Vector3 target = new Vector3();
        target = 1 * Random.insideUnitSphere;
        float x = transform.position.x + 0.5f;
        float y = transform.position.y + 5.5f;
        target.x += x;
        target.y += y;
        return target;
    }
}
