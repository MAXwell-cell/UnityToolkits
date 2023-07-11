using System;
using System.IO;
namespace Name
{
    class main{
        static void Main(string[] args){
            structTest1 demo = new structTest1();
            demo.struct1();
        }
    }
    class strtest1{
        public void stringconcatenation(){
            string str1 = "Hello";
            string str2 = " world!";
            string fullname = str1+str2;
            Console.WriteLine(fullname);
        }
        public void chartransformtostring(){
            char[] leeters = {'H','e','l','l','o'};
            string str1 = new string(leeters);
            Console.WriteLine(str1);
        }
        public void stringjoinmethod(){
            string[] strary = {"String","Join","method"};
            string str1 = string.Join(" ",strary);
            Console.WriteLine(str1);
        }
    }
    class structTest1{
            struct subject
            {
                public string name;
                public string chat;
                public int number;
            }
            public void struct1(){
                subject structtest=new subject{
                    name="夜",
                    chat="你好",
                    number=01
                };
                Console.WriteLine("his property is name:{0},chat:{1},number:{2}",
                structtest.name,structtest.chat,structtest.number);
            }
    }
    class enumtest1{
        enum enumtest2{Sun,Mod,Wed};
        public void enumtest(){
        }
    }
    class fileflow{
        public void renameAllFile(string filePath){
            String[] fileNameList = Directory;

        }
    }
}