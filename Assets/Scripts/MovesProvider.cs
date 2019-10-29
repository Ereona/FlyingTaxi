using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovesProvider : MonoBehaviour
{
    public abstract List<Move> GetMoves();
}
