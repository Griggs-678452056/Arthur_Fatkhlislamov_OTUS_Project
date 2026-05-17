using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _ammoText;
    
    public void SetHealth(float currentHealth)
    {
        _healthText.text = $"HP: {currentHealth}";
    }

    public void SetAmmo(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = $"HP: {currentAmmo} / ∞";
    }
}
