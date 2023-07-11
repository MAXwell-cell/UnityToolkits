using System;
using System.Text;
using System.Collections.Generic;

namespace DialogTools
{
   class main
   {
    //   static void Main(string[] args)
    //   {
    //     Dialogtool test = new Dialogtool();
    //     Dictionary<int,string> target = test.createHashmap();
    //     string a =test.searchinhashmap(2,target);
    //     Console.Write(a);
    //   }
   }
   class Dialogtool
   {
    public Dictionary<int,string> createHashmap(){
        Dictionary<int,string> hashmap = new Dictionary<int, string>(){
            [1]="霍，你好母啊",
            [2]="你才母呢你全家都母",
            [3]="？",
            [4]="捏妈妈滴口老师",
            [5]="捏妈妈滴鬼鬼"
        };
        return hashmap;
    }
    public string searchinhashmap(int indexes,Dictionary<int,string> target){
        string targetvalue;
        target.TryGetValue(indexes,out targetvalue);
        return targetvalue; 
    }
   }
}