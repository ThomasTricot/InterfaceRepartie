using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OscJack;
using TMPro;

public class OSC : MonoBehaviour
{
    private OscServer server;

    public static Dictionary<int, Vector2> InstrumentPositions = new Dictionary<int, Vector2>();
    public static float[] staticValues = new float[]{ 1, 1, 1, 1, 1,1 };
    public TMP_Text textMesh;
    public string text = "Searching ...";
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
        textMesh.text = text;
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
            text = data.GetElementAsString(2);

            
            float s = data.GetElementAsFloat(1);

            int i = (int)data.GetElementAsFloat(2);
            
            float x = data.GetElementAsFloat(3);
            float y = data.GetElementAsFloat(4);
            float a = data.GetElementAsFloat(5);
            float X = data.GetElementAsFloat(6);
            float Y = data.GetElementAsFloat(7);
            float A = data.GetElementAsFloat(8);
            float m = data.GetElementAsFloat(9);
            float r = data.GetElementAsFloat(10);

            // Debug.Log($"2Dobj - Session: {s}, ClassId: {i}, Position: ({x}, {y}), Angle: {a}, Velocity: ({X}, {Y}, {A}), MotionAcceleration: {m}, RotationAcceleration: {r}");
            // text = "2Dobj - Session: " + s + " ClassId: " + i + " Position: (" + x + "," + y + "), Angle: " +
            //                 a + " Velocity: (" + X + "," + Y + ", " + A + "), MotionAcceleration: " + m +
            //                 " , RotationAcceleration: " + r;
            
            staticValues[0] = data.GetElementAsFloat(6); // X
            staticValues[1] = data.GetElementAsFloat(7); // Y
            staticValues[2] = data.GetElementAsFloat(8); // A
            staticValues[3] = data.GetElementAsFloat(9); // m
            staticValues[4] = data.GetElementAsFloat(10); // r
            staticValues[5] = data.GetElementAsFloat(2); // id

            InstrumentPositions[i] = new Vector2(x, y);
            //InstrumentSpawner.VerifyIfClicked();
            
        }
        else if (command == "fseq")
        {
            int fseq = data.GetElementAsInt(1);
            // Debug.Log("Frame Sequence: " + fseq);
        }
    }
    
    public static Vector2 GetInstrumentPosition(int instrumentId)
    {
        if (InstrumentPositions.TryGetValue(instrumentId, out Vector2 position))
        {
            return position;
        }
        return Vector2.zero; // Retourner une valeur par défaut si l'ID n'est pas trouvé
    }
}
