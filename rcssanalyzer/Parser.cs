using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;

namespace rcssanalyzer
{
    /// <summary>
    /// 構文解析クラス
    /// </summary>
    class Parser
    {

        private Queue<string> logs = new Queue<string>();   // 生ログのキュー
        private int allLogsCount = 0;  // 生ログの要素数

        protected MatchCollection message;

        protected List<Hashtable> parsed = new List<Hashtable>();

        /// <summary>
        /// 解析する生ログをセット
        /// </summary>
        /// <param name="data">ログファイル</param>
        public void SetLogs(ref Queue<string> data)
        {
            logs = data;
            allLogsCount = data.Count;
        }

        /// <summary>
        /// 解析するメッセージをセット
        /// </summary>
        /// <param name="msg"></param>
        public void SetMessage(MatchCollection msg)
        {
            message = msg;
            parsed.Clear();
        }

        /// <summary>
        /// 解析した結果を返す
        /// </summary>
        /// <returns></returns>
        public List<Hashtable> GetParsed()
        {
            return parsed;
        }

        /// <summary>
        /// 処理が終了したかを返す
        /// </summary>
        /// <returns></returns>
        public bool GetFinished()
        {
            if (logs.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ログの行数を返す
        /// </summary>
        /// <returns></returns>
        public int GetAllLogsCount()
        {
            return allLogsCount;
        }

        /// <summary>
        /// まだ処理の終わっていないログの行数
        /// </summary>
        /// <returns></returns>
        public int GetHasLeftSize()
        {
            return logs.Count();
        }

        /// <summary>
        /// 配列を結果に出力
        /// </summary>
        /// <param name="ht"></param>
        protected void SetResult(Hashtable ht)
        {
            parsed.Add((Hashtable)ht.Clone());
        }

        /// <summary>
        /// 解析実行
        /// </summary>
        public void Execute()
        {
            try
            {
                ParserPlaymode pp = new ParserPlaymode();
                ParserShow ps = new ParserShow();

                parsed.Clear();
                string msg = logs.Dequeue();

                // 空白で区切る
                Regex r = new Regex(@"(\S+)");
                MatchCollection m = r.Matches(msg);

                // 最初の文字列で各構造解析クラスへ分類
                switch (m[0].ToString().Trim('('))
                {
                    case "playmode":
                        pp.SetMessage(m);
                        pp.Execute();
                        parsed = pp.GetParsed();
                        break;
                    case "show":
                        ps.SetMessage(m);
                        ps.Execute();
                        parsed = ps.GetParsed();
                        break;
                    case "team":
                        // 左チームと右チームを取得
                        Hashtable data = new Hashtable();

                        data.Add("type", "team");
                        data.Add("leftTeamName", m[2].ToString());
                        data.Add("rightTeamName", m[3].ToString());

                        this.SetResult(data);
                        break;
                    default:
                        // 解析対象以外は type -> null とする
                        Hashtable ht = new Hashtable();
                        ht["type"] = "null";
                        this.SetResult(ht);
                        break;
                }

                
            }
            catch
            {
                throw;
            }
        }
    }
}
