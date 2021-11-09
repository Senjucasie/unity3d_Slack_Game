using System;
using UnityEngine;

[System.Runtime.InteropServices.Guid("7E40067C-1D26-4BF1-A76C-FDFEB323566C")]
public class MovingCube : MonoBehaviour
{
    [SerializeField]
    private float _speed=1.0f;

    public static MovingCube LastCube { get; private set; }
    public static MovingCube Currentcube { get; private set; }

    private readonly int _positionAdjust=2;
    

    internal void Stop()
    {
        _speed = 0;
        float movedifferenceZ = transform.position.z-LastCube.transform.position.z ;
        float direction = movedifferenceZ > 0 ? 1.0f : -1.0f;
        splitz(movedifferenceZ,direction);
    }

    private void splitz(float movedifferenceZ,float direction)
    {
        float newzscale = LastCube.transform.localScale.z - Mathf.Abs(movedifferenceZ);
        float fallingcubezscale = LastCube.transform.localScale.z - newzscale;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newzscale);

        float newzposition = movedifferenceZ / _positionAdjust;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newzposition);

        float fallingcubeedge = transform.position.z+(newzscale / _positionAdjust*direction);
        float fallingcubeposition = (fallingcubeedge + fallingcubezscale / _positionAdjust*direction);

        createcube(fallingcubezscale,fallingcubeposition);

       
    }

    private void createcube(float fallingzscale,float newzposition)
    {
       var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingzscale);
        cube.transform.localPosition=new Vector3(transform.localPosition.x,transform.localPosition.y, newzposition);

        cube.AddComponent<Rigidbody>();
        Destroy(cube, 2f);

    }

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();

        if (LastCube != this)
            Currentcube = this;


    }
    private void Update()
    {
         transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
