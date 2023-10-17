using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Chat : MonoBehaviour
{
    // Start is called before the first frame update
    const int PORT = 9090;
    Client client;
    public static List<string> message = new List<string>();
    string outMessage = "";
    string name = "";
    public InputField NickName;
    public InputField smsInput;
    public GameObject Chat_View;
    public GameObject Chat_View_Oth;
    public int szMes;
    public int szMesNew;
    public float py = 0;

    void Start()
    {
        Application.runInBackground = true;
        szMes = message.ToArray().Length;
        client = new Client(PORT, "127.0.0.1");
        client.Work();
    }

    // Update is called once per frame
    void Update()
    {
        name = NickName.text;
        ///GUILayout.BeginScrollView(new Vector2(0, 0), GUILayout.Width(500), GUILayout.Height(300));
        szMesNew = message.ToArray().Length;
        if (szMesNew > szMes)
        {
            for (int i = szMes; i < szMesNew; i++)
            {
                ///GUILayout.Label(mes);
                if (message[i].Split(':')[1] == name)
                {
                    ///Chat_View.GetComponent<Text>().alignment = TextAnchor.LowerRight;
                    Chat_View.GetComponent<Text>().text = Chat_View.GetComponent<Text>().text + "\n" + message[i];
                    Chat_View_Oth.GetComponent<Text>().text = Chat_View_Oth.GetComponent<Text>().text + "\n";

                    Chat_View.GetComponent<RectTransform>().sizeDelta = new Vector2(Chat_View.GetComponent<RectTransform>().sizeDelta.x, (Chat_View.GetComponent<RectTransform>().sizeDelta.y + 50f));
                    py = (Chat_View.GetComponent<RectTransform>().localPosition.y);
                    Chat_View.GetComponent<RectTransform>().localPosition = new Vector3(Chat_View.GetComponent<RectTransform>().localPosition.x, py + 50, Chat_View.GetComponent<RectTransform>().localPosition.z);

                    Chat_View_Oth.GetComponent<RectTransform>().sizeDelta = new Vector2(Chat_View_Oth.GetComponent<RectTransform>().sizeDelta.x, (Chat_View_Oth.GetComponent<RectTransform>().sizeDelta.y + 50f));
                    py = (Chat_View_Oth.GetComponent<RectTransform>().localPosition.y);
                    Chat_View_Oth.GetComponent<RectTransform>().localPosition = new Vector3(Chat_View_Oth.GetComponent<RectTransform>().localPosition.x, py + 50, Chat_View_Oth.GetComponent<RectTransform>().localPosition.z);
                
                }
                else
                {
                    ///Chat_View.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
                    int fnIn = 0;
                    string SenName = "";
                    string SenMsg = "";
                    foreach (string s in message[i].Split(':'))
                    {
                        if (fnIn == 3)
                        {
                            SenMsg = s;
                        }
                        if (fnIn == 4)
                        {
                            SenName = s;
                        }
                        fnIn = fnIn + 1;
                    }

                    string allMsg = "";
                    allMsg = SenName + ":" + SenMsg;

                    Chat_View_Oth.GetComponent<Text>().text = Chat_View_Oth.GetComponent<Text>().text + "\n" + allMsg;
                    Chat_View.GetComponent<Text>().text = Chat_View.GetComponent<Text>().text + "\n";

                    Chat_View.GetComponent<RectTransform>().sizeDelta = new Vector2(Chat_View.GetComponent<RectTransform>().sizeDelta.x, (Chat_View.GetComponent<RectTransform>().sizeDelta.y + 50f));
                    py = (Chat_View.GetComponent<RectTransform>().localPosition.y);
                    Chat_View.GetComponent<RectTransform>().localPosition = new Vector3(Chat_View.GetComponent<RectTransform>().localPosition.x, py + 50, Chat_View.GetComponent<RectTransform>().localPosition.z);

                    Chat_View_Oth.GetComponent<RectTransform>().sizeDelta = new Vector2(Chat_View_Oth.GetComponent<RectTransform>().sizeDelta.x, (Chat_View_Oth.GetComponent<RectTransform>().sizeDelta.y + 50f));
                    py = (Chat_View_Oth.GetComponent<RectTransform>().localPosition.y);
                    Chat_View_Oth.GetComponent<RectTransform>().localPosition = new Vector3(Chat_View_Oth.GetComponent<RectTransform>().localPosition.x, py + 50, Chat_View_Oth.GetComponent<RectTransform>().localPosition.z);
                
                }

                /*Chat_View.GetComponent<Text>().text = Chat_View.GetComponent<Text>().text + "\n" + message[i];
                Chat_View_Oth.GetComponent<Text>().text = Chat_View_Oth.GetComponent<Text>().text + "\n";

                Chat_View.GetComponent<RectTransform>().sizeDelta = new Vector2(Chat_View.GetComponent<RectTransform>().sizeDelta.x, (Chat_View.GetComponent<RectTransform>().sizeDelta.y + 50f));
                py = (Chat_View.GetComponent<RectTransform>().localPosition.y);
                Chat_View.GetComponent<RectTransform>().localPosition = new Vector3(Chat_View.GetComponent<RectTransform>().localPosition.x, py + 50, Chat_View.GetComponent<RectTransform>().localPosition.z);*/

                if (i == (szMesNew - 1))
                {
                    szMes = szMesNew;
                }
            }
        }
        ///GUILayout.EndScrollView();
        ///name = GUILayout.TextField(name);
        ///outMessage = GUILayout.TextArea(outMessage, GUILayout.Width(500), GUILayout.Height(100));
        /*outMessage = smsInput.text;
        if (GUILayout.Button("Send"))
        {
            client.SendMessage(name + " : " + outMessage);
        }*/
    }
    public void SendBTN()
    {
        name = NickName.text;
        outMessage = smsInput.text + ":" + name;
        client.SendMessage(outMessage);
        smsInput.text = "";
    }
}
