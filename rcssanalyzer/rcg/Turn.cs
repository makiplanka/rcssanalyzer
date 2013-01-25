using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rcssanalyzer
{
    /// <summary>
    /// 単一のターンの状態クラス
    /// </summary>
    class Turn
    {
        /// <summary>
        /// 現在のターン
        /// </summary>
        private int turn = 0;

        /// <summary>
        /// 現在のプレーモード
        /// </summary>
        private string playmode = null;
        
        /// <summary>
        /// プレーオンか
        /// </summary>
        //private bool playon = false;

        /// <summary>
        /// 左プレーヤーのリスト
        /// </summary>
        private List<Player> playerLeft = new List<Player>();

        /// <summary>
        /// 右プレーヤーのリスト
        /// </summary>
        private List<Player> playerRight = new List<Player>();

        /// <summary>
        /// ボールの状態
        /// </summary>
        private Ball ball = new Ball();

        /// <summary>
        /// ターン数をセット
        /// </summary>
        /// <param name="temp"></param>
        public void SetTurn(int temp)
        {
            turn = temp;
        }

        /// <summary>
        /// ターン数を返す
        /// </summary>
        /// <returns></returns>
        public int GetTurn()
        {
            return turn;
        }

        /// <summary>
        /// 左側のチームの選手を追加
        /// </summary>
        /// <param name="p"></param>
        public void AddLeftPlayer(Player p)
        {
            try
            {
                playerLeft.Add(p);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 右側のチームの選手を追加
        /// </summary>
        /// <param name="p"></param>
        public void AddRightPlayer(Player p)
        {
            try
            {
                playerRight.Add(p);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 選手の状態を返す
        /// </summary>
        /// <param name="team">l または r</param>
        /// <param name="unum">選手 ID</param>
        /// <returns></returns>
        public Player GetPlayer(string team, int unum)
        {
            if (team == "l")
            {
                return playerLeft[unum];
            }
            else if (team == "r")
            {
                return playerRight[unum];
            }
            else
            {
                throw new FormatException();
            }
        }

        /// <summary>
        /// kick_count + catch_count を選手全員分返す
        /// </summary>
        /// <returns></returns>
        public Hashtable GetKickCount()
        {
            // return 用ハッシュデーブル
            Hashtable ht = new Hashtable();

            // 各チームのリスト
            ArrayList list = new ArrayList();

            try
            {
                // 左チーム
                foreach (var player in playerLeft)
                {
                    int i = 0;
                    i = int.Parse(player.Get("kick_count").ToString()) + int.Parse(player.Get("catch_count").ToString());
                    list.Add(i);
                }
                // 左チームのリストを hashtable として保存
                ht["left"] = list.Clone();

                // 右チーム
                list.Clear();
                foreach (var player in playerRight)
                {
                    int i = 0;
                    i = int.Parse(player.Get("kick_count").ToString()) + int.Parse(player.Get("catch_count").ToString());
                    list.Add(i);
                }
                // 右チームのリストを hashtable として保存
                ht["right"] = list.Clone();

                return ht;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ボールの状態を追加
        /// </summary>
        public void SetBall(Ball b)
        {
            try
            {
                ball = b;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ボールの状態を返す
        /// </summary>
        /// <returns></returns>
        public Ball GetBall()
        {
            return ball;
        }

        /// <summary>
        /// playmode の指定
        /// </summary>
        /// <param name="str">playmode</param>
        public void SetPlaymode(string str)
        {
            playmode = str;
        }

        /// <summary>
        /// playmode の取得
        /// </summary>
        /// <returns>playmode</returns>
        public string GetPlaymode()
        {
            return playmode;
        }

        /// <summary>
        /// このターンのすべての選手とボールの状態を返す
        /// </summary>
        /// <returns></returns>
        public Hashtable GetAll()
        {
            Hashtable ht = new Hashtable();
            ht["playerLeft"] = playerLeft;
            ht["playerRight"] = playerRight;
            ht["ball"] = ball;

            return ht;
        }

    }
}
