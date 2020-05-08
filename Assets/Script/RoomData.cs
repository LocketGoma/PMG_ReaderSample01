using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//방&필드 정보
public class RoomData : MonoBehaviour {
    [SerializeField] private uint roomNo;   //방 번호
    [SerializeField] private int axis_LX;
    [SerializeField] private int axis_LY;
    [SerializeField] private int axis_RX;
    [SerializeField] private int axis_RY;
    public int RoomNo { get { return (int)roomNo; } set { roomNo = (uint)value; } }
    public int Axis_LX { get { return Axis_LX; } set { Axis_LX = value; } }
    public int Axis_LY { get { return Axis_LY; } set { Axis_LY = value; } }
    public int Axis_RX { get { return Axis_RX; } set { Axis_RX = value; } }
    public int Axis_RY { get { return Axis_RY; } set { Axis_RY = value; } }

}
