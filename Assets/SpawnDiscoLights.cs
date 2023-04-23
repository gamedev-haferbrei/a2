using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDiscoLights : MonoBehaviour
{
    [SerializeField] GameObject discoLightPrefab;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(
                discoLightPrefab,
                cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 10f)),
                Quaternion.identity,
                transform
            );
        }
    }
}
