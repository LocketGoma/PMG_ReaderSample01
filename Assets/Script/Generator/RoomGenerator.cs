using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//= Room Generate Manager
public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private HashSet<RoomData> RoomList = new HashSet<RoomData>();
    [Header("Room Size / Position setting")]
    [Range(0,100)]
    [SerializeField] private int maxRoomSize = 10;
    [Range(20, 100)]
    [SerializeField] private int maxPosition = 50;

    [Header("Room Make Runs Setting")]
    [Range(0, 100)]
    [SerializeField] private int makeRuns;
    [Range(50, 250)]
    [SerializeField] private int maxRuns = 100;

    [Header("Seed / Try count")]
    [Range(10,20)]   //2^10~2^20
    [SerializeField] public int maxTryExponential = 12;
    [SerializeField] private int maxTry = 1024;

    void Start()
    {

        if (makeRuns <= 0)
            makeRuns = maxRuns;

        maxTry = (int)Mathf.Pow(2, maxTryExponential);
        Debug.Log("Runs : " + makeRuns + "try : " + maxTry);
    }

    public bool GenerateRoom() {
        return GererateRoomData(makeRuns);
    }
    public bool GenerateRoom(int runs) {
        return GererateRoomData(runs);
    }
    public static void DataPrint(RoomData data) {
        Debug.Log("LeftX : " + data.Axis_LX + " ,LeftY : " + data.Axis_LY + "\nRightX :" + data.Axis_RX + " ,RightY : " + data.Axis_RY);
    }
    public static void DataPrint(int index,RoomData data) {
        Debug.Log("index : "+index+"\nLeftX : " + data.Axis_LX + " ,LeftY : " + data.Axis_LY + "\nRightX :" + data.Axis_RX + " ,RightY : " + data.Axis_RY);
    }
    private bool GererateRoomData(int runs) {
        int loopCount = 0;
        while (loopCount++ < maxTry && RoomList.Count <= runs) {
            Debug.Log(System.DateTime.Now.Ticks);
            Random.InitState((int)System.DateTime.Now.Ticks);      //랜덤 시드 초기화
            var LeftKeys = Random.Range(0, maxPosition);
            var RightKeys = Random.Range(0, maxPosition);

            RoomData tempData = new RoomData( 1, LeftKeys, RightKeys, LeftKeys + Random.Range(1, maxRoomSize), RightKeys + Random.Range(1, maxRoomSize));
            DataPrint(loopCount,tempData);

            if (RoomList.Count > 0) {           //중복 체크 
                bool isIntersection = false;
                foreach (RoomData rm in RoomList) {
                    loopCount++;                //중복 체크 안에서도 루프 카운트 소비.

                    if (CheckIntersection(rm,tempData) == true) {
                        Debug.Log("Check!");
                        isIntersection = true;
                        break;
                    }

                }
                if (isIntersection == false) {
                    Debug.Log("maked");
                    RoomList.Add(tempData);
                }
            }
            else {
                RoomList.Add(tempData);
            }
        }

        Debug.Log("Try:" + loopCount + " RoomCount:" + RoomList.Count);

        return (RoomList.Count >= makeRuns);
    }
    
    /*
    private bool CheckIntersection(Room rm1, Room rm2) {
        //return (x1 <= room.x2 && x2 >= room.x1 && y1 <= room.y2 && y2 >= room.y1);
        //1. x1 <= room.x2 == 현재 방의 좌상단 x좌표가 비교 방 우하단 x보다 좌측에 있는가
        //2. x2 >= room.x1 == 현재 방의 우하단 x좌표가 비교 방 좌상단 x보다 우측에 있는가
        //3. y1 <= room.y2 == 현재 방의 좌상단 y좌표가 비교 방 우하단 y보다 위에 있는가
        //4. y2 >= room.y1 == 현재 방의 우하단 y좌표가 비교 방 좌상단 y보다 아래에 있는가

        RoomData rmD1 = rm1.GetRoomData();
        RoomData rmD2 = rm2.GetRoomData();

        return (((rmD1.Axis_LX <= rmD2.Axis_RX) && (rmD1.Axis_RX >= rmD2.Axis_LX)) && ((rmD1.Axis_LY <= rmD2.Axis_RY) && (rmD1.Axis_RY >= rmD2.Axis_LY)))||rmD1==rmD2;
    }
    */
    
    private bool CheckIntersection(RoomData rmD1, RoomData rmD2) {
        //return (x1 <= room.x2 && x2 >= room.x1 && y1 <= room.y2 && y2 >= room.y1);
        //1. x1 <= room.x2 == 현재 방의 좌상단 x좌표가 비교 방 우하단 x보다 좌측에 있는가
        //2. x2 >= room.x1 == 현재 방의 우하단 x좌표가 비교 방 좌상단 x보다 우측에 있는가
        //3. y1 <= room.y2 == 현재 방의 좌상단 y좌표가 비교 방 우하단 y보다 위에 있는가
        //4. y2 >= room.y1 == 현재 방의 우하단 y좌표가 비교 방 좌상단 y보다 아래에 있는가
        //true : 겹침


        return ((rmD1.Axis_LX <= rmD2.Axis_RX) && (rmD1.Axis_RX >= rmD2.Axis_LX) && ((rmD1.Axis_LY <= rmD2.Axis_RY) && (rmD1.Axis_RY >= rmD2.Axis_LY))) || ((rmD2.Axis_LX <= rmD1.Axis_RX) && (rmD2.Axis_RX >= rmD1.Axis_LX) && ((rmD2.Axis_LY <= rmD1.Axis_RY) && (rmD2.Axis_RY >= rmD1.Axis_LY))) || rmD1 == rmD2;
    }

    public HashSet<RoomData> GetRoomList() {
        return RoomList;
    }

}
