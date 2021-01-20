using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace Lab4
{
    class Program
    {
        public static Stopwatch time = new Stopwatch();
        public static string times = "";
        public static bool SortType = false;
        public static bool csv = false;
        public static bool All = false;
        public static bool mode = false;
        static void Main(string[] args)
        {
            //string[] args = new string[] { "10" };
            string path = Directory.GetCurrentDirectory() + "\\1.txt";
            string pathcsv = Directory.GetCurrentDirectory() + "\\table.csv";
            // GetRandomFile(path);
            try
            {
                if (args[0] == "0")
                {
                    Preparation("Insertion", path, pathcsv);
                }
                if (args[0] == "1")
                {
                    Preparation("Shell", path, pathcsv);
                }
                if (args[0] == "2")
                {
                    Preparation("Merge", path, pathcsv);
                }
                if (args[0] == "3")
                {
                    csv = true;
                    Preparation("QuickSort", path, pathcsv);
                }
                if (args[0] == "4")
                {
                    mode = true;
                    csv = true;
                    Preparation("PyramidSort", path, pathcsv);

                }
                if (args[0] == "5")
                {
                    csv = true;
                    Preparation("CountingSort", path, pathcsv);

                }
                if (args[0] == "6")
                {
                    mode = true;
                    csv = true;
                    Preparation("QuickSortMode", path, pathcsv);

                }
                if (args[0] == "7")
                {
                    SortType = true;
                    csv = true;
                    Preparation("QuickSort", path, pathcsv);
                    Preparation("CountingSort", path, pathcsv);

                }
                if (args[0] == "8")
                {
                    SortType = true;
                    mode = true;
                    csv = true;
                    Preparation("QuickSortMode", path, pathcsv);
                    Preparation("PyramidSort", path, pathcsv);

                }
                if (args[0] == "10")
                {
                    All = true;
                    csv = true;
                    SortType = true;
                    Preparation("Insertion", path, pathcsv);
                    Preparation("Shell", path, pathcsv);
                    Preparation("Merge", path, pathcsv);
                    Preparation("QuickSort", path, pathcsv);
                    Preparation("CountingSort", path, pathcsv);

                    mode = true;
                    Preparation("QuickSortMode", path, pathcsv);

                    Preparation("PyramidSort", path, pathcsv);

                }
                if (args[0] == "-1")
                    GetRandomFile(path);
            }
            catch
            {

            }


            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
           if(csv == true) Process.Start(new ProcessStartInfo(pathcsv) { UseShellExecute = true });
        }
        static void Preparation(string Sort, string path, string pathcsv)
        {
            if (!File.Exists(path))
            {
                GetRandomFile(path);
            }
            List<List<string>> Res = new List<List<string>>()
            {
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),
                 new List<string>(),

            };
            time.Restart();
            using (StreamReader sr = File.OpenText(path))
            {

                int i = 0;
                int j = 0;
                string rs;
                string tempStr = "";
                string[] temp;
                while ((rs = sr.ReadLine()) != null)
                {
                    tempStr = "";
                    temp = rs.Split(" ");
                    foreach (string t in temp)
                    {
                        char[] str = t.ToCharArray();
                        if (Sort == "Insertion")
                        {
                            tempStr += InsertionSort(str);
                            tempStr += " ";
                        }
                        if (Sort == "Shell")
                        {
                            tempStr += ShellSort(str);
                            tempStr += " ";
                        }
                        if (Sort == "Merge")
                        {
                            tempStr += MergeSort(str);
                            tempStr += " ";
                        }
                        if (Sort == "QuickSort")
                        {
                            tempStr += QuickSort(str);
                            tempStr += " ";
                        } 
                        if (Sort == "QuickSortMode")
                        {
                            tempStr += QuickSort(str);
                            tempStr += " ";
                        }                       
                        if (Sort == "CountingSort")
                        {
                            tempStr += CountingSort(str);
                            tempStr += " ";
                        }
                        if (Sort == "PyramidSort")
                        {
                            tempStr += PyramidSort(str);
                            tempStr += " ";
                        }
                        if (mode == true)
                        {
                            try
                            {
                                if (str[0] == 'a')
                                {
                                    j++;
                                    break;
                                }
                            }
                            catch { }

                        }
                        else
                        {
                            try
                            {
                                foreach (char letter in str)
                                {
                                    if (letter >= 48 && letter <= 57)
                                    {
                                        j++;
                                        break;
                                    }
                                }
                            }
                            catch { }
                        }

                    }
                    Res[j].Add(tempStr);
                    j = 0;
                    i++;

                }
            }
            time.Stop();
            string Сomplexity = "";
            if (Sort == "Insertion")
            {
                Сomplexity = (100 * 100).ToString();
            }
            if (Sort == "Shell")
            {
                Сomplexity = (Math.Sqrt(Math.Pow(100, 3))).ToString();
            }
            if (Sort == "Merge")
            {
                Сomplexity = (100 * Math.Log(100)).ToString();
            }
            if (Sort == "QuickSort")
            {
                Сomplexity = (100 * Math.Log(100)).ToString();
            } if (Sort == "QuickSortMode")
            {
                Сomplexity = (100 * Math.Log(100)).ToString();
            }
            if (Sort == "CountingSort")
            {
                Сomplexity = (100 * 4).ToString();
            }
            if (Sort == "PyramidSort")
            {
                Сomplexity = (100 * Math.Log(100)).ToString();
            }
            times += Sort + ":" + time.Elapsed + " Dificult:" + Сomplexity + "\r\n";


            if (SortType == false)
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(times);
                    foreach (var s in Res)
                        foreach (var str in s)
                            sw.WriteLine(str);
                }
            }
            else
            {
                if (Sort == "PyramidSort" && All==false)
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(times);
                        foreach (var s in Res)
                            foreach (var str in s)
                                sw.WriteLine(str);
                    }
                }
                else if (Sort == "CountingSort" && mode == false && All == false)
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(times);
                        foreach (var s in Res)
                            foreach (var str in s)
                                sw.WriteLine(str);
                    }
                }
                else if (Sort == "PyramidSort" && All == true)
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(times);
                        foreach (var s in Res)
                            foreach (var str in s)
                                sw.WriteLine(str);
                    }
                }



            }
            if (SortType == false)
            {
                if (csv == true)
                {
                    using (StreamWriter sw = File.CreateText(pathcsv))
                    {
                        sw.WriteLine(times);
                        string strr = "";
                        foreach (var s in Res)
                            foreach (string str in s)
                            {
                                strr = str;
                                strr = strr.Replace(' ', ';');
                                sw.WriteLine(strr);
                            }
                    }
                }
            }
            else
            {
                if (Sort == "PyramidSort")
                {
                    if (csv == true)
                    {
                        using (StreamWriter sw = File.CreateText(pathcsv))
                        {
                            sw.WriteLine(times);
                            string strr = "";
                            foreach (var s in Res)
                                foreach (string str in s)
                                {
                                    strr = str;
                                    strr = strr.Replace(' ', ';');
                                    sw.WriteLine(strr);
                                }
                        }
                    }
                }
                if (Sort == "CountingSort" && mode == true)
                {
                    if (csv == true)
                    {
                        using (StreamWriter sw = File.CreateText(pathcsv))
                        {
                            sw.WriteLine(times);
                            string strr = "";
                            foreach (var s in Res)
                                foreach (string str in s)
                                {
                                    strr = str;
                                    strr = strr.Replace(' ', ';');
                                    sw.WriteLine(strr);
                                }
                        }
                    }
                }
            }

        }
        private static Random random = new Random();

        public static void GetRandomFile(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int i = 0; i <= 100; i++)
                {
                    sw.WriteLine(GetRandomString(random.Next(20, 50)));
                }
            }
        }
        public static string GetRandomString(int length)
        {
            var chars = new char[length];
            var possibleLetters = "abcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < length; i++)
            {
                chars[i] = possibleLetters[random.Next(0, possibleLetters.Length - 1)];
                if (i % 5 == 0 && i != 0)
                {
                    chars[i] = ' ';
                }
            }

            return new string(chars);
        }
        static void Swap(ref char a, ref char b)
        {
            var t = a;
            a = b;
            b = t;
        }
        static string ShellSort(char[] array)
        {
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (int i = d; i < array.Length; i++)
                {
                    int j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - d]);
                        j = j - d;
                    }
                }
                d = d / 2;
            }

            return new string(array);
        }
        public static string InsertionSort(char[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                char cur = array[i];
                int j = i;
                while (j > 0 && cur < array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;

                }
                array[j] = cur;
            }
            return new string(array);
        }
        static void Merge(char[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new char[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //сортировка слиянием
        static char[] MergeSort(char[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }

        static string MergeSort(char[] array)
        {
            return new string(MergeSort(array, 0, array.Length - 1));
        }
        //быстрая сортировка
        static char Partition(char[] array, int minIndex, int maxIndex)
        {
            char pivot = Convert.ToChar(minIndex - 1);
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }


        static char[] QuickSort(char[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, Convert.ToChar(pivotIndex - 1));
            QuickSort(array, Convert.ToChar(pivotIndex + 1), maxIndex);

            return array;
        }

        static string QuickSort(char[] array)
        {
            return new string(QuickSort(array, '0', array.Length - 1));
        }

        //Пиромидальная 

        static int add2pyramid(char[] arr, int i, int N)
        {
            int imax;
            char buf;
            if ((2 * i + 2) < N)
            {
                if (arr[2 * i + 1] < arr[2 * i + 2]) imax = 2 * i + 2;
                else imax = 2 * i + 1;
            }
            else imax = 2 * i + 1;
            if (imax >= N) return i;
            if (arr[i] < arr[imax])
            {
                buf = arr[i];
                arr[i] = arr[imax];
                arr[imax] = buf;
                if (imax < N / 2) i = imax;
            }
            return i;
        }

        static string PyramidSort(char[] arr)
        {
            int len = arr.Length;
            //step 1: building the pyramid
            for (int i = len / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = add2pyramid(arr, i, len);
                if (prev_i != i) ++i;
            }

            //step 2: sorting
            char buf;
            for (int k = len - 1; k > 0; --k)
            {
                buf = arr[0];
                arr[0] = arr[k];
                arr[k] = buf;
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = add2pyramid(arr, i, k);
                }
            }
            return new string(arr);
        }
        //кореневого сортування
        static string CountingSort(char[] arr)
        {
            int i, j;
            char[] tmp = new char[arr.Length];
            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;
                for (i = 0; i < arr.Length; ++i)
                {
                    bool move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        arr[i - j] = arr[i];
                    else
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
            return new string(arr);
        }
    }
}
