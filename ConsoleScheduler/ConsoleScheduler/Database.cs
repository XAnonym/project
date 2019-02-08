using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PlannerConsole {
	class Database {
		static string connStr = "server=localhost;user=root;database=bdplanner;password=12345";

		MySqlConnection conn = new MySqlConnection(connStr);

		public void addTask(int id, string task, int category) {
			conn.Open();

			string sql = "INSERT INTO `bdplanner`.`tasks`(`id`,`task`,`success`,`category`)VALUES( " + Convert.ToString(id) + " , \"" + task + "\" ,b'0'," + category + " ); ";

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}
		public void addFreeTask(int id, string task, int category, string date) {
			conn.Open();

			string sql = "INSERT INTO `bdplanner`.`tasks`(`id`,`task`,`success`,`category`)VALUES( " + Convert.ToString(id) + " , \"" + task + "\" ,b'0'," + category + " ); ";

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();
			conn.Close();
			conn.Open();
			string[] strArr = date.Split('.', ' ');
			sql = "INSERT INTO `bdplanner`.`date`(`id`,date)VALUES( " + Convert.ToString(id) + " ,'" + strArr[2] + "-" + strArr[1] + "-" + strArr[0] + "' ); ";

			command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

		public void addTimeTask(int id, string task, int category, string date) {
			conn.Open();

			string sql = "INSERT INTO `bdplanner`.`tasks`(`id`,`task`,`success`,`category`)VALUES( " + Convert.ToString(id) + " , \"" + task + "\" ,b'0'," + category + " ); ";

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();
			conn.Close();
			conn.Open();
			string[] strArr = date.Split('.', ' ', ':');
			sql = "INSERT INTO `bdplanner`.`date`(`id`,date)VALUES( " + Convert.ToString(id) + " ,'" + strArr[2] + "-" + strArr[1] + "-" + strArr[0] + "' ); ";

			command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

			conn.Open();

			sql = "INSERT INTO `bdplanner`.`time`(`id`,time)VALUES( " + Convert.ToString(id) + " ,'" + strArr[3] + ":" + strArr[4] + ":" + strArr[5] + "' ); ";

			command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

		public void readTask() {
			int count = 0;
			conn.Open();

			string sql = " Select * From tasks where category = 1";

			MySqlCommand command = new MySqlCommand(sql, conn);

			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read()) {

				Program.tasks.Add(new Task());
				Program.tasks[count].AddTask(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToBoolean(reader[2]), Convert.ToInt32(reader[3]));
				count++;
			}

			conn.Close();
			conn.Open();
			sql = " Select tasks.id, tasks.task, tasks.success, tasks.category, date.date From tasks, date where tasks.category = 2 and date.id = tasks.id";
			command = new MySqlCommand(sql, conn);

			reader = command.ExecuteReader();

			while (reader.Read()) {
				string[] strArr = Convert.ToString(reader[4]).Split('.', ' ');
				DateTime time = new DateTime(Convert.ToInt32(strArr[2]), Convert.ToInt32(strArr[1]), Convert.ToInt32(strArr[0]));
				Program.tasks.Add(new Task());
				Program.tasks[count].AddTask(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToBoolean(reader[2]), Convert.ToInt32(reader[3]), time);
				count++;
			}

			conn.Close();
			conn.Open();
			sql = " Select tasks.id, tasks.task, tasks.success, tasks.category, date.date, time.time From tasks, date, time where tasks.category = 3 and date.id = tasks.id and tasks.id = time.id";
			command = new MySqlCommand(sql, conn);

			reader = command.ExecuteReader();

			while (reader.Read()) {
				string[] strArr = Convert.ToString(reader[4]).Split('.', ' ', ':');
				string[] strArr1 = Convert.ToString(reader[5]).Split('.', ' ', ':');
				DateTime time = new DateTime(Convert.ToInt32(strArr[2]), Convert.ToInt32(strArr[1]), Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr1[0]), Convert.ToInt32(strArr1[1]), Convert.ToInt32(strArr1[2]));
				Program.tasks.Add(new Task());
				Program.tasks[count].AddTask(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToBoolean(reader[2]), Convert.ToInt32(reader[3]), time);
				count++;
			}

			conn.Close();

			conn.Open();
			sql = "SELECT max(id) FROM bdplanner.tasks";
			command = new MySqlCommand(sql, conn);

			reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Task.counter = Convert.ToInt32(reader[0]);
                }
            }
            catch { }
			conn.Close();
		}

		public void delTask(int id) {
			conn.Open();

			string sql = "DELETE FROM `bdplanner`.`tasks` WHERE id = " + id;

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

		public void doneTask(int id) {
			conn.Open();

			string sql = "UPDATE `bdplanner`.`tasks` SET `success` = b'1' WHERE `id` = " + id;

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

		public void updTask(int id, string task) {
			conn.Open();

			string sql = "UPDATE `bdplanner`.`tasks` SET `task` = \"" + task + "\" WHERE `id` = " + id;

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

		public void updtTask(int id, string task) {

			conn.Open();

			string sql = "UPDATE `bdplanner`.`time` SET `time` = \"" + task + "\" WHERE `id` = " + id;

			MySqlCommand command = new MySqlCommand(sql, conn);

			command.ExecuteNonQuery();

			conn.Close();

		}

	}
}