using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace rcssanalyzer
{
    class Ball
    {
        private Hashtable parametarData = new Hashtable();

        /// <summary>
        /// 状態をセットする
        /// </summary>
        /// <param name="data"></param>
        public void Set(Hashtable data)
        {
            try
            {
                // バリデーション
                validate(data);

                // 保存
                parametarData["x"] = data["x"];
                parametarData["y"] = data["y"];
                parametarData["vx"] = data["vx"];
                parametarData["vy"] = data["vy"];
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 状態を返す
        /// </summary>
        /// <returns></returns>
        public Hashtable Get()
        {
            return parametarData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void validate(Hashtable data)
        {
            if (data.ContainsKey("x") && data.ContainsKey("y") &&
                data.ContainsKey("vx") && data.ContainsKey("vy"))
            {
                return;
            }
            else
            {
                throw new FormatException("ball クラスの引数が正しくありません。");
            }
        }
    }
}
