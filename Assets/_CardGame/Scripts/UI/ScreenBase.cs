using System.Collections;
using System.Collections.Generic;
using Niksan.UI;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    protected UIManager uIManager;

    public virtual void Init(UIManager _uIManager)
    {
        uIManager = _uIManager;
    }
}
