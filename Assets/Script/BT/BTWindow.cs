using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BehaviorTreeEditor : EditorWindow
{
    private List<Node> nodes = new List<Node>(); // 노드를 저장할 리스트
    private Vector2 offset;
    private Vector2 drag;

    // 창 열기
    [MenuItem("Window/Behavior Tree Editor")]
    private static void OpenWindow()
    {
        BehaviorTreeEditor window = GetWindow<BehaviorTreeEditor>();
        window.titleContent = new GUIContent("Behavior Tree Editor");
    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray); // 작은 그리드
        DrawGrid(100, 0.4f, Color.gray); // 큰 그리드

        DrawNodes();
        DrawConnections();

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    // 그리드 그리기
    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    // 노드 그리기
    private void DrawNodes()
    {
        foreach (var node in nodes)
        {
            node.Draw();
        }
    }

    // 노드 간 연결선 그리기
    private void DrawConnections()
    {
        foreach (var node in nodes)
        {
            foreach (var connectedNode in node.Connections)
            {
                Handles.DrawBezier(
                    node.Rect.center,
                    connectedNode.Rect.center,
                    node.Rect.center + Vector2.left * 50f,
                    connectedNode.Rect.center - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f);
            }
        }
    }

    // 노드 이벤트 처리
    private void ProcessNodeEvents(Event e)
    {
        foreach (var node in nodes)
        {
            bool guiChanged = node.ProcessEvents(e);

            if (guiChanged)
            {
                GUI.changed = true;
            }
        }
    }

    // 이벤트 처리 (드래그, 노드 추가 등)
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    // 드래그 처리
    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Drag(delta);
        }

        GUI.changed = true;
    }

    // 마우스 우클릭 시 노드 추가 메뉴
    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    // 노드 추가
    private void OnClickAddNode(Vector2 mousePosition)
    {
        nodes.Add(new Node(mousePosition));
    }

    // 노드 클래스
    public class Node
    {
        public Rect Rect;
        public string Title;
        public bool IsDragged;
        public List<Node> Connections;

        public Node(Vector2 position)
        {
            Rect = new Rect(position.x, position.y, 200, 50);
            Title = "New Node";
            Connections = new List<Node>();
        }

        public void Drag(Vector2 delta)
        {
            Rect.position += delta;
        }

        public void Draw()
        {
            GUI.Box(Rect, Title);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (Rect.Contains(e.mousePosition))
                        {
                            IsDragged = true;
                            GUI.changed = true;
                        }
                    }
                    break;

                case EventType.MouseUp:
                    IsDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && IsDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
