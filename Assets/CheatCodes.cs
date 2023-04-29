using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] GameObject npcGroup;
    [SerializeField] GameObject playerObject;

    Player player;
    Animal[] npcs;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player>();
        npcs = npcGroup.GetComponentsInChildren<Animal>();

        foreach ((string name, string callback) in CHEAT_CODES)
        {
            StartCoroutine(RecognizeCheatCode(name, callback));
        }
    }

    IEnumerator RecognizeCheatCode(string name, string callback)
    {
        int pos = 0;

        while (true)
        {
            foreach (char c in Input.inputString)
            {
                if (char.ToLower(c) == char.ToLower(name[pos]))
                {
                    pos++;
                    if (pos == name.Length)
                    {
                        pos = 0;
                        StartCoroutine(callback);
                    }
                }
                else
                {
                    pos = 0;
                }
            }
            yield return null;
        }
    }

    static readonly (string, string)[] CHEAT_CODES =
    {
        ("doge", nameof(Doge)),
        ("ninja", nameof(Ninja)),
        ("squidgame", nameof(SquidGame)),
        ("rainbow", nameof(Rainbow)),
        ("attack", nameof(Attack)),
    };

    IEnumerator Doge()
    {
        foreach (Animal npc in npcs)
        {
            npc.ToggleDoge();
        }
        yield return null;
    }

    IEnumerator Ninja()
    {
        player.ToggleNinja();
        yield return null;
    }

    public static bool squidGameActive = false;
    public static bool squidGameRed = true;
    IEnumerator SquidGame()
    {
        squidGameActive = true;
        squidGameRed = true;
        while (squidGameActive)
        {
            squidGameRed = !squidGameRed;

            float t = Random.Range(1f, 5f);
            while (squidGameActive && t > 0f)
            {
                yield return null;
                t -= Time.deltaTime;
            }
        }

        foreach (Animal npc in npcs)
        {
            npc.SquidGameUndie();
        }
    }

    public static bool rainbowActive = false;
    IEnumerator Rainbow()
    {
        rainbowActive = !rainbowActive;
        yield return null;
    }
    public static bool AttackActive = false;
    IEnumerator Attack()
    {
        AttackActive = !AttackActive;
        float t = 0;
    
        
       // Quaternion originalRot = Quaternion.identity;
    
        if (AttackActive) 
        {
            while (t<1)
            {
                foreach (Animal npc in npcs) 
                {
                    npc.transform.Rotate(new Vector3(0,0,180) * Time.deltaTime);
                }
                yield return null;
                t += Time.deltaTime;

            }
        }
        if (AttackActive == false)
            {
                foreach (Animal npc in npcs)
                {
                    npc.transform.rotation = Quaternion.identity; 
                    Debug.Log("reseted" + npc);
                }
            }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
