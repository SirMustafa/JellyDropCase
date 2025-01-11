using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileShape")]
public class TileStatsSO : ScriptableObject
{
    public enum TileTypeEnum
    {
        OnePiece,
        TwoPieces,
        ThreePieces,
        FourPieces,
    }
    public TileTypeEnum TileType;
    public List<Color> Colors;
}