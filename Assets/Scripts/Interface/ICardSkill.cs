using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardSkill
{
    void Skill<T>() where T :AttackFunc<T>;
}
