using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomManager roomManager;
	[SerializeField] private int RoomNo;   //방 번호
    [SerializeField] private int Axis_LX;
    [SerializeField] private int Axis_LY;   //2차원 기준, 3차원일때는 Y값 -> Z값으로 변경.
    [SerializeField] private int Axis_RX;
    [SerializeField] private int Axis_RY;
    
    private void Start() {
        roomManager = transform.parent.gameObject.GetComponent<RoomManager>();
		
		if (roomManager == null)
			Debug.LogError("RoomManager Setting Error!");
    }

    public void Initialized(RoomData roomInput) {
        RoomNo = roomInput.roomNumber;
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

    public RoomData GetRoomData() {
        return new RoomData(RoomNo, Axis_LX, Axis_LY, Axis_RX, Axis_RY);
    }
    
    //사실상 상속이죠?
    public static bool operator > (Room rm1, Room rm2) {

        //return rm1.


        return rm1.GetRoomData() > rm2.GetRoomData();
    }
    public static bool operator <(Room rm1, Room rm2) {
        return !(rm1 > rm2);
    }


}
