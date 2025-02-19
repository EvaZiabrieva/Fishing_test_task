using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class FishingLineView : BaseFishingLineView, IUpdatable
{
    private LineRenderer _lineRenderer;
    private PointsData _pointsData;

    private Vector3 _middlePoint;
    private List<Vector3> pointsList;
    private int vertexCount = 12;
    public FishingLineView(PointsData pointsData, LineRenderer lineRenderer, float maxLenght) : base(maxLenght)
    {
        _lineRenderer = lineRenderer;
        _pointsData = pointsData;

        SystemsContainer.GetSystem<UpdatableSystem>().RegisterUpdatable(this);
    }

    //Used to simulate the line through a Bezier curve
    public void BezierCurve()
    {
        if (GetCurrentDistance() >= _currentLength)
        {
            _middlePoint = GetMiddlePoint();
            SetLine(_middlePoint);
        }
        else
        {
            float distCoef = Mathf.InverseLerp(0, _currentLength, GetCurrentDistance());
            Vector3 middlePoint = GetMiddlePoint();
            Vector3 direction = middlePoint - _middlePoint;
            Vector3 mp = middlePoint + direction;
            Vector3 curveMiddlePoint = Vector3.Lerp(mp, middlePoint, distCoef);
            SetLine(curveMiddlePoint);
        }
    }
    //Drawing the line by using LineRenderer 
    private void SetLine(Vector3 middlePoint)
    {
        pointsList = new List<Vector3>();
        for (float radio = 0; radio <= 1; radio += 1f / vertexCount)
        {
            Vector3 tangentLineVertex1 = Vector3.Lerp(_pointsData.StartPoint.position, middlePoint, radio);
            Vector3 tangentLineVertex2 = Vector3.Lerp(middlePoint, _pointsData.EndPoint.position, radio);
            Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, radio);
            pointsList.Add(bezierPoint);
        }
        _lineRenderer.positionCount = pointsList.Count;
        _lineRenderer.SetPositions(pointsList.ToArray());
    }
    private Vector3 GetMiddlePoint()
    {
        float middleCoordsX = (_pointsData.StartPoint.position.x + _pointsData.EndPoint.position.x) / 2;
        float middleCoordsY = (_pointsData.StartPoint.position.y + _pointsData.EndPoint.position.y) / 2;
        float middleCoordsZ = (_pointsData.StartPoint.position.z + _pointsData.EndPoint.position.z) / 2;

        return new Vector3(middleCoordsX, middleCoordsY, middleCoordsZ);
    }

    private float GetCurrentDistance() => Vector3.Distance(_pointsData.StartPoint.position, _pointsData.EndPoint.position);

    public void ExecuteUpdate()
    {
        BezierCurve();
    }
}
public class PointsData
{
    public Transform StartPoint;
    public Transform EndPoint;
}