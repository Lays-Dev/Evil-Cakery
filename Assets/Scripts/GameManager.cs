using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern setup

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            // Instead of silently logging an error, throw so callers are
            // forced to handle the missing-manager case explicitly.
            if (_instance == null)
                throw new System.InvalidOperationException(
                    "GameManager instance is NULL. " +
                    "Ensure a GameManager object exists in the scene before accessing it.");

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);
    }

    #endregion
}

// -----------------------------------------------------------------------
// Example: any script that needs the GameManager wraps its access in a
// try-catch so a missing manager never silently corrupts game state.
// -----------------------------------------------------------------------
//
// private void Start()
// {
//     try
//     {
//         GameManager gm = GameManager.Instance;
//         // use gm here ...
//     }
//     catch (System.InvalidOperationException ex)
//     {
//         Debug.LogError($"Could not retrieve GameManager: {ex.Message}");
//         // Gracefully disable this component rather than crashing.
//         enabled = false;
//     }
// }
