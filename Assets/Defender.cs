using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public TextMeshProUGUI defenseHP;
    public AudioSource damageSFX;

    [SerializeField]
    private float hp = 100f;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        defenseHP.text = Mathf.Round(hp).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        damageSFX.Play();

        if(hp <= 0)
        {
            isAlive = false;
            defenseHP.text = "0";
        }
        else
        {
            defenseHP.text = Mathf.Round(hp).ToString();
        }
    }
}
