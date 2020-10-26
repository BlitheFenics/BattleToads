using UnityEngine;

/* Source File Name: CameraController
 * Author's Name: Phoenix Makins
 * Student Number: 101193192
 * Date Last Modified: 2020-10-25
 * Program Description: has the camera track the player at a set position
 * Revision History: created it, Added Internal documentation
 */
public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset; 

    // moves camera to player position with some offset so it doesnt follow exactly
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
