using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraMove : Singleton<CameraMove> {
    static AudioSource audioSource;
    public static AudioClip audioMove;
    public static AudioClip audioUndo;
    Transform m_Transform;
    [SerializeField] GameObject START;
    [SerializeField] GameObject StartUi;
    [SerializeField] GameObject StageListUi;
    [SerializeField] GameObject stage;
    [SerializeField] GameObject IngameUi;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioMove = Resources.Load<AudioClip>("DM-CGS-01"); //이동 소리
        audioUndo = Resources.Load<AudioClip>("DM-CGS-07"); //실행취소 소리

        Screen.SetResolution(1920, 1080, false);            //창모드 1920x1080
        m_Transform = GetComponent<Transform>();
        SystemMng.ins.state = SystemMng.STATE.PAUSE;
        m_Transform.DOMove(new Vector3(6, -3, -10), 3);
        StartCoroutine(StartUI());
    }

    public void GameStart() {
        SystemMng.ins.state = SystemMng.STATE.PAUSE;
        m_Transform.DOMove(new Vector3(6, 13, -10), 2);
        StartCoroutine(Gamestart());
    }
    public void ShowStageList() {
        m_Transform.DOMove(new Vector3(6, 13, -10), 3);
        SystemMng.ins.state = SystemMng.STATE.RESET;
        StartCoroutine(StageListUI());
    }
    public void SelectStage() {
        stage.SetActive(true);
        StageListUi.SetActive(false);
        m_Transform.DOMove(new Vector3(6, -3, -10), 3);
        StartCoroutine(IngameUI());
    }
    public void PlayMoveSound() {
        audioSource.PlayOneShot(audioMove);
    }
    public void PlayUndoSound() {
        audioSource.PlayOneShot(audioUndo);
    }
    IEnumerator Gamestart() {
        StartUi.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        START.SetActive(false);
        stage.SetActive(true);
        m_Transform.DOMove(new Vector3(6, -3, -10), 3);
        yield return new WaitForSeconds(3.0f);
        IngameUi.SetActive(true);
        SystemMng.ins.state = SystemMng.STATE.PLAY;
        StageMng.ins.Step = 0;
        StageMng.ins.goalCnt = 0;
    }
    IEnumerator StartUI() {
        yield return new WaitForSeconds(3.0f);
        StartUi.SetActive(true);
        SystemMng.ins.state = SystemMng.STATE.PLAY;
    }
    IEnumerator IngameUI() {
        yield return new WaitForSeconds(3.0f);
        IngameUi.SetActive(true);
        SystemMng.ins.state = SystemMng.STATE.PLAY;
    }
    IEnumerator StageListUI() {
        IngameUi.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SystemMng.ins.state = SystemMng.STATE.PAUSE;
        stage.SetActive(false);
        StageListUi.SetActive(true);
    }
}
