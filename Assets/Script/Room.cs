using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int RoomNo;   //방 번호
    [SerializeField] private int Axis_LX;
    [SerializeField] private int Axis_LY;
    [SerializeField] private int Axis_RX;
    [SerializeField] private int Axis_RY;
    public RoomManager roomManager;


    private void Start() {
        roomManager = transform.parent.gameObject.GetComponent<RoomManager>();
    }

    public void Initialized(RoomData roomInput) {
        RoomNo = roomInput.RoomNo;
        Axis_LX = roomInput.Axis_LX;
        Axis_LY = roomInput.Axis_LY;
        Axis_RX = roomInput.Axis_RX;
        Axis_RY = roomInput.Axis_RY;

        Debug.Log(RoomNo);

        InitBatch();
    }

    public void InitBatch() {
        gameObject.transform.position = new Vector3(Axis_LX, 0,Axis_LY );
        gameObject.transform.localScale = new Vector3(Axis_RX - Axis_LX, 1,Axis_RY - Axis_LY);

    }


}
