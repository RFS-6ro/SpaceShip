using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
    static class GameObjectExtensions
    {
        public static void HandleComponent<T>(this GameObject gameObject, Action<T> handler)
        {
            var component = gameObject.GetComponent<T>();
            if (component != null)
                handler?.Invoke(component);
        }

        public static bool ContainsComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null ? true : false;
        }

        public static void HandleComponent<T>(this Transform transform, Action<T> handler)
        {
            var component = transform.GetComponent<T>();
            if (component != null)
                handler?.Invoke(component);
        }

        public static bool ContainsComponent<T>(this Transform transform)
        {
            return transform.GetComponent<T>() != null ? true : false;
        }

        public static void SetAnimationOffset(this Animator animator, out Coroutine coroutine, float seconds = 0f)
        {
            if (seconds == 0f)
            {
                seconds = UnityEngine.Random.Range(0f, 1f);
            }

            UnityEngine.Object.FindObjectOfType<GameHandlerForManagers>().StartCoroutine(AnimationOffset(animator, seconds), out coroutine);
        }

        private static IEnumerator AnimationOffset(Animator animator, float value)
        {
            animator.enabled = false;
            yield return new WaitForSeconds(value);
            animator.enabled = true;
        }
    }
}
