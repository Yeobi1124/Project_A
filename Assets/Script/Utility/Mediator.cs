using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator<T1, T2>
{
    public T1 t1;
    public T2 t2;

    public virtual void Notify(object sender, string ev){}
}
