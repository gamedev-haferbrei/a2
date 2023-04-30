using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] GameObject npcGroup;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject doomObject;

    DoomHUD doomHud;
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
        ("iddqd", nameof(Doom))
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

    IEnumerator Doom()
    {
        doomHud = doomObject.GetComponent<DoomHUD>();
        float t = 0;
        var sr = player.GetComponent<SpriteRenderer>(); 
        var clr = sr.color;
        if (!player.isDoom)
        {
            while (t <= 2)
            {
                player.transform.localScale += new Vector3(5 * t, 5 * t) * Time.deltaTime;
                if (t >= 1)
                {
                    clr.a -= clr.a * 0.1f;
                    sr.color = clr;

                }
                t += Time.deltaTime;
                yield return null;
            }
            while (doomHud.transform.position.y <= player.transform.position.y - 2.55f) 
            {
                doomHud.transform.position += new Vector3(0, player.transform.position.y + 4) * Time.deltaTime;
                t += Time.deltaTime;
                yield return null;
            }
            doomHud.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2.55f);
            player.isDoom = !player.isDoom;
        }
        else
        {
            player.isDoom = !player.isDoom;
            clr.a = 0.1f;
            while (t <= 2)
            {
                player.transform.localScale += new Vector3(-5 * t, -5 * t) * Time.deltaTime;
                if (t >= 1)
                {
                    if (player.isNinja)
                    {
                        clr.a = 0.5f;
                        sr.color = clr;
                    }
                    else
                    {
                        clr.a += 0.05f;
                        sr.color = clr;
                    }
                }
                doomHud.transform.position += new Vector3(0, player.transform.position.y - 7) * Time.deltaTime;
                t += Time.deltaTime;
                yield return null;
            }
            doomHud.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 8f);
            player.transform.localScale = new Vector3(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
