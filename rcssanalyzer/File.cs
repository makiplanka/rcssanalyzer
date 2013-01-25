using System;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace rcssanalyzer
{
    class File
    {
        /// <summary>
        /// ログの各行の配列
        /// </summary>
        public Queue<string> AllLogs;
        
        /// <summary>
        /// 各行が配列になった生ログを返す
        /// </summary>
        /// <returns>生ログ</returns>
        public Queue<string> GetLogs()
        {
            return AllLogs;
        }

        /// <summary>
        /// ファイル読み込みメソッド
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>ファイル読み込みに成功すれば true を返す</returns>
        public void LoadFile(string path)
        {
            try
            {
                AllLogs = new Queue<string>();

                System.IO.StreamReader sr = new System.IO.StreamReader(path);
                while (sr.Peek() > -1)
                {
                    AllLogs.Enqueue(sr.ReadLine());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
