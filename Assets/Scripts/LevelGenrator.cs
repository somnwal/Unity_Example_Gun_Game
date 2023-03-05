using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenrator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;

    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left };

    public Direction selectedDirection;

    public LayerMask whatIsRoom;

    private GameObject endRoom;

    public float xOffset = 18f, yOffset = 10f;

    private List<GameObject> layoutRoomObejects = new List<GameObject>();

    public RoomPrefabs rooms;

    private List<GameObject> generatedoutlines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction) Random.Range(0, 4);
        moveGenerationPoint();

        for(int i=0; i < distanceToEnd; i++) {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            
            // 리스트에 방 추가
            layoutRoomObejects.Add(newRoom);

            if(i+1 == distanceToEnd) {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;

                layoutRoomObejects.RemoveAt(layoutRoomObejects.Count - 1);

                endRoom = newRoom;
            }

            selectedDirection = (Direction) Random.Range(0, 4);
            moveGenerationPoint();

            while(Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom)) {
                moveGenerationPoint();
            }
        }

        createRoomOutline(Vector3.zero);

        foreach(GameObject room in layoutRoomObejects) {
            createRoomOutline(room.transform.position);
        }

        createRoomOutline(endRoom.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void moveGenerationPoint() {
        switch(selectedDirection) {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);                    
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0, 0f);                    
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);                    
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0, 0f);                    
                break;
        }
    }

    public void createRoomOutline(Vector3 roomPosition) {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);

        int directionCount = 0;

        if(roomAbove)
            directionCount++;
        if(roomBelow)
            directionCount++;
        if(roomLeft)
        directionCount++;
        if(roomRight)
        directionCount++;

        switch(directionCount) {
            case 0:
                Debug.LogError("방이 없음");
                break;
            case 1:
                if(roomAbove)
                    generatedoutlines.Add(Instantiate(rooms.roomU, roomPosition, transform.rotation));
                if(roomBelow)
                    generatedoutlines.Add(Instantiate(rooms.roomD, roomPosition, transform.rotation));
                if(roomLeft)
                    generatedoutlines.Add(Instantiate(rooms.roomL, roomPosition, transform.rotation));
                if(roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomR, roomPosition, transform.rotation));
                
                break;
            case 2:
                if(roomAbove && roomBelow)
                    generatedoutlines.Add(Instantiate(rooms.roomUD, roomPosition, transform.rotation));
                if(roomAbove && roomLeft)
                    generatedoutlines.Add(Instantiate(rooms.roomUL, roomPosition, transform.rotation));
                if(roomAbove && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomUR, roomPosition, transform.rotation));
                if(roomBelow && roomLeft)
                    generatedoutlines.Add(Instantiate(rooms.roomDL, roomPosition, transform.rotation));
                if(roomBelow && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomDR, roomPosition, transform.rotation));

                if(roomLeft && roomRight)
                generatedoutlines.Add(Instantiate(rooms.roomLR, roomPosition, transform.rotation));
                
                break;
            case 3:
                if(roomAbove && roomLeft && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomULR, roomPosition, transform.rotation));
                if(roomAbove && roomBelow && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomUDR, roomPosition, transform.rotation));
                if(roomBelow && roomLeft && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomDLR, roomPosition, transform.rotation));
                if(roomAbove && roomBelow && roomLeft)
                    generatedoutlines.Add(Instantiate(rooms.roomUDL, roomPosition, transform.rotation));
                
                break;
            case 4:
                if(roomAbove && roomBelow && roomLeft && roomRight)
                    generatedoutlines.Add(Instantiate(rooms.roomUDLR, roomPosition, transform.rotation));
                
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs {
    public GameObject roomU, roomD, roomL, roomR,
                    roomLR, roomUD, roomUR, roomDR,
                    roomDL, roomUL, roomUDR, roomDLR,
                    roomUDL, roomULR, roomUDLR;
}