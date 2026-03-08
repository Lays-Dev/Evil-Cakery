// This script sets the screen resolution to 1920x1080 in fullscreen mode when the game starts.
// Attach this script to resolution game object
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

    }
}
