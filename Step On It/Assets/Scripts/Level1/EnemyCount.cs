using UnityEngine;
using System.Collections;

public class EnemyCount : MonoBehaviour
{
    int count;
    // Update is called once per frame
    public int getCount()
    {
        return count;
    }
    public void setCount(int count)
    {
        this.count = count;
    }
}
