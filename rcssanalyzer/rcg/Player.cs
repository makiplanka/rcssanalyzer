using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rcssanalyzer
{
    /// <summary>
    /// プレイヤークラス
    /// </summary>
    class Player
    {
        /// <summary>
        /// 背番号
        /// </summary>
        private int unum = 0;

        /// <summary>
        /// プレーヤーのパラメータ
        /// </summary>
        Hashtable parameterData = new Hashtable();

        /// <summary>
        /// 状態をセットする
        /// </summary>
        /// <param name="ht"></param>
        public void Set(Hashtable data)
        {
            try
            {
                Validate(data);

                // 必要なデータのみ保存
                // 必要に応じて追加する
                unum = int.Parse(data["unum"].ToString());
                parameterData["kick_count"] = data["kick_count"].ToString();
                parameterData["catch_count"] = data["catch_count"].ToString();
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// 指定したキーの値を返す
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>存在しない場合は FormatException を返す</returns>
        public Object Get(string key)
        {
            try
            {
                // キーがあれば
                if (parameterData.ContainsKey(key))
                {
                    return parameterData[key];
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// パラメータをすべて返す
        /// </summary>
        /// <returns>すべてのパラメータ</returns>
        public Hashtable GetAll()
        {
            return parameterData;
        }

        /// <summary>
        /// バリデーション
        /// </summary>
        /// <param name="d"></param>
        private void Validate(Hashtable d)
        {
            // キーの値が保存されているか確認する
            if (d.ContainsKey("unum") &&
                d.ContainsKey("x") &&
                d.ContainsKey("y") &&
                d.ContainsKey("vx") &&
                d.ContainsKey("vy") &&
                d.ContainsKey("kick_count") &&
                d.ContainsKey("catch_count"))
            {
                return;
            }
            else
            {
                throw new FormatException("必要な player クラスの値がありません。");
            }
        }

    }
}
