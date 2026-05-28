using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public static int deaths;

    public TextMeshProUGUI deathText;

    void Start()
    {
        UpdateText();
    }

    public void AddDeath()
    {
        deaths++;
        UpdateText();
    }

    void UpdateText()
    {
        deathText.text = "Mortes: " + deaths;
    }
}