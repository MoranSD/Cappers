using Gameplay.Player;
using Gameplay.SeaFight.Ship;
using Infrastructure.GameInput;
using Leopotam.Ecs;
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
    public class Cannon : MonoBehaviour, IInteractor
    {
        public bool IsInteractable => reloadTime <= 0 || !isAiming || !isShooting;

        public event Action OnInteracted;

        [SerializeField] private Transform barrelTF;
        [SerializeField] private Transform aimTF;
        [SerializeField] private CannonBall cannonBallPrefab;
        [SerializeField] private Transform aimStartPivot;
        [SerializeField] private float reloadDuration = 10;

        private float reloadTime;
        private bool isAiming;
        private bool isShooting;

        private EcsWorld world;
        private PlayerController player;
        private IInput input;
        private EnemyShip enemyShip;

        public void Initialize(EcsWorld world, PlayerController player, IInput input, EnemyShip enemyShip)
        {
            this.world = world;
            this.player = player;
            this.input = input;
            this.enemyShip = enemyShip;
        }

        private void Update()
        {
            if(reloadTime > 0)
                reloadTime -= Time.deltaTime;

            if (!isAiming) return;

            if (input.IsExitButtonPressed)
            {
                isAiming = false;
                player.SetFreeze(false);
                player.GameCamera.ExitFollowState();
                return;
            }

            if (input.IsInteractButtonPressed)
            {
                reloadDuration += reloadTime;
                //удалить сущность "прицел"
                var ball = Instantiate(cannonBallPrefab, barrelTF.position, Quaternion.identity);
                ball.Fly(barrelTF, aimTF, () =>
                {
                    //todo: check for critical zones (just check for collisions)
                    enemyShip.ApplyDamage(10, false);
                });
            }
        }

        public void Interact()
        {
            OnInteracted?.Invoke();

            player.SetFreeze(true);
            player.GameCamera.EnterFollowState(aimTF);

            isAiming = true;

            //создать сущность "прицел"
        }
    }
}
