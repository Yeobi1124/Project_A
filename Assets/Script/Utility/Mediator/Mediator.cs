using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mediator : MonoBehaviour
{
    public abstract void Notify(object sender, string ev, params object[] objects);
}