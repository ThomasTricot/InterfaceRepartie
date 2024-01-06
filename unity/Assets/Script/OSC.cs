using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OscJack;

public class OSC : MonoBehaviour
{
    private OscServer server;
    
    public static float StaticX { get; private set; }
    public static float StaticY { get; private set; }

    void Start()
    {
        
        Debug.Log("OSC INITIALISED");

        server = new OscServer(3333);

        server.MessageDispatcher.AddCallback("/tuio/2Dcur", (string address, OscDataHandle data) => {
            Process2DCur(address, data);
        });

        server.MessageDispatcher.AddCallback("/tuio/2Dobj", (string address, OscDataHandle data) => {
            Process2DObj(address, data);
        });
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (server != null)
        {
            server.Dispose();
        }
    }

    void Process2DCur(string address, OscDataHandle data)
    {
        string command = data.GetElementAsString(0);
        if (command == "set")
        {
            float s = data.GetElementAsFloat(1);
            float x = data.GetElementAsFloat(2);
            float y = data.GetElementAsFloat(3);
            float X = data.GetElementAsFloat(4);
            float Y = data.GetElementAsFloat(5);
            float m = data.GetElementAsFloat(6);

            // Debug.Log($"2Dcur - Session: {s}, Position: ({x}, {y}), Velocity: ({X}, {Y}), MotionAcceleration: {m}");
            
        }
        else if (command == "fseq")
        {
            int fseq = data.GetElementAsInt(1);
            // Debug.Log("Frame Sequence: " + fseq);
        }
    }

    void Process2DObj(string address, OscDataHandle data)
    {
        string command = data.GetElementAsString(0);
        if (command == "set")
        {
            float s = data.GetElementAsFloat(1);
            float i = data.GetElementAsFloat(2);
            float x = data.GetElementAsFloat(3);
            float y = data.GetElementAsFloat(4);
            float a = data.GetElementAsFloat(5);
            float X = data.GetElementAsFloat(6);
            float Y = data.GetElementAsFloat(7);
            float A = data.GetElementAsFloat(8);
            float m = data.GetElementAsFloat(9);
            float r = data.GetElementAsFloat(10);

            // Debug.Log($"2Dobj - Session: {s}, ClassId: {i}, Position: ({x}, {y}), Angle: {a}, Velocity: ({X}, {Y}, {A}), MotionAcceleration: {m}, RotationAcceleration: {r}");
            StaticX = x;
            StaticY = y;
            // Debug.Log(StaticX + "  " + StaticY);
        }
        else if (command == "fseq")
        {
            int fseq = data.GetElementAsInt(1);
            // Debug.Log("Frame Sequence: " + fseq);
        }
    }
}
