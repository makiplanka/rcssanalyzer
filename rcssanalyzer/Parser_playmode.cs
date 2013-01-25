using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace rcssanalyzer
{
    /// <summary>
    /// playmode 構文解析
    /// </summary>
    class ParserPlaymode : Parser
    {
        public new void Execute()
        {
            Hashtable ht = new Hashtable();

            // ターン取得
            ht["turn"] = message[1].ToString();
            // 解析の種類
            ht["type"] = "playmode";

            // モード取得
            switch (message[2].ToString().Trim(')'))
            {
                case "kick_off_l":
                case "foul_charge_l":
                case "free_kick_l":
                case "kick_in_l":
                case "corner_kick_l":
                case "goal_kick_l":
                case "goal_l":
                    ht["info"] = message[2].ToString().Remove(message[2].Length - 3);
                    ht["team"] = "l";
                    break;
                case "kick_off_r":
                case "foul_charge_r":
                case "free_kick_r":
                case "kick_in_r":
                case "corner_kick_r":
                case "goal_kick_r":
                case "goal_r":
                    ht["info"] = message[2].ToString().Remove(message[2].Length - 3);
                    ht["team"] = "r";
                    break;
                default:
                    ht["info"] = message[2].ToString().Remove(message[2].Length - 1);
                    ht["team"] = "null";
                    break;
            }

            // 結果をセット
            SetResult(ht);

        }
    }
}
