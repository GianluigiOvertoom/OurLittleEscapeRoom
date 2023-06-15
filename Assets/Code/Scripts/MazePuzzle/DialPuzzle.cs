using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject dial;
    [SerializeField] private GameObject maze;

    private void LateUpdate()
    {
        Vector3 eulerRotation = new Vector3(dial.transform.eulerAngles.x + 180, dial.transform.eulerAngles.z, dial.transform.eulerAngles.y);

        maze.transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
