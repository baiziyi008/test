using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class SqliteManager
    {
        static private SqliteManager _manager = null;
        static public SqliteManager GetManager() {
            if (_manager == null)
                _manager = new SqliteManager();
            return _manager;
        }

        private string _dbpath = string.Empty;
        private SQLiteConnection _conn = null;
        private SQLiteCommand _cmd = null;

        public void init(string dbpath) {
            _dbpath = dbpath;
        }
        public bool open() {
            bool bflag = false;
            try
            {
                _conn = new SQLiteConnection();
                SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
                connstr.DataSource = _dbpath;
                _conn.ConnectionString = connstr.ToString();
                _conn.Open();
                _cmd = new SQLiteCommand();
                _cmd.Connection = _conn;
                bflag = true;
            }
            catch (Exception) { bflag = false; }
            return bflag;
        }
        public void close() {
            try
            {
                _conn.Close();
                _conn.Dispose();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool excute_cmd(string sql)
        {
            bool bflag = true;
            _cmd.CommandText = sql;
            _cmd.ExecuteNonQuery();
            return bflag;
        }

        /// <summary>
        /// 读取记录，需手动关闭reader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SQLiteDataReader get_reader(string sql) {
            try
            {
                _cmd.CommandText = sql;
                return _cmd.ExecuteReader();
            }
            catch (Exception e) {
                e.ToString();
                return null;
            }
        }

        private Dictionary<string, string> get_table_dict(Object obj) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string typeName = obj.GetType().ToString();
            FieldInfo[] infos = obj.GetType().GetFields();
            if (infos.Length > 0) {
                foreach (FieldInfo info in infos) {
                    if(!dict.ContainsKey(info.Name))
                        dict.Add(info.Name, info.GetValue(obj).ToString());
                }
            }
            return dict;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool table_data_delete(Object obj) {
            Dictionary<string, string> dict = get_table_dict(obj);
            string sql = "delete from " + dict["table_name"];
            foreach (string key in dict.Keys)
            {
                if (key == "id")
                {
                    sql += " where id=" + dict[key];
                    return excute_cmd(sql);
                }
            }
            return false;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool table_data_insert(Object obj, ref string id) {
            Dictionary<string, string> dict = get_table_dict(obj);
            string sql = "insert into " + dict["table_name"];
            string fields = string.Empty;
            string values = string.Empty;
            foreach (string key in dict.Keys) {
                if (key != "table_name" && key != "id")
                {
                    fields += key + ",";
                    values += "\"" + dict[key] + "\",";
                }
            }
            fields = fields.TrimEnd(',');
            values = values.TrimEnd(',');
            sql = sql + " (" + fields + ") values(" + values + ")";
            bool flag  = excute_cmd(sql);
            if (flag) {
                flag = false;
                sql = "select last_insert_rowid()";
                _cmd.CommandText = sql;
                object result = _cmd.ExecuteScalar();
                if (result != null)
                {
                    id = result.ToString();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
