using System.Collections;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private Transform[] PointDonutGroup;

    private int PointDonutIndex;
    
    private bool Side = true;

    public Transform[] GetNewPointGroup()
    {
        PointDonut PointDonut = PointDonutGroup[PointDonutIndex].GetComponent<PointDonut>();

        Transform[] Result = PointDonut.PointGroupRight;

        if (Side)
        {
            SetPointGroup(PointDonut.PointGroupLeft, false);
            SetPointGroup(PointDonut.PointGroupRight, true);
        }
        else
        {
            SetPointGroup(PointDonut.PointGroupRight, false);
            SetPointGroup(PointDonut.PointGroupLeft, true);
            
            Result = PointDonut.PointGroupLeft;
        }

        void SetPointGroup(Transform[] PointGroup, bool Active)
        {
            if (Active)
            {
                StartCoroutine(GetDown(PointGroup));
            }
            else
            {
                for (int i = 0; i < PointGroup.Length; i++)
                {
                    PointGroup[i].gameObject.SetActive(false);
                }
            }
        }

        Side = !Side;
        
        PointDonutGroup[PointDonutIndex].transform.position = new Vector3(0.0f, 0.0f,
            PointDonutGroup[PointDonutIndex].transform.position.z + 24.0f);

        if (PointDonutIndex + 1 < PointDonutGroup.Length) PointDonutIndex = PointDonutIndex + 1;
        else PointDonutIndex = 0;
        
        return Result; 
    }

    private IEnumerator GetDown(Transform[] PointGroup)
    {
        for (int i = 0; i < PointGroup.Length; i++)
        {
            PointGroup[i].gameObject.SetActive(true);
            PointGroup[i].GetComponent<Point>().Down();
            
            yield return new WaitForSeconds(0.25f);
        }
    }
}
