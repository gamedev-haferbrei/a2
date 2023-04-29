using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 1;

    [SerializeField] GameObject playerObject;
    Dance playerDance;
    Player player;

    public Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        playerDance = playerObject.GetComponent<Dance>();
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDance.dancing) return; // No moving while dancing!

        dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) dir.y += 1;
        if (Input.GetKey(KeyCode.S)) dir.y -= 1;

        if (Input.GetKey(KeyCode.D)) dir.x += 1;
        if (Input.GetKey(KeyCode.A)) dir.x -= 1;

        dir.Normalize();

        if (player.isNinja) dir /= 2;

        if (dir != Vector3.zero & CheatCodes.squidGameRed) CheatCodes.squidGameActive = false;

        transform.position += dir * Time.deltaTime * speed;
    }
}
