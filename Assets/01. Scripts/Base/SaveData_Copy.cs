using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData_Copy : Singleton<SaveData_Copy>
{
    [SerializeField]
    public List<Puzzles> puzzleData_copy = new List<Puzzles>();
}
