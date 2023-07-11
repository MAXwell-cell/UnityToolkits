using System;
namespace Name
{
    // class main{
    //     static void Main(string[] args){
    //         leetcode1 test = new leetcode1();
    //         int[] arrary = new int[]{1,2,3,4,5,6};
    //         Console.Write(test.Twosum(arrary,7)[0]+"\n");
    //         Console.Write(test.Twosum(arrary,7)[1]);
    //     }
    // }
    class leetcode1{
        public int[] Twosum(int[] nums,int target)
        {
            for (int i = 0;i<nums.Length;i++)
            {
                for(int j = i+1;j<nums.Length;j++)
                {
                    if(target==nums[i]+nums[j])
                    {
                        return new int[]{i,j};
                    }
                }
            }
            return new int[]{0,0};
        }
    }
    class leetcode2{

    }
}