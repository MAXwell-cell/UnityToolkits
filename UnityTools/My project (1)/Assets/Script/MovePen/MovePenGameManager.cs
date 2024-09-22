using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePenGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> penCapList;
    [SerializeField] private List<GameObject> penList;
    private GameObject curCheckedGameObject;
    private bool TouchAbility = true;
    /// <summary>
    /// 交换动画的持续时间
    /// </summary>
    private float swapDuration = 0.5f;
    /// <summary>
    /// 定义颜色字典,int,color
    /// </summary>
    public Dictionary<int, Color> colorDictionary = new Dictionary<int, Color>();
    /// <summary>
    /// 通知校验笔帽颜色
    /// </summary>
    public event Action checkPenCapColor;
    /// <summary>
    /// 已经对应的笔数量
    /// </summary>
    [HideInInspector] public int Accuracy = 0;
    [SerializeField] private Text AccuracyText;
    private static MovePenGameManager instance;
    public static MovePenGameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // 初始化字典
        colorDictionary.Add(1, Color.red);
        colorDictionary.Add(2, Color.green);
        colorDictionary.Add(3, Color.blue);
        colorDictionary.Add(4, Color.yellow);
        BreakRank();
    }

    private void BreakRank()
    {
        // 创建1到4的整数列表
        List<int> colorNum = new List<int> { 1, 2, 3, 4 };

        // 打乱整数列表(随机化顺序)
        ShuffleList(colorNum);

        // 分配值时，确保符合新的规则
        for (int i = 0; i < penCapList.Count; i++)
        {
            // 如果当前候选值违反规则，交换它与后续的值，确保满足规则
            if (IsValueRestricted(i, colorNum[i]))
            {
                // 找到下一个符合规则的值并交换
                for (int j = i + 1; j < colorNum.Count; j++)
                {
                    if (!IsValueRestricted(i, colorNum[j]))
                    {
                        // 交换 i 和 j 位置的值
                        int temp = colorNum[i];
                        colorNum[i] = colorNum[j];
                        colorNum[j] = temp;
                        break;
                    }
                }
            }

            // 将符合规则的值赋给物体
            penCapList[i].GetComponent<PenCap>().capColorNumber = colorNum[i];
        }

        // 输出结果
        foreach (var obj in penCapList)
        {
            colorDictionary.TryGetValue(obj.GetComponent<PenCap>().capColorNumber, out Color color);
            obj.GetComponent<SpriteRenderer>().color = color;
            Debug.Log($"MyProperty: {obj.GetComponent<PenCap>().capColorNumber}");
        }
    }

    // 检查该值是否受限制，i表示元素位置，value表示候选的值
    private bool IsValueRestricted(int index, int value)
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

    // 打乱列表
    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }



    private void Update()
    {
        if (TouchAbility)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从鼠标点击位置发射一条射线
                RaycastHit hit; // 用于存储射线碰撞的信息
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
                // 如果射线碰撞到物体的 Collider
                if (Physics.Raycast(ray, out hit))
                {
                    // 检查碰撞的物体是否是当前物体
                    if (hit.collider != null && hit.collider.gameObject.name == "pen")
                    {
                        //存储当前碰撞的钢笔的GameObject和其挂载的Penscript脚本
                        GameObject curpen = hit.collider.gameObject;
                        PenScript penScript = curpen.GetComponent<PenScript>();
                        //如果没有当前选中的钢笔
                        if (curCheckedGameObject == null)
                        {
                            curpen.transform.Translate(new Vector3(0, 2, 0), Space.Self);
                            curCheckedGameObject = curpen;
                        }
                        else if (curCheckedGameObject == curpen)
                        {
                            curpen.transform.Translate(new Vector3(0, -2, 0), Space.Self);
                            curCheckedGameObject = null;
                        }
                        else if (curCheckedGameObject != curpen)
                        {
                            TouchAbility = false;
                            curpen.transform.Translate(new Vector3(0, 2, 0), Space.Self);
                            StartCoroutine(Interchange(curCheckedGameObject, curpen));
                            curCheckedGameObject = null;
                        }
                    }
                }
            }
        }

    }
    IEnumerator Interchange(GameObject curobj, GameObject othobj)
    {
        // 记录开始的位置和父节点
        Vector3 startPosition1 = curobj.transform.position;
        Vector3 startPosition2 = othobj.transform.position;
        Transform startParent1 = curobj.transform.parent;
        Transform startParent2 = othobj.transform.parent;

        float elapsedTime = 0f;

        // 逐渐插值位置和父节点
        while (elapsedTime < swapDuration)
        {
            float t = elapsedTime / swapDuration;

            // 平滑插值位置
            curobj.transform.position = Vector3.Lerp(startPosition1, startPosition2, t);
            othobj.transform.position = Vector3.Lerp(startPosition2, startPosition1, t);

            // 平滑插值父节点（如果需要平滑过渡父节点）
            if (t < 0.5f)
            {
                curobj.transform.parent = startParent1;
                othobj.transform.parent = startParent2;
            }
            else
            {
                curobj.transform.parent = startParent2;
                othobj.transform.parent = startParent1;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终的位置和父节点正确
        curobj.transform.position = startPosition2;
        othobj.transform.position = startPosition1;
        curobj.transform.parent = startParent2;
        othobj.transform.parent = startParent1;

        //等待时间再重置位置
        yield return new WaitForSeconds(0.3f);

        // 应用额外的变换
        curobj.transform.Translate(new Vector3(0, -2, 0), Space.Self);
        othobj.transform.Translate(new Vector3(0, -2, 0), Space.Self);

        //重置正确率计数
        Accuracy = 0;
        //发送事件通知钢笔校验颜色
        checkPenCapColor?.Invoke();
        AccuracyText.text = $"正确率{Accuracy*25}%";

        // 设置 TouchAbility 为 true（确保在你的类中定义了这个变量）
        TouchAbility = true;
    }
}
