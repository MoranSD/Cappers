using System;
using System.Collections;
using UnityEngine;

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
}
