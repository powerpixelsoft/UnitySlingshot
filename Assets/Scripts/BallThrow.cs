using UnityEngine;
using System.Collections;

public class BallThrow : MonoBehaviour 
{
    Trajectory T;

    //int width = Screen.width;
    //int height = Screen.height;

    public float shotValue = 0.0f;
    public float barTopLimit = 200.0f;
    public float barHeight;
    public Texture texture;
    public GameObject ball;
    public Transform source;

    Material material;
    Color color; 
    //float counter = 0.0f;
    float counter1 = 0.0f;
    bool up = true;
    bool down = false;
    public float snapValue = 0.0f;

    public float newShotPower = 12.5f;
    void Start()
    {
        T = GameObject.Find("tip").GetComponent<Trajectory>();
        color = new Color(1, 1, 1, 1);
    }

    void Update()
    {       
        CountPressPingPong();
        shotValue = (int)(ShotPower(barHeight));
        MakeShot(T.CheckDistance2D(T.powMul));
        barHeight = CountPressPingPong() * barTopLimit;
    }
    void OnGUI()
    {
        Graphics.DrawTexture(new Rect(40, 600, 20, -1 * barHeight), texture, 
                             new Rect(0, 0, 10, 100), 0, 0, 0, 0,
                             ChangeColor(color, MapRange(barHeight, 0.0f, barTopLimit, 0.0f, 1.0f)), material);
    }
    void MakeShot(float power)
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject prefab = Instantiate(ball, source.position, Quaternion.identity) as GameObject;
            prefab.GetComponent<Rigidbody>().AddForce(source.transform.TransformDirection(0, 0, 1 * power * 1.25f), ForceMode.Impulse);
            Destroy(prefab, 5.0f);
        }
    }
    public float ShotPower(float inputValue)
    {
        if (Input.GetMouseButtonUp(0))
        {
            snapValue = inputValue;
            return snapValue;
        }
        return snapValue;
    }
    Color ChangeColor(Color color, float height)
    {
        Color color1 = new Color(0,1,0,1);
        Color color2 = new Color(1,0,0,1);

        color = Color.Lerp(color1, color2, height);
        return color;
    }
    float MapRange(float inputValue, float inputValueMin, float inputValueMax, float outputValueMin, float outputValueMax)
    {
        float outputValue = (inputValue - inputValueMin) / 
                            (inputValueMax - inputValueMin) * 
                            (outputValueMax - outputValueMin) + outputValueMin;
        return outputValue;
    }
    float CountPressPingPong()
    {
        if (Input.GetMouseButton(0))
        {
            if (up)
            {
                counter1 += Time.deltaTime/2;
                if (counter1 >= 1.0f)
                {
                    up = false;
                    down = true;
                    return counter1;
                }
                return counter1;
            }
            else if (down)
            {
                counter1 -= Time.deltaTime/2;
                if (counter1 <= 0.0f)
                {
                    up = true;
                    down = false;
                    return counter1;
                }
                return counter1;
            }
            return counter1;
        }
        else
        {
            counter1 = 0.0f;
            return counter1;
        }
        //Debug.Log(string.Format("Counter value = {0}, UP = {1}, DOWN = {2}", counter1, up, down));
    }



    //-------------------------------------
    //float CountPressOneWay()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        counter += Time.deltaTime;
    //        if (counter >= 1.0f)
    //        {
    //            counter = 1.0f;
    //            return counter;
    //        }
    //        return counter;
    //    }
    //    else
    //    {
    //        counter = 0.0f;
    //        return counter;
    //    }
    //}

}
