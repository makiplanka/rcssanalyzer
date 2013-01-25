using System.Collections;
using System.Collections.Generic;
using System;

namespace rcssanalyzer
{
    class Analyze
    {
        /// <summary>
        /// 左チームの累計ボール支配ターン
        /// </summary>
        private int possessionLeft = 0;
        /// <summary>
        /// 右チームの累計ボール支配ターン
        /// </summary>
        private int possessionRight = 0;

        // 現在のボール支配権を持っているチーム
        private string nowBallControlTeam = null;

        /// <summary>
        /// 処理実行
        /// </summary>
        public void Execute()
        {
            try
            {
                // ボール支配
                if (nowBallControlTeam == "l")
                {
                    possessionLeft++;
                }
                else if (nowBallControlTeam == "r")
                {
                    possessionRight++;
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// ボールの支配チームを指定
        /// </summary>
        /// <param name="team"></param>
        public void SetControlTeam(string team)
        {
            try
            {
                if (team == "l" || team == "r")
                {
                    nowBallControlTeam = team;
                }
            }
            catch
            {
                throw new FormatException("l または r 以外の文字が渡されました: " + team);
            }
        }

        /// <summary>
        /// 左チームのボール支配率を百分率で得る
        /// </summary>
        /// <returns></returns>
        public float GetPossessionLeftParcent()
        {
            return (float)(possessionLeft) / (float)(possessionLeft + possessionRight);
        }

        /// <summary>
        /// 右チームのボール支配率を百分率で得る
        /// </summary>
        /// <returns></returns>
        public float GetPossessionRightParcent()
        {
            return (float)(possessionRight) / (float)(possessionLeft + possessionRight);
        }

    }
}
