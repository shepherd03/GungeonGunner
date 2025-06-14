using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeGraph", menuName = "Scriptable Objects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    [HideInInspector]public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector]public List<RoomNodeSO> roomNodeList = new List<RoomNodeSO>();
    [HideInInspector]public Dictionary<string,RoomNodeSO> roomNodeDictionary = new Dictionary<string, RoomNodeSO>();

    private void Awake()
    {
        LoadRoomDictionary();
    }

    private void LoadRoomDictionary()
    {
        roomNodeDictionary.Clear();

        foreach (var node in roomNodeList)
        {
            roomNodeDictionary[node.id] = node; 
        }
    }

    public RoomNodeSO GetRoomNode(RoomNodeTypeSO roomNodeType)
    {
        foreach (var node in roomNodeList)
        {
            if (node.roomNodeType == roomNodeType)
            {
                return node;
            }
        }

        return null;
    }

    public IEnumerable<RoomNodeSO> GetChildRoomNodes(RoomNodeSO parentRoomNode)
    {
        foreach (var childNodeID in parentRoomNode.childRoomNodeIDList)
        {
            yield return GetRoomNode(childNodeID);
        }
    }

    public RoomNodeSO GetRoomNode(string id)
    {
        if (roomNodeDictionary.TryGetValue(id, out RoomNodeSO roomNode))
        {
            return roomNode;
        }
        return null;
    }

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom;
    [HideInInspector] public Vector2 linePosition;

    public void OnValidate()
    {
        LoadRoomDictionary();
    }

    public void SetNodeToDrawConnectionLineForm(RoomNodeSO node, Vector2 position)
    {
        roomNodeToDrawLineFrom = node;
        linePosition = position;
    }

#endif

    #endregion
}
