using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public int Damage;
    public int SkillId;
    public MonoBehaviour Owner;

    public bool IsPlayerTheOwner()
    {
        return Owner is Player;
    }

    public bool IsMonsterTheOwner()
    {
        return Owner is Monster;
    }
}
