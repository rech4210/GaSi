using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventHandler<T> where T : Events<T>
{
    void Event();
}
