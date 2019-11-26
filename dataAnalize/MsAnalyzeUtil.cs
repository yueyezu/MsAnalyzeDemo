using System;
using System.Collections.Generic;

namespace dataAnalize
{
    /// <summary>
    /// 安捷伦数据MS文件，解析的实现类，具体协议信息，可以找对应的文献对应，这里不单独提供了。
    /// 
    /// 后续完善考虑:
    /// 1、 对于解析的数据，可以存放到实体类，然后统一进行输出。
    /// 2、 转化效率上进行优化提升。
    /// 3、 单例结构改造 or 静态初始化方法优化，模块封装更集中。
    /// </summary>
    public class MsAnalyzeUtil
    {
        private static string _logFilePath = "";

        /// <summary>
        /// 初始化分析方法
        /// 可以改造为using方式，将具体操作都私有化，资源释放管理起来。
        /// </summary>
        /// <param name="logFilePath"></param>
        public static void Init(string logFilePath = "")
        {
            _logFilePath = logFilePath;
        }

        /**------------------- 文件解析的主要方法 -----------------------**/

        /// <summary>
        /// 获取所有的色谱数据，包括其下质谱数据
        /// </summary>
        /// <param name="allData">全部的数据</param>
        public static void GetChrom(byte[] allData)
        {
            OutResult("总体", "总字节数：" + allData.Length);

            int number = GetIntLitter(allData, 278, 281 - 278 + 1); // 色谱总数量
            int startAddr = GetIntLitter(allData, 260, 263 - 260 + 1); //第一个色谱的地址
            OutResult("头信息", string.Format("色谱数据信息: 色谱点数-{0}", number));

            int startBit = 2 * (startAddr - 1); //第一个色谱的位地址
            int startMass, time, abundance;
            for (int i = 0; i < number; i++)
            {
                startMass = GetIntLitter(allData, startBit, 4);
                time = GetIntLitter(allData, startBit + 4, 4);
                abundance = GetIntLitter(allData, startBit + 8, 4);

                OutResult("色谱", String.Format("{0,4}：[{3},{1},{2}]", i + 1, MsToMin(time), abundance, time));

                GetAllMass(allData, startMass, i + 1);

                startBit = startBit + 12;
            }
        }

        /// <summary>
        /// 获取所有质谱数据
        /// </summary>
        /// <param name="allData"></param>
        /// <param name="startAddr"></param>
        /// <param name="sNo"></param>
        private static void GetAllMass(byte[] allData, int startAddr, int sNo)
        {
            int startBit = startAddr * 2;
            int time = GetIntLitter(allData, startBit, 4);
            int number = GetIntLitter(allData, startBit + 10, 2);
            int maxMass = GetIntLitter(allData, startBit + 12, 2);
            int maxAbundance = GetIntLitter(allData, startBit + 14, 2);
            OutResult("质谱数据", String.Format("{0,4}：保留时间-{1},质谱点数-{2},最高峰-[{3},{4}]",
                sNo, MsToMin(time), number, maxMass, maxAbundance));

            Dictionary<int, int> massPoints = new Dictionary<int, int>();
            string allPoints = "";

            startBit = startBit + 16;
            int mass, abundance;
            for (int i = 0; i < number; i++)
            {
                mass = GetIntLitter(allData, startBit, 2);
                abundance = GetIntLitter(allData, startBit + 2, 2);
                abundance = abundance / 20;

                massPoints.Add(mass, abundance);
                allPoints += string.Format("[{0},{1}] ", mass, abundance);

                startBit = startBit + 4;
            }

            OutResult("质谱数据", String.Format("{0,4}：{1}", sNo, allPoints));
        }

        /// <summary>
        /// 解析结果的输出
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        private static void OutResult(string type, string msg)
        {
            if (string.IsNullOrEmpty(_logFilePath))
            {
                string str = string.Format("【{0}】 {1}", type, msg) + Environment.NewLine;
                FileHelper.AppendText(_logFilePath, str);
            }
        }

        /*----------- 对外的公开方法 不测试时，可以私有化 -------------*/

        /// <summary>
        /// 在字节数组的指定位置获取数值
        /// </summary>
        /// <param name="srcArr"></param>
        /// <param name="start"></param>
        /// <param name="count">可以为：2，4</param>
        /// <returns></returns>
        public static int GetIntLitter(byte[] srcArr, int start, int count)
        {
            byte[] times = new byte[count];
            Buffer.BlockCopy(srcArr, start, times, 0, count);
            Array.Reverse(times);

            if (count == 4)
            {
                int number = BitConverter.ToInt32(times, 0);
                return number;
            }
            else
            {
                int number = BitConverter.ToInt16(times, 0);
                return number;
            }
        }

        /// <summary>
        /// 在字节数组的指定位置获取数值
        /// </summary>
        /// <param name="srcArr"></param>
        /// <param name="start"></param>
        /// <param name="count">可以为：4，8</param>
        /// <returns></returns>
        public static double GetFloatLitter(byte[] srcArr, int start, int count)
        {
            byte[] times = new byte[count];
            Buffer.BlockCopy(srcArr, start, times, 0, 4);
            Array.Reverse(times);

            if (count == 4)
            {
                double number = BitConverter.ToSingle(times, 0);
                return number;
            }
            else
            {
                double number = BitConverter.ToDouble(times, 0);
                return number;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static double MsToMin(int ms)
        {
            int sToMs = 60 * 1000;
            int m = ms / sToMs;
            double mx = (ms % sToMs) / (sToMs * 1.0);
            double min = Math.Round(m + mx, 5, MidpointRounding.AwayFromZero);

            return min;
        }
    }
}
