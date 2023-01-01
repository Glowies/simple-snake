using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SnakeBody : SnakeHazard
{
    public class Factory : PlaceholderFactory<UnityEngine.Object, SnakeBody>
    {
    }
}
