using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;

public class StageMng : Singleton<StageMng> {
    public TMP_Text StageTxt;
    public TMP_Text StepTxt;
    public int Step = 0;
    public int CurStage = 1;
    public int goalCnt = 0;
    [SerializeField] GameObject StageClearUI;
    [SerializeField] public List<Transform> stageList = new List<Transform>();
    [SerializeField] public List<Stage> stage_List = new List<Stage>();
    public Stage[] stageArray;
    public string path;
    private void Start() {
        SetCurStage();
        path = Application.persistentDataPath + "/StageData.json";
        if (File.Exists(path))
            LoadData();
        else
            InitData();

    }
    // Update is called once per frame
    void Update() {
        StageTxt.text = "Stage " + CurStage;
        StepTxt.text = "Step : " + Step;

        if (CurStage == stageList.Count)
            return;
        if (stage_List[CurStage].goalNum == goalCnt) {
            goalCnt = 0;
            print("성공");
            SystemMng.ins.state = SystemMng.STATE.PAUSE;
            int s = stage_List[CurStage].step;
            stage_List[CurStage].step = Step < s ? Step : s;
            stage_List[CurStage].IsClear = true;
            SaveData();
            StageClearUI.SetActive(true);
        }
    }
    public void SetCurStage() {
        for (int i = 1; i < stageList.Count; i++) {
            if (stageList[i].gameObject.activeSelf)
                CurStage = i;
        }
    }
    public void SelectStage(int n) {
        stageList[n].gameObject.SetActive(true);
        for (int i = 1; i < stageList.Count; i++) {
            if (i != n)
                stageList[i].gameObject.SetActive(false);
        }

        goalCnt = 0;
        Step = 0;
        SetCurStage();
    }
    public void StepUp() {
        Step++;
    }
    public void StepDown() {
        Step--;
    }
    public void CntUp() {
        goalCnt++;
    }
    public void CntDown() {
        goalCnt--;
    }
    public void NextStageBtn() {
        if (CurStage + 1 >= stageList.Count) {
            print("모든 스테이지 클리어");
            return;
        }
        SystemMng.ins.state = SystemMng.STATE.RESET;
        StartCoroutine(NextStage());
    }
    public void StageListBtn() {
        SystemMng.ins.state = SystemMng.STATE.PAUSE;
        StageClearUI.SetActive(false);
        CameraMove.ins.ShowStageList();
    }
    IEnumerator NextStage() {
        yield return new WaitForSeconds(0.1f);
        stageList[CurStage].gameObject.SetActive(false);
        CurStage += 1;
        stageList[CurStage].gameObject.SetActive(true);
        StageClearUI.SetActive(false);
        goalCnt = 0;
        Step = 0;
        SystemMng.ins.state = SystemMng.STATE.PLAY;

    }
    IEnumerator setCurStage() {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < stageList.Count; i++) {
            if (stageList[i].gameObject.activeSelf)
                CurStage = i + 1;
        }
    }
    public void InitData() {
        stage_List.Clear();
        Array.Clear(stageArray, 0, stageArray.Length);
        Stage defualtStage = new Stage(-1, -1);
        defualtStage.IsClear = true;
        Stage stage1 = new Stage(1, 4);
        Stage stage2 = new Stage(2, 3);
        Stage stage3 = new Stage(3, 4);
        Stage stage4 = new Stage(4, 5);
        Stage stage5 = new Stage(5, 3);
        Stage stage6 = new Stage(6, 5);
        Stage stage7 = new Stage(7, 5);
        Stage stage8 = new Stage(8, 6);
        stage_List.Add(defualtStage);
        stage_List.Add(stage1);
        stage_List.Add(stage2);
        stage_List.Add(stage3);
        stage_List.Add(stage4);
        stage_List.Add(stage5);
        stage_List.Add(stage6);
        stage_List.Add(stage7);
        stage_List.Add(stage8);
        stageArray = stage_List.ToArray();
        var result = JsonConvert.SerializeObject(stageArray);
        print(result);
        File.WriteAllText(path, result);
    }
    public void LoadData() {
        string JsonFile;
        if (File.Exists(path)) {
            JsonFile = File.ReadAllText(path);
            print(JsonFile);
            stageArray = JsonConvert.DeserializeObject<Stage[]>(JsonFile);
            stage_List = stageArray.ToList();
        }
    }

    public void SaveData() {
        stageArray = stage_List.ToArray();
        var result = JsonConvert.SerializeObject(stageArray);
        print(result);
        File.WriteAllText(path, result);
    }

    [System.Serializable]
    public class Stage {
        public int stageNum;
        public int goalNum;
        public int step = 999999999;
        public bool IsClear = false;
        public Stage(int snum, int gnum) {
            this.stageNum = snum;
            this.goalNum = gnum;
        }
    }
}