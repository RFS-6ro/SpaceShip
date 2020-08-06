using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class BaseHealthView : MonoBehaviour
    {
        public abstract void ShowHealth(int newHealth, int maxHealth = 0);
    }
}
