using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    private float maxHunger = 100f;
    private float maxThrust = 100f;
    [SerializeField]
    private float hunger;
    private float thrust;
    private float health = 100f;

    [SerializeField]
    private Text hungerText;
    [SerializeField]
    private Text thrustText;

    [SerializeField]
    private Image healthStats;
    [SerializeField]
    private Image staminaStats;

    private float waitFor = 4f;

    private void Start()
    {
        hunger = maxHunger;
        thrust = maxThrust;
    }

    private void Update()
    {
        StartCoroutine(DisplayHunger());
        StartCoroutine(DisplayThrust());
    }

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100;
        healthStats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100;
        staminaStats.fillAmount = staminaValue;
    }

    IEnumerator DisplayHunger()
    {
        if (hunger != 0)
        {
            hunger -= 0.1f * Time.deltaTime;
            hungerText.text = "" + hunger;
            yield return new WaitForSeconds(waitFor);
        }
        else
        {
            health -= 1 * Time.deltaTime;
            DisplayHealthStats(health);
        }
    }

    IEnumerator DisplayThrust()
    {
        if (thrust != 0)
        {
            thrust -= 0.1f * Time.deltaTime;
            thrustText.text = "" + thrust;
            yield return new WaitForSeconds(waitFor);
        }
        else
        {
            health -= 1 * Time.deltaTime;
            DisplayHealthStats(health);
        }
    }

}
