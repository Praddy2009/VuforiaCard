using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RevealText : MonoBehaviour
{
    [SerializeField]
    TMP_Text serviceText;
    [SerializeField]
    float time1 = 0.1f, time2 = 0.1f, time3 = 0.1f, inBetweenTime = 1f;
    [SerializeField]
    string[] stringsToShow;
    int index =0;
    [SerializeField]
    int NumBlinks = 4;
    void Start()
    {
        serviceText.text = "";
        StartCoroutine(blinkCursor());
    }

    IEnumerator Reveal()
    {
        serviceText.text = "";
        var numCharsRevealed = 0;
        while (numCharsRevealed < stringsToShow[index].Length)
        {
            while (stringsToShow[index][numCharsRevealed] == ' ')
                ++numCharsRevealed;
            ++numCharsRevealed;

            serviceText.text = stringsToShow[index].Substring(0, numCharsRevealed);
            yield return new WaitForSeconds(time1);
        }

        yield return new WaitForSeconds(inBetweenTime);

        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        var numCharsRemoved = stringsToShow[index].Length;
        while (numCharsRemoved >= 0 )
        {
            serviceText.text = stringsToShow[index].Substring(0, numCharsRemoved);
            numCharsRemoved -= 1;
            yield return new WaitForSeconds(time3);
        }
        serviceText.text = "";
        yield return new WaitForSeconds(0.35f);
        index += 1;
        if (index >= stringsToShow.Length) index = 0;
        StartCoroutine(blinkCursor());
    }


    IEnumerator blinkCursor()
    {
        int i = 0;
        while (i < NumBlinks)
        {
            serviceText.text += "|";
            yield return new WaitForSeconds(time2);
            serviceText.text = serviceText.text.Substring(0, serviceText.text.Length - 1);
            yield return new WaitForSeconds(time2);
            i++;
        }
        StartCoroutine(Reveal());
    }
}
