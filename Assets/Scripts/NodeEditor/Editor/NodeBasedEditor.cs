using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AmazingNodeEditor
{
    public class NodeBasedEditor : EditorWindow
    {
        private List<Node> nodes;
        private List<Connection> connections;

        protected Vector2 defaultNodeDimensions = new Vector2(400, 200);

        protected void OnClickRemoveNode(Node node)
        {
           if(connections != null)
           {
                var connectionsToRemove = new List<Connection>();

                for (int i = 0; i < connections.Count; ++i)
                {
                    if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                    {
                        connectionsToRemove.Add(connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; ++i)
                {
                    connections.Remove(connectionsToRemove[i]);
                }

                connectionsToRemove = null;
            }

           nodes.Remove(node);
        }

        protected void OnClickRemoveConnection(Connection connection)
        {
            connections.Remove(connection);
        }

        protected string nodesPath = $"{Application.streamingAssetsPath}/nodes.xml";
        protected string connectionsPath = $"{Application.streamingAssetsPath}/connections.xml";

        protected void Save()
        {
            XMLSaver.Serialize(nodes, nodesPath);
            XMLSaver.Serialize(connections, connectionsPath);
        }

        protected void Load()
        {
            var nodesDeserialized = XMLSaver.Deserialize<List<Node>>(nodesPath);
            var connectionsDeserialized = XMLSaver.Deserialize<List<Connection>>(connectionsPath);

            nodes = new List<Node>();
            connections = new List<Connection>();

            foreach (var node in nodesDeserialized)
            {
                var dim = new Vector2(node.rect.width, node.rect.height);

                nodes.Add(new Node());

                //nodes.Add(new Node(
                //node.rect.position,
                //dim,
                //defaultNodeStyle,
                //OnClickInPoint,
                //OnClickOutPoint,
                //OnClickRemoveNode,
                //node.inPoint.id,
                //node.outPoint.id
                //));
            }

            foreach(var connection in connectionsDeserialized)
            {
                var inPoint = nodes.First(n => n.inPoint.id == connection.inPoint.id).inPoint;
                var outPoint = nodes.First(n => n.outPoint.id == connection.outPoint.id).outPoint;
                connections.Add(new Connection(inPoint, outPoint, OnClickRemoveConnection));
            }
        }
    }
}
