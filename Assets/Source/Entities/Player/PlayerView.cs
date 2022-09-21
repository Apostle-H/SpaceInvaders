using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform healthPanel;

    [SerializeField] private GameObject healthPrefab;

    private GameObject[] healthUIObjs;
    private int currentHealth;

    public void DrawHealth(int health)
    {
        healthUIObjs = new GameObject[health];
        
        for (int heartNum = 0; heartNum < health; heartNum++)
        {
            GameObject tempHeart = Instantiate(healthPrefab, healthPanel);

            healthUIObjs[heartNum] = tempHeart;
        }

        currentHealth = health;
    }

    public void UpdateHealth(int newHealth)
    {
        Debug.Log(newHealth);
        if (newHealth > currentHealth && newHealth < healthUIObjs.Length)
        {
            for (int heartNum = currentHealth - 1; heartNum < newHealth - 1; heartNum++)
            {
                healthUIObjs[heartNum].SetActive(true);
            }
        }
        else if (newHealth < currentHealth && newHealth >= 0)
        {
            for (int heartNum = currentHealth - 1; heartNum >= newHealth; heartNum--)
            {
                healthUIObjs[heartNum].SetActive(false);
            }
        }

        currentHealth = newHealth;
    }
}
