using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] TargetAim targetAim;
    MeshRenderer mesh;
    float range;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        range = targetAim.range+10;
        Vector3 sphereRange = new Vector3(range, range, range);
        GetComponent<Transform>().localScale = sphereRange;
    }
    private void OnMouseEnter()
    {
        mesh.enabled = true;
    }
    private void OnMouseExit()
    {
        mesh.enabled = false;
    }
}
