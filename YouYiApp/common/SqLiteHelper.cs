using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using YouYiApp.config;
using YouYiApp.model;
using YouYiApp.model.view;

namespace YouYiApp.common
{
    public class SQLiteHelper
    {

        public static SQLiteConnection db;

        private static object singleton_Lock = new object(); //锁同步
        public SQLiteHelper()
        {
            lock (singleton_Lock)
            {
                if (null == db)
                {
                    JichuViewModel jichuViewModel = JichuViewModel.GetJichuViewModel();
                    string path = BaseConfig.DATA_PATH_50;
                    if (!jichuViewModel.LinMin)
                    {
                        path = BaseConfig.DATA_PATH_30;
                    }
                    var options = new SQLiteConnectionString(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), true,
                    key: BaseConfig.DATA_PATH_SECRET);
                    db = new SQLiteConnection(options);
                }
            }
            //db.CreateTable<GunsModel>();//表已存在不会重复创建
        }

        public int Add<T>(T model)
        {
            return db.Insert(model);
        }

        public int Update<T>(T model)
        {
            return db.Update(model);
        }

        public int Delete<T>(T model)
        {
            return db.Update(model);
        }
        public List<T> Query<T>(string sql) where T : new()
        {
            return db.Query<T>(sql);
        }
        public int Execute(string sql)
        {
            return db.Execute(sql);
        }

        public void Close()
        {
            db.Close();
        }

        public void GetTables()
        {
            var mapping = db.TableMappings;
            foreach (var a in mapping)
            {
                string name = a.TableName;
                LogHelper.ShowLog("tableName: {0}", name);
                foreach (var col in a.Columns)
                {
                    LogHelper.ShowLog("col {0}", col.Name);
                }
            }
        }
    }
}