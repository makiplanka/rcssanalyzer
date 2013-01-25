using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rcssanalyzer
{
    public partial class Main : Form
    {

        // 使用クラス
        private File file;
        private Analyze analyze;
        private Turns turns;

        public Main()
        {
            InitializeComponent(); 

        }

        private void MainLoad(object sender, EventArgs e)
        {
            
            //イベントハンドラをイベントに関連付ける
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1DoWork);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(backgroundWorker1ProgressChanged);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker1RunWorkerCompleted); 
             
        }

        /// <summary>
        /// ログファイルを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLogFileClick(object sender, EventArgs e)
        {
            // インスタンス作成
            OpenFileDialog dialog = new OpenFileDialog();

            // ファイルの種類
            dialog.Filter =
                "rcg ファイル (*.rcg)|*.rcg";

            // タイトル指定
            dialog.Title = "ログファイルを選択してください";

            // ダイアログボックスを閉じる前に現在のディレクトリを復元する
            dialog.RestoreDirectory = true;

            // ダイアログ表示
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // OK ボタンがクリックされたときファイルを読み込む
                try
                {
                    file = new File();

                    file.LoadFile(dialog.FileName);
                    textBox.Text += dialog.FileName + Environment.NewLine;
                    
                    // プログレスバーの初期化
                    toolStripProgressBar1.Minimum = 0;
                    toolStripProgressBar1.Maximum = 100;
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel1.Text = "[1/2] ログを構造解析中...";

                    // BackgroundWorker の ProgressChanged イベントが発生するようにする
                    backgroundWorker1.WorkerReportsProgress = true;
                    backgroundWorker1.WorkerSupportsCancellation = true;

                    backgroundWorker1.RunWorkerAsync();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// BackgroundWorker の DoWork イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;
            Parser parser = new Parser();
            int parcent = 0;

            analyze = new Analyze();
            turns = new Turns();

            try
            {
                // 処理のかかる時間を開始

                // ログを保持したクラスを構文解析クラスへ
                parser.SetLogs(ref file.AllLogs);

                // ログファイルの構造解析
                // Parser で 1 行ずつ構造解析 → Turns に保存
                while (!parser.GetFinished())
                {
                    parser.Execute();
                    turns.AddParsed(parser.GetParsed());

                    // プログレスバーの更新
                    parcent = (parser.GetAllLogsCount() - parser.GetHasLeftSize());
                    parcent = (int)((float)(parcent) / (float)(parser.GetAllLogsCount()) * 100);
                    backgroundWorker1.ReportProgress(parcent);
                }

                toolStripStatusLabel1.Text = "[2/2] 試合解析中...";

                // 試合解析
                for (int i = 2; i < turns.GetTurnCount(); i++)
                {
                    // 解析するターンが存在すれば
                    if (turns.GetExistTurn(i) && turns.GetExistTurn(i - 1)
                        // play_on ならば
                        && turns.GetTurn(i).GetPlaymode() == "play_on")
                    {
                        // 解析をセットして実行
                        analyze.SetControlTeam(turns.GetKickerThisTurn(i - 1, i));
                        analyze.Execute();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine +
                    ex.StackTrace, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// ProgressChanged イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;

        }
        
        /// <summary>
        /// RunWorkerCompleted イベントハンドラ
        /// 処理が終わったときに呼び出される
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "完了";

            // テキストボックスに記述される文字列
            textBox.Text +=
                "ボール支配率:" + Environment.NewLine +
                " " + turns.GetTeamName("l") + " " + analyze.GetPossessionLeftParcent().ToString("P1") +
                " vs " + turns.GetTeamName("r") + " " + analyze.GetPossessionRightParcent().ToString("P1") + Environment.NewLine +
                "---------------------------------" + Environment.NewLine;
            MessageBox.Show("完了", "読み込み完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

    }
}
