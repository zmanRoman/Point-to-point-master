using System.Collections.Generic;
using UnityEngine;


public class PointMovement : MonoBehaviour
{
    public static PointMovement Instance { get; private set; }
    [SerializeField] private GameObject point;
    [SerializeField] private List<Vector3> target;
    [SerializeField] private List<GameObject> points;
    private float _speed;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        LoadPointsFromMemory();
        Menu.Instance.Slider.onValueChanged.AddListener(delegate {SetSpeed (Menu.Instance.Slider.value);});
    }
   
    private void Update()
    {
        if(target.Count != 0)
        { //moving 
            transform.position = Vector2.MoveTowards(transform.position, target[0], _speed*Time.deltaTime);
            if (transform.position == target[0])
            {
                target.RemoveAt(0);
                Destroy(points[0]);
                points.RemoveAt(0);
            }
        }

        if (!Input.GetKeyDown(KeyCode.Mouse0)) return; //mouse point to list
      
        var a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.Add(new Vector3(a.x, a.y, 0f));
        points.Add(Instantiate(point, target[^1], Quaternion.identity));
        
    }

    private void SetSpeed(float value)
    {
        _speed = value;
    }

    private void LoadPointsFromMemory()
    {
        var anArray = PlayerPrefsX.GetVector3Array("VectorsArray");
        target.AddRange(anArray);
        foreach (var vector in target)
        {
            points.Add(Instantiate(point, vector, Quaternion.identity));
        }
        gameObject.transform.position = PlayerPrefsX.GetVector3("position", transform.position);
    }

    public void ClearPoint()
    {
        target.Clear();
        foreach(var a in points)
        {
            Destroy(a);
        }
        points.Clear();
    }
    private void OnApplicationQuit()
    { //save date
        PlayerPrefsX.SetVector3("position", transform.position);
        var anArray  = target.ToArray();
        PlayerPrefsX.SetVector3Array("VectorsArray", anArray);
    }
    
}

        

