using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//방&필드 정보
[Serializable]
public class RoomData {
    [SerializeField] public int roomNumber;   //방 번호
    [SerializeField] public int Axis_LX;
    [SerializeField] public int Axis_LY;
    [SerializeField] public int Axis_RX;
    [SerializeField] public int Axis_RY;

    public int RoomNumber { get { return roomNumber; } set { roomNumber = value; } }

    public RoomData() { roomNumber = -1; Axis_LX = 0; Axis_LY = 0; Axis_RX = 0; Axis_RY = 0; }
    public RoomData(int roomNo, int lx, int ly, int rx, int ry) { roomNumber = roomNo; Axis_LX = lx; Axis_LY = ly; Axis_RX = rx; Axis_RY = ry; }
    public RoomData(int lx, int ly, int rx, int ry) { Axis_LX = lx; Axis_LY = ly; Axis_RX = rx; Axis_RY = ry; }



    public static bool operator > (RoomData rm1, RoomData rm2) {
        return (rm1.Axis_RX - rm1.Axis_LX > rm2.Axis_RX - rm2.Axis_LX) || (rm1.Axis_RY - rm1.Axis_LY > rm2.Axis_RY - rm2.Axis_LY);

    }
    public static bool operator <(RoomData rm1, RoomData rm2) {
        return !(rm1 > rm2);
    }
}
