using Gameplay.Game;
using Infrastructure.DataProviding;
using Infrastructure;
using UnityEngine;
using Utils.Interaction;
using Leopotam.Ecs;
using System.Linq;
using Gameplay.UnitSystem.Controller;

namespace Utils
{
    public static class EnvironmentProvider
    {
        public static bool TryGetEnemiesAround(Transform transform, float range, out IAttackTarget[] targets)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            return TryGetTargetsAround(transform, range, gameConfig.EnemyLayer, out targets);
        }

        private static bool TryGetTargetsAround(Transform transform, float range, LayerMask targetLayer, out IAttackTarget[] targets)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetLayer);

            targets = colliders
                .Where(x => x.TryGetComponent(out IAttackTargetView attackTargetView) && attackTargetView.Target.IsDead == false)
                .Select(x => x.GetComponent<IAttackTargetView>().Target)
                .ToArray();

            return targets.Length > 0;
        }

        public static bool HasInteractorAround(Transform transform, float range)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            return Physics.CheckSphere(transform.position, range, gameConfig.InteractorLayer);
        }

        public static bool TryGetInteractor(Transform transform, float range, out IInteractor interactor)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            var colliders = Physics.OverlapSphere(transform.position, range, gameConfig.InteractorLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out interactor))
                    return true;
            }

            interactor = null;
            return false;
        }

        public static bool TryGetUnitAround(Transform transform, float interactRange, out EcsEntity unit)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            LayerMask unitLayer = gameConfig.UnitLayer;
            var colliders = Physics.OverlapSphere(transform.position, interactRange, unitLayer);

            var units = colliders
                .Where(x => x.GetComponent<UnitController>() != null)
                .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
                .Select(x => x.GetComponent<UnitController>().EcsEntity)
                .ToArray();

            if (units.Length > 0)
            {
                unit = units[0];
                return true;
            }
            else
            {
                unit = default;
                return false;
            }
        }
    }
}
