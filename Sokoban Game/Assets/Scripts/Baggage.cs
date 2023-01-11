using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baggage : MonoBehaviour {
    CommandMng CM;
    RaycastHit2D hit;
    int layerMask;
    public float x;
    public float y;
    private void Start() {
        CM = new CommandMng();
        layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Baggage"));
    }
    void Update() {
        if (SystemMng.ins.state == SystemMng.STATE.PAUSE)
            return;

        if (StageMng.ins.Step > CM.GetCount()) // 플레이어는 이동하였지만 짐은 제자리일 경우에도 CommandMng에 위치 저장
            Skip();

        UndoPos();
        ResetPos();
    }
    public void UndoPos() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Vector2 newPos = CM.Undo();
            if (newPos.x == 0 && newPos.y == 0)
                return;
            this.transform.position = newPos;
        }
    }
    public void ResetPos() {
        if (Input.GetKeyDown(KeyCode.C) || SystemMng.ins.state == SystemMng.STATE.RESET) {
            Vector2 newPos = CM.Reset();
            if (newPos.x == 0 && newPos.y == 0)
                return;
            this.transform.position = newPos;
        }

    }
    public bool MoveSelf(string dir) {
        switch (dir) {
            case "left":
                hit = Physics2D.Raycast(transform.position - transform.right, -transform.right, 0.2f, layerMask);
                if (hit.collider == null) {
                    CM.Execute(new Vector2(this.transform.position.x - 1, this.transform.position.y), this.transform.position);
                    this.transform.Translate(new Vector3(-1, 0, 0));
                    return true;
                } else 
                    return false;
                
            case "right":
                hit = Physics2D.Raycast(transform.position + transform.right, transform.right, 0.2f, layerMask);
                if (hit.collider == null) {
                    CM.Execute(new Vector2(this.transform.position.x + 1, this.transform.position.y), this.transform.position);
                    this.transform.Translate(new Vector3(1, 0, 0));
                    return true;
                } else
                    return false;
            case "up":
                hit = Physics2D.Raycast(transform.position + transform.up, transform.up, 0.2f, layerMask);
                if (hit.collider == null) {
                    CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y + 1), this.transform.position);
                    this.transform.Translate(new Vector3(0, 1, 0));
                    return true;
                } else
                    return false;
            case "down":
                hit = Physics2D.Raycast(transform.position - transform.up, -transform.up, 0.2f, layerMask);
                if (hit.collider == null) {
                    CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y - 1), this.transform.position);
                    this.transform.Translate(new Vector3(0, -1, 0));
                    return true;
                } else
                    return false;
        }
        return false;
    }
    public void Skip() {
        CM.Execute(this.transform.position, this.transform.position);
    }
}
