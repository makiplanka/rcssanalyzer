using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace rcssanalyzer
{
    /// <summary>
    /// show 構文解析
    /// </summary>
    class ParserShow : Parser
    {
        public new void Execute()
        {
            int loopedCount = 1;
            int turn = 0;

            try
            {

                // マッチした回数繰り返し
                while (loopedCount < message.Count)
                {

                    // ターン数取得
                    if (loopedCount == 1)
                    {
                        turn = int.Parse(message[loopedCount].ToString());
                    }
                    // ボール
                    // x y vx vy
                    else if (message[loopedCount].ToString() == "((b)")
                    {
                        Hashtable ht = new Hashtable();
                        ht["type"] = "ball";
                        ht["turn"] = turn;
                        ht["x"] = message[loopedCount + 1].ToString();
                        ht["y"] = message[loopedCount + 2].ToString();
                        ht["vx"] = message[loopedCount + 3].ToString();
                        ht["vy"] = message[loopedCount + 4].ToString().Trim(')');
                        SetResult(ht);

                        loopedCount += 4;

                    }
                    // 選手
                    // players
                    // ((side unum) type state x y vx vy body neck [pointx pointy] (v h 90) (s 4000 1 1 127000)[(f side unum)])
                    //              (c 1 1 1 1 1 1 1 1 1 1 1))
                    else if (message[loopedCount].ToString() == "((l" || message[loopedCount].ToString() == "((r")
                    {
                        Hashtable ht = new Hashtable();
                        ht["type"] = "player";
                        ht["turn"] = turn;
                        ht["team"] = message[loopedCount].ToString().Trim('(');
                        ht["unum"] = message[loopedCount + 1].ToString().Trim(')');
                        ht["x"] = message[loopedCount + 4].ToString();
                        ht["y"] = message[loopedCount + 5].ToString();
                        ht["vx"] = message[loopedCount + 6].ToString();
                        ht["vy"] = message[loopedCount + 7].ToString();
                        ht["body"] = message[loopedCount + 8].ToString();
                        ht["neck"] = message[loopedCount + 9].ToString();
                        // skip point
                        int p;
                        for (p = 10; message[loopedCount + p].ToString().Trim('(') != "v"; p++) ;
                        p++; // skip v
                        ht["view_quality"] = message[loopedCount + p++].ToString();
                        ht["view_width"] = message[loopedCount + p++].ToString().Trim(')');
                        p++; // skip s
                        ht["stamina"] = message[loopedCount + p++].ToString();
                        ht["stamina_effort"] = message[loopedCount + p++].ToString();
                        ht["stamina_recovery"] = message[loopedCount + p++].ToString();
                        ht["stamina_capacity"] = message[loopedCount + p++].ToString().Trim(')');

                        // skip f
                        int c;
                        for (c = 18; message[loopedCount + c].ToString().Trim('(') != "c"; c++) ;

                        // (c kick dash turn catch move tneck cview say tackle pointto atttention)
                        c++;    // skip c
                        ht["kick_count"] = message[loopedCount + c].ToString();
                        ht["dash_count"] = message[loopedCount + c + 1].ToString();
                        ht["turn_count"] = message[loopedCount + c + 2].ToString();
                        ht["catch_count"] = message[loopedCount + c + 3].ToString();
                        ht["move_count"] = message[loopedCount + c + 4].ToString();
                        ht["turn_neck_count"] = message[loopedCount + c + 5].ToString();
                        ht["change_view_count"] = message[loopedCount + c + 6].ToString();
                        ht["say_count"] = message[loopedCount + c + 7].ToString();
                        ht["tackle_count"] = message[loopedCount + c + 8].ToString();
                        ht["pointto_count"] = message[loopedCount + c + 9].ToString();
                        ht["attentionto_count"] = message[loopedCount + c + 10].ToString();

                        // 結果をセット
                        SetResult(ht);

                        loopedCount += 29;

                    }

                    loopedCount++;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
