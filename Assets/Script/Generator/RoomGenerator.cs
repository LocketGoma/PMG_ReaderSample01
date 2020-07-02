using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//= Room Generate Manager
public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private HashSet<RoomData> RoomList = new HashSet<RoomData>();
    [Range(20,100)]
    [SerializeField] private int maxSize=20;
    [SerializeField] private int makeRuns;
    [SerializeField] private int maxRuns = 100;
    [Range(10,20)]   //2^10~2^20
    [SerializeField] public int maxTryExponential = 12;
    [SerializeField] private int maxTry = 1024;

    void Start()
    {

        if (makeRuns <= 0)
            makeRuns = maxRuns;

        maxTry = (int)Mathf.Pow(2, maxTryExponential);
    }

    public bool GenerateRoom() {
        return GererateRoomData(makeRuns);
    }
    public bool GenerateRoom(int runs) {
        return GererateRoomData(runs);
    }
    private bool GererateRoomData(int runs) {
        int loopCount = 0;
        while (loopCount++ < maxTry || RoomList.Count <= runs) {
            RoomData tempData = new RoomData( 1, Random.Range(0, maxSize), Random.Range(0, maxSize), Random.Range(0, maxSize), Random.Range(0, maxSize));
            Debug.Log(RoomList.Count);
            if (RoomList.Count > 0) {           //중복 체크 
                bool isIntersection = false;
                foreach (RoomData rm in RoomList) {
                    loopCount++;                //중복 체크 안에서도 루프 카운트 소비.
                    if (CheckIntersection(tempData, rm) == true) {
                        isIntersection = true;
                        break;
                    }
                }
                if (isIntersection == false)
                    RoomList.Add(tempData);
            }
            else
                RoomList.Add(tempData);
        }  
        return (RoomList.Count >= makeRuns);
    }
    

    private bool CheckIntersection(Room rm1, Room rm2) {
        //return (x1 <= room.x2 && x2 >= room.x1 && y1 <= room.y2 && room.y2 >= room.y1);
        //1. x1 <= room.x2 == 현재 방의 좌상단 x좌표가 비교 방 우하단 x보다 좌측에 있는가
        //2. x2 >= room.x1 == 현재 방의 우하단 x좌표가 비교 방 좌상단 x보다 우측에 있는가
        //3. y1 <= room.y2 == 현재 방의 좌상단 y좌표가 비교 방 우하단 y보다 위에 있는가
        //4. y2 >= room.y1 == 현재 방의 우하단 y좌표가 비교 방 좌상단 y보다 아래에 있는가
        //true : 겹침 / false : 안겹침.

        RoomData rmD1 = rm1.GetRoomData();
        RoomData rmD2 = rm2.GetRoomData();

        return !((rmD1.Axis_LX <= rmD2.Axis_RX) && (rmD1.Axis_RX >= rmD2.Axis_LX) && (rmD1.Axis_RY <= rmD2.Axis_LY) && (rmD1.Axis_RY >= rmD2.Axis_LY));
    }
    private bool CheckIntersection(RoomData rmD1, RoomData rmD2) {
        //return (x1 <= room.x2 && x2 >= room.x1 && y1 <= room.y2 && room.y2 >= room.y1);
        //1. x1 <= room.x2 == 현재 방의 좌상단 x좌표가 비교 방 우하단 x보다 좌측에 있는가
        //2. x2 >= room.x1 == 현재 방의 우하단 x좌표가 비교 방 좌상단 x보다 우측에 있는가
        //3. y1 <= room.y2 == 현재 방의 좌상단 y좌표가 비교 방 우하단 y보다 위에 있는가
        //4. y2 >= room.y1 == 현재 방의 우하단 y좌표가 비교 방 좌상단 y보다 아래에 있는가
        //true : 겹침 / false : 안겹침.


        return !((rmD1.Axis_LX <= rmD2.Axis_RX) && (rmD1.Axis_RX >= rmD2.Axis_LX) && (rmD1.Axis_RY <= rmD2.Axis_LY) && (rmD1.Axis_RY >= rmD2.Axis_LY));
    }

    public HashSet<RoomData> GetRoomList() {
        return RoomList;
    }

}
