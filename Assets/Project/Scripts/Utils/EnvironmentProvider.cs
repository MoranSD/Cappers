using Gameplay.Game;
using Infrastructure.DataProviding;
using Infrastructure;
using UnityEngine;
using Utils.Interaction;
using Leopotam.Ecs;
using System.Linq;
using Gameplay.Game.ECS;

namespace Utils
{
    public static class EnvironmentProvider
    {
        public static bool TryGetEnemyHoldersAround(Transform transform, float range, out IEcsEntityHolder[] enemyHolders)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            return TryGetEntityHoldersAround(transform, range, gameConfig.EnemyLayer, out enemyHolders);
        }

        public static bool TryGetUnitHoldersAround(Transform transform, float range, out IEcsEntityHolder[] unitHolders)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            return TryGetEntityHoldersAround(transform, range, gameConfig.UnitLayer, out unitHolders);
        }

        public static bool TryGetEntitiesAround(Transform transform, float range, LayerMask targetLayer, out EcsEntity[] entities)
        {
            bool hasEntitiesAround = TryGetEntityHoldersAround(transform, range, targetLayer, out var holders);

            entities = holders
                .Select(x => x.EcsEntity)
                .Where(x => x.IsAlive())
                .ToArray();

            return hasEntitiesAround;
        }

        public static bool TryGetEntityHoldersAround(Transform transform, float range, LayerMask targetLayer, out IEcsEntityHolder[] holders)
        {
            var colliders = Physics.OverlapSphere(transform.position, range, targetLayer);

            holders = colliders
                .Where(x => x.GetComponent<IEcsEntityHolder>() != null)
                .Select(x => x.GetComponent<IEcsEntityHolder>())
                .Where(x => x.EcsEntity.IsAlive())
                .ToArray();

            return holders.Length > 0;
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

        public static IEcsEntityHolder GetClosestHolder(Vector3 position, IEcsEntityHolder[] holders)
        {
            var closestTarget = holders[0];

            foreach (var target in holders)
            {
                if (Vector3.Distance(position, target.transform.position) <
                    Vector3.Distance(position, closestTarget.transform.position))
                    closestTarget = target;
            }

            return closestTarget;
        }
    }
}
