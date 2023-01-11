using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

public class StageList : MonoBehaviour {

    [SerializeField] Button[] Buttons;
    [SerializeField] TMP_Text[] ClearTexts;
    private void OnEnable() {
        print("onenable»£√‚");
        ListCheck();
    }
    void ListCheck() {
        Buttons[0].enabled = true;
        for (int i = 1; i < StageMng.ins.stage_List.Count-1; i++) {
            if (StageMng.ins.stage_List[i].IsClear) {
                Buttons[i].enabled = true;
                ClearTexts[i - 1].text = "Clear\n"+ StageMng.ins.stage_List[i].step+"step";
            } else {
                Buttons[i].enabled = false;
            }
        }

    }
    public void btnClick() {
        switch (EventSystem.current.currentSelectedGameObject.name) {
            case "1":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(1);
                break;
            case "2":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(2);
                break;
            case "3":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(3);
                break;
            case "4":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(4);
                break;
            case "5":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(5);
                break;
            case "6":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(6);
                break;
            case "7":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(7);
                break;
            case "8":
                CameraMove.ins.SelectStage();
                StageMng.ins.SelectStage(8);
                break;
            default:
                break;
        }
    }
    public void btnReset() {
        for(int i=0; i< ClearTexts.Length; i++) {
            ClearTexts[i].text = "";
        }
        StageMng.ins.InitData();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}

