using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerConsole {
	class Task {
		public static int counter = 0;
		int id,
		category;
		string task;
		bool success;
		public DateTime time;
		private Database db = new Database();

		public void AddTask(int id, string task, bool success, int category) {
			counter = id;
			this.id = counter;
			this.task = task;
			this.success = success;
			this.category = category;

		}

		public void AddTask(int id, string task, bool success, int category, DateTime time) {
			counter = id;
			this.id = counter;
			this.task = task;
			this.success = success;
			this.category = category;
			this.time = time;

		}
		public Task(string task, int category) {
			counter++;
			this.id = counter;
			this.task = task;
			this.success = false;
			this.category = category;
			db.addTask(counter, task, category);

		}

		public Task(DateTime time, string task, int category) {
			counter++;
			this.id = counter;
			this.time = time;
			this.task = task;
			this.success = false;
			this.category = category;

			if (category == 2) {
				db.addFreeTask(counter, task, category, Convert.ToString(time));
			} else if (category == 3) {
				db.addTimeTask(id, task, category, Convert.ToString(time));
			}
		}

		public Task() {}

		public int getID() {
			return id;

		}

        public int getCategory()
        {
            return category;

        }

        public bool getSuccess() {
			return success;

		}

		public void setSuccess(bool a) {
			this.success = a;

		}

		public void Write(int category) {
			if (this.category == category) {
				Console.WriteLine("("+Convert.ToString(this.id)+")" + "  " + this.task);
			}
		}

		public void Write(DateTime time) {
			if (this.time.ToShortDateString() == time.ToShortDateString() && this.category == 2) {
				Console.WriteLine("("+Convert.ToString(this.id)+")" + "  " + this.task);

			}
		}

		public void Write(DateTime time, int category) {
			if (this.time.ToShortDateString() == time.ToShortDateString() && this.category == category) {
				Console.WriteLine("("+Convert.ToString(this.id)+")" + "  " + this.time.ToShortTimeString() + "  " + this.task);

			}
		}

		public void editTime(DateTime time) {

			this.time = time;

		}
		public void editTask(String task) {

			this.task = task;
		}

	}
}