using UnityEngine;
using UnityEngine.UI;

// Holds the UI-side representation of a single upgrade button.
// Must be a MonoBehaviour (or at least serializable) to appear in the Inspector.
[System.Serializable]
public class UpgradeButton
{
    public Text upgradeText;
    public Button button;
}

public class UIManager : MonoBehaviour
{
    // ------------------------------------------------------------------
    // Delegate + Event
    // Any system (economy, audio, analytics …) can subscribe to this
    // event and react when the player clicks an upgrade button.
    // UIManager itself never needs to know who is listening.
    // ------------------------------------------------------------------

    /// <summary>
    /// Fired when the player requests an upgrade.
    /// Passes the index of the upgrade that was clicked.
    /// </summary>
    public delegate void UpgradeRequestedHandler(int upgradeIndex);
    public static event UpgradeRequestedHandler OnUpgradeRequested;

    [SerializeField] private UpgradeButton[] upgradeButtons;

    // ------------------------------------------------------------------
    // Lifecycle
    // ------------------------------------------------------------------

    private void Start()
    {
        RegisterButtonListeners();
    }

    private void OnDestroy()
    {
        // Always unregister to avoid memory leaks / stale delegates.
        UnregisterButtonListeners();
    }

    // ------------------------------------------------------------------
    // Listener registration
    // ------------------------------------------------------------------

    private void RegisterButtonListeners()
    {
        if (upgradeButtons == null) return;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i; // capture loop variable for the closure
            upgradeButtons[i].button?.onClick.AddListener(() => OnUpgradeButtonClicked(index));
        }
    }

    private void UnregisterButtonListeners()
    {
        if (upgradeButtons == null) return;

        foreach (var ub in upgradeButtons)
            ub.button?.onClick.RemoveAllListeners();
    }

    // ------------------------------------------------------------------
    // Button handler — raises the delegate event
    // ------------------------------------------------------------------

    private void OnUpgradeButtonClicked(int upgradeIndex)
    {
        // Notify all subscribers (null-check guards against zero listeners).
        OnUpgradeRequested?.Invoke(upgradeIndex);
    }
}

// -----------------------------------------------------------------------
// Example subscriber — attach this to any GameObject that needs to react
// to upgrade clicks (e.g. an economy / resource manager).
// -----------------------------------------------------------------------
//
// public class UpgradeSystem : MonoBehaviour
// {
//     private void OnEnable()
//     {
//         UIManager.OnUpgradeRequested += HandleUpgrade;   // subscribe
//     }
//
//     private void OnDisable()
//     {
//         UIManager.OnUpgradeRequested -= HandleUpgrade;   // unsubscribe
//     }
//
//     private void HandleUpgrade(int upgradeIndex)
//     {
//         Debug.Log($"Upgrade {upgradeIndex} requested!");
//         // apply cost, multiplier, etc.
//     }
// }
