using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ConsoleFlashCards.Models;

namespace ConsoleFlashCards
{
    public interface ISQLiteManager
    {
        SQLiteConnection CreateConnection();
        void CreateTable(SQLiteConnection conn);
        void InsertData(SQLiteConnection conn, string front, string back);
        void ReadData(SQLiteConnection conn);
        Flashcard GetRandomFlashcard(SQLiteConnection conn);
        void UpdateData(SQLiteConnection conn, int id, string front, string back);
        void DeleteData(SQLiteConnection conn, int id);
        void CloseConnection(SQLiteConnection conn);
    }
}
