using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace dataAnalize
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    txtFilePath.Text = ofd.FileName;
                }
            }
        }

        private void btnSelSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txtSavePath.Text = foldPath;
            }
        }

        // 测试解析点击事件
        private void TestStart_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择数据文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ClearShow();

            try
            {
                // 获取文件的所有字节信息
                byte[] allbytes = FileHelper.ReadFileByteData(filePath);
                AddShow("总体", "总字节数：" + allbytes.Length);
                AddLine();

                // 获取头信息
                int sNumber = MsAnalyzeUtil.GetIntLitter(allbytes, 278, 281 - 278 + 1);
                AddShow("头信息", "色谱数据数量：" + sNumber);
                int startB = MsAnalyzeUtil.GetIntLitter(allbytes, 260, 263 - 260 + 1);
                AddShow("头信息", "第一个色谱数据起点：" + startB);
                AddLine();

                // 第一个色谱数据
                int sStartA = 2 * (startB - 1);
                int zStartB = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA, 4);
                AddShow("色谱数据", "第一个色谱的质谱地址：" + zStartB);
                int sTime = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA + 4, 4);
                AddShow("色谱数据", "第一个色谱的保留时间(ms)：" + sTime);
                AddShow("色谱数据", "第一个色谱的保留时间(m)：" + MsAnalyzeUtil.MsToMin(sTime));
                int sAbundance = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA + 8, 4);
                AddShow("色谱数据", "第一个色谱的信号值：" + sAbundance);
                AddLine();

                // 第一个质谱数据
                int zStartA = zStartB * 2;
                int zTime = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA, 4);
                AddShow("质谱头", "第一个质谱的保留时间(ms)：" + zTime);
                AddShow("质谱头", "第一个质谱的保留时间(m)：" + MsAnalyzeUtil.MsToMin(zTime));
                int zNumber = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA + 10, 2);
                AddShow("质谱头", "第一个质谱的峰个数：" + zNumber);
                int zMaxMass = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA + 12, 2);
                AddShow("质谱头", "第一个质谱最高峰的质量数：" + zMaxMass);
                int zMaxAbundance = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA + 14, 2);
                AddShow("质谱头", "第一个质谱最高峰的信号值：" + zMaxAbundance);
                AddLine();

                // 第一个色谱的所有质谱点
                zStartA = zStartA + 16;
                int zMass = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA, 2);
                AddShow("质谱数据", "第一个质谱第一峰的质量数：" + zMass);
                int zAbundance = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA + 2, 2);
                AddShow("质谱数据", "第一个质谱第一峰的信号值：" + zAbundance);
                zStartA = zStartA + 4;
                zMass = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA, 2);
                AddShow("质谱数据", "第一个质谱第二峰的质量数：" + zMass);
                zAbundance = MsAnalyzeUtil.GetIntLitter(allbytes, zStartA + 2, 2);
                AddShow("质谱数据", "第一个质谱第二峰的信号值：" + zAbundance);
                AddLine();

                // 第二个色谱数据
                sStartA = sStartA + 12;
                zStartB = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA, 4);
                AddShow("色谱数据", "第二个色谱的质谱地址：" + zStartB);
                sTime = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA + 4, 4);
                AddShow("色谱数据", "第二个色谱的保留时间(ms)：" + sTime);
                AddShow("色谱数据", "第二个色谱的保留时间(m)：" + MsAnalyzeUtil.MsToMin(sTime));
                sAbundance = MsAnalyzeUtil.GetIntLitter(allbytes, sStartA + 8, 4);
                AddShow("色谱数据", "第二个色谱的信号值：" + sAbundance);
            }
            catch (Exception ex)
            {
                AddShow("错误", "文件解析错误：" + ex.Message);
            }
        }

        // 全部解析点击事件
        private void AllStart_Click(object sender, EventArgs e)
        {
            // 所有的路径处理
            string filePath = txtFilePath.Text;
            string savePath = txtSavePath.Text;
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("请选择数据文件与保存路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string saveFilePath = savePath + "\\" + FileHelper.GetFileNameNoExtension(filePath) + ".txt";
            if (FileHelper.IsExistFile(saveFilePath))
                FileHelper.ClearFile(saveFilePath);
            else
                FileHelper.CreateFile(saveFilePath);
            MsAnalyzeUtil.Init(saveFilePath);

            // 开始解析
            ClearShow();
            AddShow("说明", "开始进行文本解析");
            AddShow("说明", "保存文件路径：" + saveFilePath);
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                // 获取文件的所有字节信息
                byte[] allDatas = FileHelper.ReadFileByteData(filePath);

                // 进行全部文件的解析导出
                MsAnalyzeUtil.GetChrom(allDatas);
                stopwatch.Stop();
                AddShow("说明", "文件解析完成。用时(ms)：" + stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                AddShow("错误", "文件解析错误：" + ex.Message);
            }
            Process.Start("explorer.exe", savePath);
        }

        /*--------------- 数据输出的方法 ----------------------*/

        // 清理展示内容
        private void ClearShow()
        {
            listResult.Items.Clear();
        }

        // 本地输出
        private void AddShow(string type, string msg)
        {
            listResult.Items.Add("【" + type + "】 " + msg);
        }
        // 本地输出
        private void AddLine()
        {
            listResult.Items.Add("--------------------------------------");
        }
    }
}
