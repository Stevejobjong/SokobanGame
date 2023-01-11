using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    CommandMng CM;
    RaycastHit2D hit;
    int layerMask;
    public float x;
    public float y;
    void Start() {
        layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Baggage"));//자기 자신 제외시키기
        CM = new CommandMng();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (SystemMng.ins.state == SystemMng.STATE.PAUSE)       //state가 PUASE일때 return;
            return;
        Move();
        UndoPos();
        ResetPos();
    }
    public void UndoPos() {                                     //Z키 입력시 실행취소
        if (Input.GetKeyDown(KeyCode.Z)) {
            Vector2 newPos = CM.Undo();
            if (newPos.x == 0 && newPos.y == 0)
                return;
            StageMng.ins.Step -= 1;
            CameraMove.ins.PlayUndoSound();
            this.transform.position = newPos;
        }

    }
    public void ResetPos() {                                    //C키 입력시 위치 초기화
        if (Input.GetKeyDown(KeyCode.C)||SystemMng.ins.state==SystemMng.STATE.RESET) {
            Vector2 newPos = CM.Reset();
            if (newPos.x == 0 && newPos.y == 0)
                return;

            StageMng.ins.Step = 0;
            this.transform.position = newPos;
        }

    }
    void Move() {
        //왼쪽 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            spriteRenderer.sprite = sprites[0];
            hit = Physics2D.Raycast(transform.position, -transform.right, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {                       //벽에 막힘 return
                    print("왼쪽 벽에 막힘");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {                    //짐 발견
                    print("짐 발견");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("left"))                                      //짐이 이동불가면 return
                        return;
                }
            }
            StageMng.ins.StepUp();                                                      //Step수 1증가
            CM.Execute(new Vector2(this.transform.position.x - 1, this.transform.position.y), this.transform.position);//CommnadMng에 이동 기록
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(-1, 0, 0));
        }
        //오른쪽 이동
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            spriteRenderer.sprite = sprites[1];
            hit = Physics2D.Raycast(transform.position, transform.right, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("오른쪽 벽에 막힘");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("짐 발견");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("right"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x + 1, this.transform.position.y), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(1, 0, 0));
        }
        //위쪽 이동
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            spriteRenderer.sprite = sprites[2];
            hit = Physics2D.Raycast(transform.position, transform.up, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("위쪽 벽에 막힘");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("짐 발견");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("up"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y + 1), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(0, 1, 0));
        }
        //아래쪽 이동
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            spriteRenderer.sprite = sprites[3];
            hit = Physics2D.Raycast(transform.position, -transform.up, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("아래쪽 벽에 막힘");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("짐 발견");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("down"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y - 1), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(0, -1, 0));
        }
    }

}
