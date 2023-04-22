using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RecognizeCheatCode("doge", nameof(Doge)));
    }

    IEnumerator RecognizeCheatCode(string name, string callback)
    {
        int pos = 0;
        Action reset = () =>
        {
            pos = 0;
        };

        while (true)
        {
            foreach (char c in Input.inputString)
            {
                if (c == name[pos])
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
                    if (c == name[pos]) pos++;
                }
            }
            yield return null;
        }
    }

    IEnumerator Doge()
    {
        Debug.Log("DOGE");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
