using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngameUi : MonoBehaviour {
    [SerializeField] Button StageListBtn;
    public void ShowStageList() {
        SystemMng.ins.state = SystemMng.STATE.PAUSE;
        CameraMove.ins.ShowStageList();
    }
    public void ExitBtn() {
        Application.Quit();
    }
    private void Update() {
        if (SystemMng.ins.state == SystemMng.STATE.PAUSE) {
            StageListBtn.enabled = false;
        }else
            StageListBtn.enabled = true;
    }

}
