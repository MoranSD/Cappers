using System;
using System.Linq;
using UnityEngine;
using Gameplay.World.Data;

namespace Gameplay.Ship.Map.View.IconsHolder
{
    public class MapIconsHolder : MonoBehaviour
    {
        public event Action<int> OnClickOnLocation;

        [SerializeField] private MapLocationIcon[] icons;

        public void Initialize(GameWorldConfig worldConfig)
        {
            var locationsConfigs = worldConfig.Locations;

            for (int i = 0; i < icons.Length; i++)
            {
                var icon = icons[i];

                if(locationsConfigs.Any(x => x.Id == icon.LocationId) == false)
                {
                    icon.gameObject.SetActive(false);
                    Debug.LogError($"Missing location config {icon.LocationId}");
                    continue;
                }

                var locationConfig = locationsConfigs.First(x => x.Id == icon.LocationId);

                var iconLocationId = icon.LocationId;
                icon.SelectButton.onClick.AddListener(() => OnClickOnLocation?.Invoke(iconLocationId));
                icon.LocationNameText.text = locationConfig.Name;
                //TODO: set icon from config
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < icons.Length; i++)
                icons[i].SelectButton.onClick.RemoveAllListeners();
        }

        public void SetIconsVisibility(params int[] visibleLocationsIds)
        {
            for (int i = 0; i < icons.Length; i++)
                icons[i].gameObject.SetActive(ContainsInVisibleIds(icons[i].LocationId));

            bool ContainsInVisibleIds(int iconLocationId) => visibleLocationsIds.Contains(iconLocationId);
        }
    }
}
