using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class GameSettings : ScriptableObject {
    public bool CoOp = false;
    public string LevelName = "";
}
