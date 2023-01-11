using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMng : Singleton<SystemMng>
{

    public Scene scene;
    public enum STATE { START, PLAY, PAUSE , RESET}
    public STATE state = STATE.PLAY;
}
