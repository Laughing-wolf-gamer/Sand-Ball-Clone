using UnityEngine;


public class DeformingPlane : MonoBehaviour {
    private MeshFilter meshFilter;
    private Mesh planeMesh;
    private Vector3[] vertsArray;
    private MeshCollider meshCollider;
    [SerializeField] private float radius;
    [SerializeField] private float power;
    private void Start(){
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        planeMesh = meshFilter.mesh;
        vertsArray = planeMesh.vertices;
    }
    public void Deform(Vector3 pointToDeform){
        pointToDeform = transform.InverseTransformPoint(pointToDeform);
        if(vertsArray.Length > 0){
            for(int i = 0; i < vertsArray.Length; i++){
                float dist = (vertsArray[i] - pointToDeform).sqrMagnitude;
                if(dist < radius){
                    vertsArray[i] -= Vector3.up * power;
                }
            }
            planeMesh.vertices = vertsArray;
            meshCollider.sharedMesh = planeMesh;
        }

    }

}
