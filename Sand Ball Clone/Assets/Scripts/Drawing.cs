using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Drawing : MonoBehaviour {
    [SerializeField] private bool inEditor;
    private Camera cam;
    private bool TouchStart,Touching,TouchEnd;
    private void Awake(){
        cam = GetComponent<Camera>();

    }
    private void Start(){
#if UNITY_EDITOR
        inEditor = true;
#else
        inEditor = false;
#endif
    }
    private void OnPc(){
        if(!EventSystem.current.IsPointerOverGameObject()){
            TouchStart = Input.GetMouseButtonDown(0);
            Touching = Input.GetMouseButton(0);
            TouchEnd = Input.GetMouseButtonUp(0);
        }
    }
    private void OnMobile(){
        Touch touch = Input.GetTouch(0);
        int fingerId = touch.fingerId;
        if(!EventSystem.current.IsPointerOverGameObject(fingerId)){
            TouchStart = touch.phase == TouchPhase.Began ? true: false;
            Touching = touch.phase == TouchPhase.Moved ? true: false;
            TouchEnd = touch.phase == TouchPhase.Ended ? true: false;
        }
    }
    private void Update(){
        if(inEditor){
            OnPc();
        }else{
            OnMobile();
        }
        if(Touching){
            DeformMesh();
        }
    }
    private void DeformMesh(){
        Vector3 point = Input.mousePosition;
        if(!inEditor){
            point = Input.GetTouch(0).position;
        }
        Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit)){
            DeformingPlane deformingPlane = hit.transform.GetComponent<DeformingPlane>();
            if(deformingPlane != null){
                deformingPlane.Deform(hit.point);
            }
        }
    }

    public void Restart(){
        SceneManager.LoadSceneAsync(0);
    }
}
