using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VibratorService : IVibratorService
{
    public void Vibrate() => Handheld.Vibrate();
}
