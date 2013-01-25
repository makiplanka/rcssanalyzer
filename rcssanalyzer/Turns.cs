using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rcssanalyzer
{
    class Turns
    {
        /// <summary>
        /// 各ターンのデータ
        /// </summary>
        private IDictionary<int, Turn> turns = new Dictionary<int, Turn>();

        /// <summary>
        /// 左チーム名
        /// </summary>
        private String leftTeamName = null;
        /// <summary>
        /// 右チーム名
        /// </summary>
        private String rightTeamName = null;

        /// <summary>
        /// 構造解析済みのデータを分類する
        /// </summary>
        /// <param name="data">Hashtable 型の構造解析データ</param>
        public void AddParsed(List<Hashtable> data)
        {
            try
            {
                switch (data[0]["type"].ToString())
                {
                    case "ball":
                        AddParsedTurn(data);
                        break;
                    case "playmode":
                        // ターン数を取得して playmode に挿入
                        // playmode は同じターンなら始めに宣言される

                        int nowTurn = int.Parse(data[0]["turn"].ToString());

                        Turn turn = new Turn();
                        turn.SetTurn(nowTurn);
                        turn.SetPlaymode(data[0]["info"].ToString());

                        // 指定ターンに挿入
                        turns[nowTurn] = turn;
                        break;
                    case "team":
                        leftTeamName = data[0]["leftTeamName"].ToString();
                        rightTeamName = data[0]["rightTeamName"].ToString();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ターンを追加する
        /// </summary>
        /// <param name="data">Hashtable 型の構造解析データ</param>
        private void AddParsedTurn(List<Hashtable> data)
        {
            try
            {
                // 現在のターン
                int nowTurn = int.Parse(data[0]["turn"].ToString());

                Turn turn = new Turn();
                Ball ball = new Ball();

                // 既に指定されたターン数があれば playmode のみ引き継ぎ
                if (GetExistTurn(nowTurn))
                {
                    // 既にあるターンを取得
                    Turn existTurn = GetTurn(nowTurn);

                    // playmode のみ引き継ぎ
                    turn.SetPlaymode(existTurn.GetPlaymode());
                }
                else
                    // 指定されていなければ前のターンから playmode を取得
                {
                    // 3000 ターンが無い場合などの対処
                    // 2999 → 3001
                    int i = 0;
                    while (true)
                    {
                        i++;
                        if (GetExistTurn(nowTurn - i))
                        {
                            break;
                        }
                    }
                    turn.SetPlaymode(turns[nowTurn - i].GetPlaymode());
                }

                // 現在のターンをセット
                turn.SetTurn(nowTurn);

                // ボールの状態を格納
                ball = ConvertBall(data[0]);
                turn.SetBall(ball);

                // 先頭にあるボールの配列を削除
                data.RemoveAt(0);

                // player データを格納
                foreach (var ht in data)
                {
                    Player player = new Player();
                    player = ConvertPlayer(ht);

                    // チームに分別
                    switch (ht["team"].ToString())
                    {
                        case "l":
                            turn.AddLeftPlayer(player);
                            break;
                        case "r":
                            turn.AddRightPlayer(player);
                            break;
                        default:
                            throw new FormatException();
                    }
                }

                turns[turn.GetTurn()] = turn;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 全ターン数を取得
        /// </summary>
        /// <returns>Turns に格納されているターン数を返す</returns>
        public int GetTurnCount()
        {
            return turns.Count;
        }

        /// <summary>
        /// 指定したターンを取得
        /// </summary>
        /// <param name="turn">Turns に格納されているターン数</param>
        /// <returns>指定されたターンの Turn 型を返す</returns>
        public Turn GetTurn(int turn)
        {
            try
            {
                return turns[turn];
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 指定したターンが存在しているかを返す
        /// </summary>
        /// <param name="turn"></param>
        /// <returns></returns>
        public bool GetExistTurn(int turn)
        {
            return turns.ContainsKey(turn);
        }
            

        /// <summary>
        /// parsed Hashtable のデータを Ball 型へ変換
        /// </summary>
        /// <param name="data">Hashtable に保存された Ball データ</param>
        /// <returns>Ball 型</returns>
        private Ball ConvertBall(Hashtable data)
        {
            try
            {
                Ball ball = new Ball();
                ball.Set(data);
                return ball;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// parsed Hashtable のデータを Player 型へ変換
        /// </summary>
        /// <param name="data">Hashtable に保存された Player データ</param>
        /// <returns>Player 型</returns>
        private Player ConvertPlayer(Hashtable data)
        {
            try
            {
                Player player = new Player();
                player.Set(data);
                return player;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// このターンでキックしたチームを返す
        /// </summary>
        /// <param name="prevTurn">前のターン</param>
        /// <param name="thisTurn">現在のターン</param>
        /// <returns>キックしたチーム (l or r)</returns>
        public string GetKickerThisTurn(int pTurn, int tTurn)
        {
            Hashtable prevTurn = GetTurn(pTurn).GetKickCount();
            Hashtable thisTurn = GetTurn(tTurn).GetKickCount();

            ArrayList thisTurnLeft = (ArrayList)thisTurn["left"];
            ArrayList thisTurnRight = (ArrayList)thisTurn["right"];
            ArrayList prevTurnLeft = (ArrayList)prevTurn["left"];
            ArrayList prevTurnRight = (ArrayList)prevTurn["right"];

            // kick_count が増えた選手がヒットするまで繰り返す
            for (int i = 0; i < thisTurnLeft.Count; i++)
            {
                // 左チームに該当する選手がいるか探す
                if (prevTurnLeft[i].ToString() != thisTurnLeft[i].ToString())
                {
                    return "l";
                }
                // 右チームに該当する選手がいるか探す
                else if (prevTurnRight[i].ToString() != thisTurnRight[i].ToString())
                {
                    return "r";
                }
            }

            return "n";
        }

        /// <summary>
        /// チーム名を取得する
        /// </summary>
        /// <param name="team">r または l</param>
        /// <returns>チーム名</returns>
        public string GetTeamName(string team)
        {
            switch (team)
            {
                case "l":
                    return leftTeamName;
                case "r":
                    return rightTeamName;
                default:
                    throw new FormatException("l または r 以外の文字が渡されました: " + team);
            }
        }
    }
}
