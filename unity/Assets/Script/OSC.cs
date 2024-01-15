using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using UnityEngine;

using OscJack;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine.Serialization;

public class OSC : MonoBehaviour
{
    private OscServer server;

    public static Dictionary<int, Vector2> InstrumentPositions = new Dictionary<int, Vector2>();
    public static Dictionary<int, float> InstrumentRotations = new Dictionary<int, float>();
    public TMP_Text textMesh;
    public string text = "Searching ...";

    public List<int> sessionElementsList;
    
    private Dictionary<int, int> sessionIdToObjId = new Dictionary<int, int>();
    private List<int> previousSessionElementsList = new List<int>();
    private Queue<int> idsToDestroy = new Queue<int>();

    public WebSocketClient webSocketClient;



    void Start()
    {
        textMesh.text = "testing ...";
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
        while (idsToDestroy.Count > 0)
        {
            int objId = idsToDestroy.Dequeue();
            switch (objId)
            {
                case 0:
                    InstrumentPositions[0] = Vector2.zero;
                    GameController.DestroyPiano();
                    break;
                case 1:
                    InstrumentPositions[1] = Vector2.zero;
                    GameController.DestroyGuitare();
                    break;
                case 2:
                    InstrumentPositions[2] = Vector2.zero;
                    GameController.DestroyBattery();
                    break;
                case 3:
                    InstrumentPositions[3] = Vector2.zero;
                    GameController.DestroyViolon();
                    break;
                case 4:
                    InstrumentPositions[4] = Vector2.zero;
                    GameController.DestroyReponse();
                    break;
                case 5:
                    InstrumentPositions[5] = Vector2.zero;
                    GameController.DestroyMusique();
                    break;
                case 6:
                    InstrumentPositions[6] = Vector2.zero;
                    GameController.DestroyValidate();
                    break;
                default:
                    break;
            }
        }

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

            if (InstrumentPositions.TryGetValue(4, out Vector2 reponsePosition))
            {
                // Taille des zones de réponse (à ajuster selon votre setup)
                float objectWidth = 1.0f; // La largeur totale de l'objet de réponse
                float objectHeight = 1.0f; // La hauteur totale de l'objet de réponse
                float padding = 0.25f; // Espace supplémentaire autour de l'objet pour agrandir la zone cliquable

                float left = reponsePosition.x - objectWidth / 2 - padding;
                float right = reponsePosition.x + objectWidth / 2 + padding;
                float top = reponsePosition.y + objectHeight / 2 + padding;
                float bottom = reponsePosition.y - objectHeight / 2 - padding;


                // Vérifiez dans quelle zone se trouve le clic
                if (x >= left && x < reponsePosition.x && y >= reponsePosition.y && y < top)
                {
                    webSocketClient.OnAnswerButtonClicked("C"); // Haut gauche - Réponse C
                }
                else if (x >= reponsePosition.x && x < right && y >= reponsePosition.y && y < top)
                {
                    webSocketClient.OnAnswerButtonClicked("D"); // Haut droit - Réponse D
                }
                else if (x >= left && x < reponsePosition.x && y < reponsePosition.y && y >= bottom)
                {
                    webSocketClient.OnAnswerButtonClicked("A"); // Bas gauche - Réponse A
                }
                else if (x >= reponsePosition.x && x < right && y < reponsePosition.y && y >= bottom)
                {
                    webSocketClient.OnAnswerButtonClicked("B"); // Bas droit - Réponse B
                }
            }

        }

        else if (command == "alive")
        {
            Debug.Log(data.GetElementAsInt(1));
        }
    }

    // Debug.Log($"2Dcur - Session: {s}, Position: ({x}, {y}), Velocity: ({X}, {Y}), MotionAcceleration: {m}");

        
        

    void Process2DObj(string address, OscDataHandle data)
    {
        string command = data.GetElementAsString(0);

        if (command == "set")
        {
            text = data.GetElementAsString(2);
            string stringToInt = data.GetElementAsString(2);
            int i;
            try
            {
                i = int.Parse(stringToInt.Substring(0, 1));
            }
            catch
            {
                i = 0;
            }
            
            int s = (int)data.GetElementAsFloat(1);
            float x = data.GetElementAsFloat(3);
            float y = data.GetElementAsFloat(4);
            float a = data.GetElementAsFloat(5);
            float X = data.GetElementAsFloat(6);
            float Y = data.GetElementAsFloat(7);
            float A = data.GetElementAsFloat(8);
            float m = data.GetElementAsFloat(9);
            float r = data.GetElementAsFloat(10);

            // Debug.Log($"2Dobj - Session: {s}, ClassId: {i}, Position: ({x}, {y}), Angle: {a}, Velocity: ({X}, {Y}, {A}), MotionAcceleration: {m}, RotationAcceleration: {r}");
             // text = "2Dobj - Session: " + s + " ClassId: " + stringToInt + " Position: (" + x + "," + y + "), Angle: " +
             //                 a + " Velocity: (" + X + "," + Y + ", " + A + "), MotionAcceleration: " + m +
             //                 " , RotationAcceleration: " + r;
             //
            sessionIdToObjId[s] = i;
            InstrumentPositions[i] = new Vector2(x, y);
            InstrumentRotations[i] = a;
        }
        else if (command == "alive")
        {
            int number = data.GetElementCount();

            List<int> currentSessionElementsList = new List<int>();
            for (int i = 1; i < number; i++)
            {
                currentSessionElementsList.Add(data.GetElementAsInt(i));
            }

            foreach (int sessionId in previousSessionElementsList)
            {
                if (!currentSessionElementsList.Contains(sessionId))
                {
                    // Debug.Log($"Session ID retiré: {sessionId}");
                    if (sessionIdToObjId.TryGetValue(sessionId, out int objId))
                    {
                        // Debug.Log($"ID d'objet correspondant: {objId}");
                        idsToDestroy.Enqueue(objId);
                    }
                }
            }

            previousSessionElementsList = new List<int>(currentSessionElementsList);
        }
    }
    
    public static Vector2 GetInstrumentPosition(int instrumentId)
    {
        if (InstrumentPositions.TryGetValue(instrumentId, out Vector2 position))
        {
            return position;
        }
        return Vector2.zero;
    }
    
    public static float GetInstrumenRotation(int instrumentId)
    {
        if (InstrumentRotations.TryGetValue(instrumentId, out float rotation))
        {
            return rotation;
        }
        return 0;
    }
}
