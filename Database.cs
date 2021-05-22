using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Diagnostics;

namespace TowerDefenceEksamensProjekt
{
    public class Database
    {
        public static SQLiteConnection connection = new SQLiteConnection("Data Source=TowerDefence.db; Version=3; New=True");

        public static void DatabaseSetup()
        {
            connection.Open();
            SQLiteCommand command;
#if DEBUG
            command = new SQLiteCommand("DROP TABLE IF EXISTS User", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP TABLE IF EXISTS Highscore", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("DROP TABLE IF EXISTS Realm", connection);
            command.ExecuteNonQuery();
#endif
            connection.Close();
            if (!TabelCheck("User"))
            {
                connection.Open();

                command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS User (Username VARCHAR(18),Password VARCHAR(18), PRIMARY KEY (Username))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('ADMIN','ADMIN');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('HOFFE', 'HOFFE');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('KREIE','FCM');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('PEPEGA', 'REEEE');", connection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Highscore (" +
                    "Score INTEGER, " +
                    "realm VARCHAR(15)," +
                    "Username VARCHAR(15)," +
                    "FOREIGN KEY (Username) REFERENCES User(Username))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES (100,'So','PEPEGA')", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES (80,'So','KREIE')", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES (30,'So','Hoffe')", connection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Realm (Realms VARCHAR(18))", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Hav');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Kyst');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Flod');", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('So');", connection);
                command.ExecuteNonQuery();

                CreateTable("EnemyDB",
                "Name TEXT, " +
                "HP INTEGER," +
                "Lv INTEGER");

                InsertIntoTable("EnemyDB", "'Shadow',30,2");

                CreateTable("TowerDB", "TowerName VARCHAR(15)");
                InsertIntoTable("TowerDB", "'IceTower'");
                InsertIntoTable("TowerDB", "'Wall'");
                InsertIntoTable("TowerDB", "'Mine'");
                InsertIntoTable("TowerDB", "'BananaFarm'");
                InsertIntoTable("TowerDB", "'Stripclub'");

                DoesTableExist("EnemyDB");
            }

            connection.Close();
        }

        public static void DoesTableExist(string Table)
        {
            var cmd = new SQLiteCommand("SELECT * from " + Table + "", connection);
            var dataset = cmd.ExecuteReader();
            while (dataset.Read())
            {
                var id = dataset.GetString(0);
                Debug.WriteLine($"{id}");
            }
        }

        public static void CreateTable(string TableName, string Values)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=TowerDefence.db; Version=3; New=True");

            connection.Open();

            string querry = "CREATE TABLE IF NOT EXISTS " + TableName + " (" + Values + ")";

            var command = new SQLiteCommand(querry, connection);
            command.ExecuteNonQuery();
        }

        public static void InsertIntoTable(string TableName, string Values)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=TowerDefence.db; Version=3; New=True");

            connection.Open();

            string querry = "INSERT INTO " + TableName + " VALUES(" + Values + ")";

            var command = new SQLiteCommand(querry, connection);
            command.ExecuteNonQuery();
        }

        public static bool Userlogin(string user, string pass)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=TowerDefence.db; Version=3; New=True");

            connection.Open();
            string querry = "Select * from User Where Username = '" + user + "' and Password = '" + pass + "'";

            var command = new SQLiteCommand(querry, connection);
            var dataset = command.ExecuteReader();

            if (dataset.Read())
            {
                return true;
            }
            connection.Close();
            return false;
        }

        public static HighScore[] Loadhighscore()
        {
            connection.Open();
            var highscores = new List<HighScore>();
            var command = new SQLiteCommand("Select * from Highscore", connection);
            SQLiteDataReader result = command.ExecuteReader();
            while (result.Read())
            {
                var row = new HighScore();
                row.highscore = result.GetInt32(0);
                row.realm = result.GetString(1);
                row.user = result.GetString(2);
                highscores.Add(row);
            }

            connection.Close();
            return highscores.ToArray();
        }

        public static EnemyDB[] LoadEnemyDB()
        {
            connection.Open();
            var EnemyList = new List<EnemyDB>();
            var command = new SQLiteCommand("Select * from EnemyDB", connection);
            SQLiteDataReader result = command.ExecuteReader();
            while (result.Read())
            {
                var row = new EnemyDB();
                row.Name = result.GetString(0);
                row.HP = result.GetInt32(1);
                row.Lv = result.GetInt32(2);
                EnemyList.Add(row);
            }

            connection.Close();
            return EnemyList.ToArray();
        }

        public static bool TabelCheck(string user)
        {
            connection.Open();
            string querry = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + user + "'";

            var command = new SQLiteCommand(querry, connection);
            var dataset = command.ExecuteReader();

            if (dataset.Read())
            {
                return true;
            }
            connection.Close();
            return false;
        }

        //public static void InsertIntoHighscore(string user, string realm, string score)
        //{
        //    SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
        //    connection.Open();
        //    var command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES ('" + score + "','" + realm + "','" + user + "')", connection);
        //    command.ExecuteReader();
        //}
    }
}