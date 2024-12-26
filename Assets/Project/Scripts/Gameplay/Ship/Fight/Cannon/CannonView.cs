using Gameplay.SeaFight.Ship.View;
using Gameplay.UnitSystem.Controller;
using Infrastructure.GameInput;
using System;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Ship.Fight.Cannon
{
    public class CannonView : MonoBehaviour, ICannonView, IUnitInteractable
    {
        public event Action OnInteracted;
        public event Action<IUnitController> OnUnitInteracted;

        public bool IsInteractable { get; private set; } = false;
        public Transform Pivot => transform;

        [field: SerializeField] public Transform AimPivot { get; private set; }
        [field: SerializeField] public Transform UnitInteractPivot { get; private set; }

        [SerializeField] private float aimMoveSpeed = 3;
        [SerializeField] private CannonBall cannonBallPrefab;
        [SerializeField] private Transform ballStartPivot;

        private EnemyShipView enemyShipView;
        private IInput input;
        private bool isInitialized;
        private bool isAiming;

        public void Initialize(EnemyShipView enemyShipView, IInput input)//todo: передать вьюху вражеского корабля, чтобы взять границы
        {
            isInitialized = true;
            this.enemyShipView = enemyShipView;
            this.input = input;

            //todo: убрать эффект от метода SetInactive
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (isAiming == false) return;

            var moveDirection = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
            var aimZone = enemyShipView.AimZone;
            AimPivot.position = aimZone.ClampPosition(AimPivot.position + (moveDirection * aimMoveSpeed * Time.deltaTime));
        }

        public void SetInactive()
        {
            //todo: обьект не должен просто скрываться
            if (isInitialized)
                throw new System.Exception();

            IsInteractable = false;
            //todo: временное
            gameObject.SetActive(false);
        }
        public void SetAvailable(bool available) => IsInteractable = available;

        public void Interact() => OnInteracted?.Invoke();
        public void Interact(IUnitController unit) => OnUnitInteracted?.Invoke(unit);

        public void BeginAim()
        {
            AimPivot.gameObject.SetActive(true);
            AimPivot.position = enemyShipView.AimZone.transform.position;
            isAiming = true;
        }
        public void EndAim()
        {
            AimPivot.gameObject.SetActive(false);
            isAiming = false;
        }
        public void DrawCannonFly(Action callBack)
        {
            var ball = Instantiate(cannonBallPrefab, ballStartPivot.position, Quaternion.identity);
            ball.Fly(ballStartPivot, AimPivot, callBack);
        }
        public void OnUnitAim()
        {
            //todo: установить в рандомную позицию в пределах прицеливания
            AimPivot.position = enemyShipView.AimZone.transform.position;
        }
    }
}
