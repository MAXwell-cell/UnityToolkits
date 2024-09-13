using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePenGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> penCapList;
    void Start()
    {
        BreakRank();
    }
    public void BreakRank()
    {
        // 创建1到4的整数列表
        List<int> colorNum = new List<int> { 1, 2, 3, 4 };

        // 打乱整数列表(随机化顺序)
        int n = colorNum.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = colorNum[k];
            colorNum[k] = colorNum[n];
            colorNum[n] = value;
        }

        // 分配值时，确保符合新的规则
        for (int i = 0; i < penCapList.Count; i++)
        {
            for (int j = 0; j < colorNum.Count; j++)
            {
                if (!IsValueRestricted(i, colorNum[j]))
                {
                    // 如果值符合规则，将其赋值给对象，并从候选列表中移除
                    penCapList[i].GetComponent<PenCap>().capColorNumber = colorNum[j];
                    colorNum.RemoveAt(j);
                    break;
                }
            }
        }

        // 输出结果
        foreach (var obj in penCapList)
        {
            Debug.Log($"MyProperty: {obj.GetComponent<PenCap>().capColorNumber}");
        }
    }
    // 检查该值是否受限制，i表示元素位置，value表示候选的值
    public bool IsValueRestricted(int index, int value)
    {
        // 根据规则，第一个元素不能是1，第二个元素不能是2，依此类推
        if ((index == 0 && value == 1) ||
            (index == 1 && value == 2) ||
            (index == 2 && value == 3) ||
            (index == 3 && value == 4))
        {
            return true;
        }
        return false;
    }
}
