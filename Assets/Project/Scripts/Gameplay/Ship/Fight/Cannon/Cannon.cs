using Gameplay.Player;
using Gameplay.SeaFight.Ship;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;
using System;
using System.Collections;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Ship.Fight.Cannon
{
    public class CannonBall : MonoBehaviour
    {
        [SerializeField] private float flightTime = 2f;
        [SerializeField] private AnimationCurve heightCurve;

        public void Fly(Transform startPoint, Transform endPoint, Action callBack)
        {
            StartCoroutine(FlightProcess(startPoint.position, endPoint.position, callBack));
        }

        private IEnumerator FlightProcess(Vector3 start, Vector3 end, Action callBack)
        {
            float t = 0;

            while (t <= 1)
            {
                transform.position = Vector3.Lerp(start, end, t) + Vector3.up * heightCurve.Evaluate(t);
                t += Time.deltaTime / flightTime;
                yield return new WaitForEndOfFrame();
            }

            transform.position = end;
            callBack?.Invoke();

            Destroy(gameObject);
        }
    }
    public interface ICannonView
    {
        event Action OnPlayerInteract;
        event Action OnUnitInteract;

        Transform AimPivot { get; }

        void SetAvailable(bool available);
        void BeginAim();
        void EndAim();
        void DrawCannonFly(Action callBack);
    }
    public class CannonView : MonoBehaviour, ICannonView
    {
        public event Action OnPlayerInteract;
        public event Action OnUnitInteract;

        [field: SerializeField] public Transform AimPivot { get; private set; }

        [SerializeField] private TriggerInteractor interactor;
        [SerializeField] private CannonBall cannonBallPrefab;
        [SerializeField] private Transform ballStartPivot;
        [SerializeField] private Transform aimStartPivot;

        public void Initialize()
        {
            interactor.OnInteracted += OnPlayerInteracted;
        }

        public void Dispose()
        {
            interactor.OnInteracted -= OnPlayerInteracted;
        }

        public void SetAvailable(bool available) => interactor.IsInteractable = available;

        private void OnPlayerInteracted() => OnPlayerInteract?.Invoke();
        private void OnUnitInteracted() => OnUnitInteract?.Invoke();

        public void BeginAim()
        {
            //show and activate aim object
        }

        public void EndAim()
        {
            //hide and deactivate aim object
        }

        public void DrawCannonFly(Action callBack)
        {
            var ball = Instantiate(cannonBallPrefab, ballStartPivot.position, Quaternion.identity);
            ball.Fly(ballStartPivot, AimPivot, callBack);
        }
    }

    public class Cannon : ITickable
    {
        public bool IsAvailable => reloadTime <= 0 && !isAiming && !isShooting;

        private float reloadTime;
        private bool isAiming;
        private bool isShooting;

        private readonly PlayerController player;
        private readonly IInput input;
        private readonly EnemyShip enemyShip;
        private readonly ICannonView view;
        private float reloadDuration;

        public Cannon(PlayerController player, IInput input, EnemyShip enemyShip, ICannonView view)
        {
            this.player = player;
            this.input = input;
            this.enemyShip = enemyShip;
            this.view = view;
            reloadDuration = 10;
        }

        public void Initialize()
        {
            view.OnPlayerInteract += OnPlayerInteract;
            view.SetAvailable(true);
        }

        public void Dispose()
        {
            view.OnPlayerInteract -= OnPlayerInteract;
        }

        public void Update(float deltaTime)
        {
            if(reloadTime > 0)
            {
                reloadTime -= deltaTime;

                if(reloadTime <= 0)
                    view.SetAvailable(true);
            }

            if (!isAiming) return;

            if (input.IsExitButtonPressed)
            {
                ExitInteraction();
                return;
            }

            if (input.IsInteractButtonPressed)
            {
                reloadDuration += reloadTime;
                view.SetAvailable(false);

                ExitInteraction();
                isShooting = true;
                view.EndAim();

                view.DrawCannonFly(() =>
                {
                    isShooting = false;
                    enemyShip.ApplyDamage(view.AimPivot, 10);
                });
            }
        }

        private void OnPlayerInteract()
        {
            if (isAiming)
                throw new System.Exception();

            if (reloadTime > 0)
                return;

            isAiming = true;
            view.BeginAim();

            player.SetFreeze(true);
            player.GameCamera.EnterFollowState(view.AimPivot);
        }

        private void ExitInteraction()
        {
            isAiming = false;
            player.SetFreeze(false);
            player.GameCamera.ExitFollowState();
        }
    }
}
