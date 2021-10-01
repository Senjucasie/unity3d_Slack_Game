using System;
using UnityEngine;

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
        Debug.Log(movedifferenceZ);
        splitz(movedifferenceZ);
    }

    private void splitz(float movedifferenceZ)
    {
        float newzscale = LastCube.transform.localScale.z - Mathf.Abs(movedifferenceZ);
        float fallingcubez = LastCube.transform.localScale.z - newzscale;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newzscale);

        float newzposition = movedifferenceZ / _positionAdjust;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newzposition);
       
    }

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();

        Currentcube = this;

    }
    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
