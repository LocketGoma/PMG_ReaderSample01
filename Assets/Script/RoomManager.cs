using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private LoadData loadData;
    [SerializeField] private GameObject[] RoomList;     //Room List
    [SerializeField] private GameObject RoomSample;     //Room Sample Prefab
    [SerializeField] private int roomCount;
    [SerializeField] private JsonParser jsonParser;
    [SerializeField] private RoomGenerator roomGenerator;

    private HashSet<RoomData> roomListData;
    public int RoomCount { get { return roomCount; } set { roomCount = value; } }


    // Start is called before the first frame update
    void Start()
    {
        if (jsonParser!=null)
             MakeRoomFromJson();

       // MakeRoomFromGenerator();

    }
    public void MakeRoomFromJson() {

        jsonParser.Init();
        loadData = jsonParser.LoadData;
        roomCount = loadData.RoomCount;
        Debug.Log(loadData.RoomCount);
        RoomList = new GameObject[loadData.RoomCount];


        for (int i = 0; i < roomCount; i++) {
            Instantiate(RoomSample).transform.parent = gameObject.transform;
            RoomList[i] = transform.GetChild(i).gameObject;
            RoomList[i].GetComponent<Room>().Initialized(loadData.Room[i]);
        }
    }
    public void BTNTest() {
        Debug.Log("Test3");
    }

    public void MakeRoomFromGenerator() {
        Debug.Log("Test");

        roomGenerator.GenerateRoom();
        roomListData = roomGenerator.GetRoomList();
        roomCount = roomListData.Count;
        RoomList = new GameObject[roomCount];

       int i = 0;
        foreach(RoomData rm in roomListData) {
            Instantiate(RoomSample).transform.parent = gameObject.transform;
            RoomList[i] = transform.GetChild(i).gameObject;
            RoomList[i].GetComponent<Room>().Initialized(rm);

            Debug.Log(i);
            i++;
        }

    }

    public void RoomReset() {
        foreach(Transform room in transform) {
            roomListData.Clear();
            Destroy(room.gameObject);
        }

    }

}
