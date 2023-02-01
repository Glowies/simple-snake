using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleBindLTS : MonoBehaviour
{
    public string LTSKey = string.Empty;

    [Zenject.Inject]
    private ILongTermStorage _lts;
    private Toggle _toggle;

    void Awake()
    {
        TryGetComponent(out _toggle);
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    void OnEnable()
    {
        if(LTSKey == string.Empty)
        {
            return;
        }

        var value = _lts.GetBool(LTSKey);
        _toggle.isOn = value;
    }

    void OnValueChanged(bool value)
    {
        if(LTSKey == string.Empty)
        {
            return;
        }
        
        _lts.SetBool(LTSKey, value);
    }
}
