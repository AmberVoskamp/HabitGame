using TMPro;
using UnityEngine;

public class UpgradeWeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _stateUpgrade;

    public void SetState(Vector2 upgradeDamageData)
    {
        _stateUpgrade.text = $"Damage: {upgradeDamageData.x} -> {upgradeDamageData.y}";
    }
}
