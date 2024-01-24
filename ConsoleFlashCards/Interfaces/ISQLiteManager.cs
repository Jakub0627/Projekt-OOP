using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleFlashCards
{
    public interface ISQLiteManager
    {
        SQLiteConnection CreateConnection();
        void CreateTable(SQLiteConnection conn);
        void InsertData(SQLiteConnection conn, string front, string back);
        void ReadData(SQLiteConnection conn);
    }
}
