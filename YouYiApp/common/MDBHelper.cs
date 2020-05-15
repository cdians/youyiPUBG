using System;
using System.Data;
using System.Data.OleDb;

namespace YouYiApp.common
{
    public class MDBHelp

    {

        private string _fileName;

        private string _connectionString;

        private OleDbConnection _odcConnection;

        /// <summary>

        /// 构建函数

        /// </summary>

        /// <param name="fileName">MDB文件（含完整路徑）</param>

        public MDBHelp(string fileName)

        {

            this._fileName = fileName;

            this._connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";";

        }

        /// <summary>

        /// 建立连接（打开数据库文件）

        /// </summary>

        public void Open()

        {

            try

            {

                // 建立连接

                this._odcConnection = new OleDbConnection(this._connectionString);

                // 打开连接

                this._odcConnection.Open();
            }

            catch (Exception)

            {

                throw new Exception("嘗試打开 " + this._fileName + " 失敗, 請確認文件是否存在！");

            }

        }

        /// <summary>

        /// 断开连接（关闭据库文件）

        /// </summary>

        public void Close()

        {

            this._odcConnection.Close();

        }

        /// <summary>

        /// 根据sql命令返回一个DataSet

        /// </summary>

        /// <param name="sql">sql命令</param>

        /// <returns>以DataTable形式返回数据</returns>

        public DataSet GetDataSet(string sql)

        {

            DataSet ds = new DataSet();

            try

            {

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, this._odcConnection);

                adapter.Fill(ds);

            }

            catch (Exception)

            {

                throw new Exception("sql語句： " + sql + " 執行失敗！");

            }

            return ds;

        }

    }

}
