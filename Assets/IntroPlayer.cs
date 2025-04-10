using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    public TextMeshPro introText;

    private string[] texts;

    private int index = 0;

    private void Awake()
    {
        texts = new string[8];
        texts[0] = "Hello and Welcome to the VR Tower Defense Simulator";
        texts[1] = "In This Game, You Are the Tower!";
        texts[2] = "You Will be Tasked With Taking Down Waves of Enemies!";
        texts[3] = "You Gain Score for Each Hit on an Enemy...";
        texts[4] = "You Gain Extra Score for Each Kill!";
        texts[5] = "Try to Get the Highest Score You Can...";
        texts[6] = "Before Your HP Hits Zero!";
        texts[7] = "Hit the Green Button to Start!";
    }

    // Update is called once per frame
    void Update()
    {
        if (index < texts.Length)
        {
            introText.text = texts[index];
        }
    }

    public void nextText()
    {
        StartCoroutine(IncrementIndex());
    }

    IEnumerator IncrementIndex()
    {
        index++;
        if(index >= texts.Length)
        {
            introText.GetComponentInParent<Animator>().enabled = true;
            yield return new WaitForSeconds(1f);
            Destroy(introText.GetComponentInParent<Animator>());
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
    }
}
