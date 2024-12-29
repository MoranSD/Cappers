using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UnitSystem.Upgrade.Menu
{
    public class UnitUpgradeCard : MonoBehaviour
    {
        public int Id { get; private set; }

        [Header("Upgrade")]
        [SerializeField] private GameObject upgradeStatePanel;
        [SerializeField] private TextMeshProUGUI upgradeText;
        [Header("Heal")]
        [SerializeField] private GameObject healStatePanel;
        [SerializeField] private TextMeshProUGUI healText;
        [Header("Other")]
        public Button BuyButton;
        [SerializeField] private TextMeshProUGUI BuyButtonText;

        public void DrawHealState(UnitToHealData data)
        {
            healStatePanel.SetActive(true);
            upgradeStatePanel.SetActive(false);

            Id = data.Id;

            healText.text = $"cant upgrade while not full hp \n" +
                $"{data.CurrentHealth}/{data.MaxHealth}";

            BuyButtonText.text = $"{data.Price}$";
        }
        public void DrawHealFailed()
        {

        }
        public void DrawHealSuccess(UnitToUpgradeData data)
        {
            DrawUpgradeState(data);
        }
        public void DrawUpgradeState(UnitToUpgradeData data)
        {
            healStatePanel.SetActive(false);
            upgradeStatePanel.SetActive(true);

            Id = data.Id;

            if (data.IsMaxLevel)
            {
                upgradeText.text = 
                    $"Health:{data.Health} \n" +
                    $"Speed:{data.Speed} \n" +
                    $"Damage:{data.Damage} \n";

                BuyButtonText.text = "MAX";
            }
            else
            {
                upgradeText.text = $"Stats after upgrade: \n" +
                    $"Health:{data.Health} {(data.UpgradeStatsInfo[0] == 'o' ? "+1" : "")} \n" +
                    $"Speed:{data.Speed} {(data.UpgradeStatsInfo[1] == 'o' ? "+1" : "")} \n" +
                    $"Damage:{data.Damage} {(data.UpgradeStatsInfo[2] == 'o' ? "+1" : "")} \n";

                BuyButtonText.text = $"{data.Price}";
            }
        }
        public void DrawUpgradeFailed()
        {

        }
        public void DrawUpgradeSuccess(UnitToUpgradeData data)
        {
            DrawUpgradeState(data);
        }
    }
}
