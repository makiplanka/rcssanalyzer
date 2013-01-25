using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rcssanalyzer
{
    /*
    class Database
    {
        private List<Hashtable> inputData = new List<Hashtable>();

        public void set(ref List<Hashtable> data)
        {
            inputData = data;
        }

        public void execute()
        {
            try
            {
                // INSERT するデータの種類
                switch (inputData[0]["type"].ToString())
                {
                    case "ball":
                        addBall();
                        addShow();
                        break;
                    case "playmode":
                        addPlaymode();
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
        /// ball テーブルに insert
        /// </summary>
        private void addBall()
        {
            SQLiteConnector.Instance.ExecuteInsert(
                "insert into ball(turn, x, y, vx, vy) values(?,?,?,?,?)",
                inputData[0]["turn"].ToString(),
                inputData[0]["x"].ToString(),
                inputData[0]["y"].ToString(),
                inputData[0]["vx"].ToString(),
                inputData[0]["vy"].ToString()
            );
            // 挿入後削除
            inputData.RemoveAt(0);
        }

        /// <summary>
        /// show テーブルに insert
        /// ball を先に実行すること
        /// </summary>
        private void addShow()
        {
            try
            {
                // 選手分繰り返し
                foreach (var ht in inputData)
                {
                    SQLiteConnector.Instance.ExecuteInsert(
                        "insert into show(turn, team, unum, x, y, vx, vy, body, neck, view_quality, view_width, stamina, stamina_effort, stamina_recovery," +
                        "stamina_capacity, kick_count, dash_count, turn_count, catch_count, move_count, turn_neck_count, change_view_count, say_count," +
                        "tackle_count, pointto_count, attentionto_count) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)",
                        ht["turn"].ToString(),
                        ht["team"].ToString(),
                        ht["unum"].ToString(),
                        ht["x"].ToString(),
                        ht["y"].ToString(),
                        ht["vx"].ToString(),
                        ht["vy"].ToString(),
                        ht["body"].ToString(),
                        ht["neck"].ToString(),
                        ht["view_quality"].ToString(),
                        ht["view_width"].ToString(),
                        ht["stamina"].ToString(),
                        ht["stamina_effort"].ToString(),
                        ht["stamina_recovery"].ToString(),
                        ht["stamina_capacity"].ToString(),
                        ht["kick_count"].ToString(),
                        ht["dash_count"].ToString(),
                        ht["turn_count"].ToString(),
                        ht["catch_count"].ToString(),
                        ht["move_count"].ToString(),
                        ht["turn_neck_count"].ToString(),
                        ht["change_view_count"].ToString(),
                        ht["say_count"].ToString(),
                        ht["tackle_count"].ToString(),
                        ht["pointto_count"].ToString(),
                        ht["attentionto_count"].ToString()
                        );
                }
            }
            catch
            {
                throw;
            }


        }


        /// <summary>
        /// playmode テーブルに insert
        /// </summary>
        private void addPlaymode()
        {
            try
            {
                SQLiteConnector.Instance.ExecuteInsert(
                    "insert into playmode(turn, info, team) values(?,?,?)",
                    inputData[0]["turn"].ToString(),
                    inputData[0]["info"].ToString(),
                    inputData[0]["team"].ToString()
                    );
            }
            catch
            {
                throw;
            }
        }
    }
    */
}