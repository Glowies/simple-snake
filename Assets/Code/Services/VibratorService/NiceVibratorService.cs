using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lofelt.NiceVibrations;

public class NiceVibratorService : IVibratorService
{
    public NiceVibratorService()
    {
        HapticController.fallbackPreset = HapticPatterns.PresetType.Selection;
    }

    public void Vibrate()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
    }
}
