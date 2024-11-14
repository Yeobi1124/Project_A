using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediator
{
    public abstract class AbstractMediator : MonoBehaviour
    {
        public abstract void Notify(object sender, string ev, params object[] objects);
    }
}