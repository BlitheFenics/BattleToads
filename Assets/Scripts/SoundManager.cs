using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Source File Name: SoundManager
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-10-25
 * Program Description: plays sound effect on button pushes
 * Revision History: created it, Added Internal documentation
 */
public class SoundManager : MonoBehaviour
{
    public AudioSource button;

    // Plays sound effect when a menu button is pushed
    public void PlayButton()
    {
        button.Play();
    }
}
