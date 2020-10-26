using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Source File Name: HealthBar
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-10-25
 * Program Description: Sets the health bar value and slides the bar accordingly
 * Revision History: created it, Added Internal documentation
 */
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        // Sets the max health value 
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        // Sets the current health value

        slider.value = health;
    }
}
