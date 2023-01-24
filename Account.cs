using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bank{

    class Account{

        private string? username, password, fullName, email;
        private int balance;

        public Account(string? u, string? p, string? f, string? e, int b = 0){

            Username = u;
            Password = p;
            FullName = f;
            Email = e;
            balance = b;

            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = '" + Username + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            var result = cmd.ExecuteScalar();

            if (Convert.ToInt32(result) == 0){

                string query2 = "insert into accounts(username, password, fullname, email, balance) values ('" + Username+ "','" + Password + "','" + FullName + "','" + Email + "','" + balance + "')";
                MySqlCommand cmd2 = new MySqlCommand(query2, conn);

                cmd2.ExecuteReader();

            }

            conn.Close();


        }

        public string? Username{

            get { return username; }

            set { username = value; }

        }

        public string? Password{

            get { return password; }

            set { password = value; }

        }

        public string? FullName{

            get { return fullName;}

            set { fullName = value; }

        }

        public string? Email{

            get { return email; }

            set { email = value; }

        }

        public int getBalance() { return balance; }

        public void withdraw(int amount) {

            bool success = false;

            if(amount >= 0){

                if(amount<=balance){

                    

                    try
                    {
                        balance -= amount;

                        string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                        MySqlConnection conn = new MySqlConnection(connstring);

                        conn.Open();

                        string query = "update accounts set balance='" + balance + "' where username='" + this.username + "'";

                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        reader.Close();
                        conn.Close();

                        success = true;

                    }catch(Exception e)
                    {
                        success = false;
                        balance += amount;

                        Console.WriteLine("\nWithdrew failed.");
                    }

                    if(success) Console.WriteLine("\nWithdrew successfully, your new balance is $" + balance );
                    
                  
                }

                else Console.WriteLine("Not enough money");
            }

            else Console.WriteLine("\nWithdraw failed, please try again and enter a positive amount");
        }

        public void add(int amount) {

            bool success = false;

            if (amount >= 0)
            {

                try
                {
                    balance += amount;

                    string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                    MySqlConnection conn = new MySqlConnection(connstring);

                    conn.Open();

                    string query = "update accounts set balance='" + balance + "' where username='" + this.username + "'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Close();
                    conn.Close();

                    success = true;

                }
                catch (Exception e)
                {
                    success = false;
                    balance -= amount;

                    Console.WriteLine("\nAdd failed.");
                }

                if(success) Console.WriteLine("\nAdded successfully, your new balance is $" + balance);
            }

            else Console.WriteLine("\nAdd failed, please try again and enter a positive amount");

        }

        public void transfer(Account? a, int amount) {

            bool success = false;

            if(amount >= 0){

                if(amount<=balance){

                    if (a != null)
                    {
                        try
                        {
                            this.balance -= amount;
                            a.balance += amount;

                            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                            MySqlConnection conn = new MySqlConnection(connstring);

                            conn.Open();

                            string query = "update accounts set balance='" + this.balance + "' where username='" + this.username + "'";
                            string query2 = "update accounts set balance='" + a.balance + "' where username='" + a.username + "'";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            MySqlCommand cmd2 = new MySqlCommand(query2, conn);

                            MySqlDataReader reader = cmd.ExecuteReader();
                            reader.Close();

                            MySqlDataReader reader2 = cmd2.ExecuteReader();
                            reader.Close();

                            conn.Close();

                            success = true;

                        }
                        catch(Exception e)
                        {
                            success = false;
                            this.balance += amount;
                            a.balance -= amount;

                            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                            MySqlConnection conn = new MySqlConnection(connstring);

                            conn.Open();

                            string query = "update accounts set balance='" + this.balance + "' where username='" + this.username + "'";
                            string query2 = "update accounts set balance='" + a.balance + "' where username='" + a.username + "'";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            MySqlCommand cmd2 = new MySqlCommand(query2, conn);

                            MySqlDataReader reader = cmd.ExecuteReader();
                            reader.Close();

                            MySqlDataReader reader2 = cmd2.ExecuteReader();
                            reader.Close();

                            conn.Close();

                            Console.WriteLine("\nTranfer failed.");
                        }
                    }

                    else Console.WriteLine("\nTransfer failed");
                }

                else Console.WriteLine("Not enough money");
            }

            if(success) Console.WriteLine("\nTransfer successfully, your new balance is $" + this.balance);

        }
        
        public void changePassword(String? newPassword) {

            bool success = false;
            string? oldPassword = this.password;

            try
            {
                this.password = newPassword;

                string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                MySqlConnection conn = new MySqlConnection(connstring);

                conn.Open();

                string query = "update accounts set password='" + this.password + "' where username='" + this.username + "'";
                

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Close();
                conn.Close();

                success = true;

            }
            catch(Exception e)
            {
                success = false;
                this.password = oldPassword;

                Console.WriteLine("\nPassword change failed.");
            }

            if(success) Console.WriteLine("\nPassword changed successfully, please sign in again");
        }
        public String? toString() {

            return "\nName: " + fullName + "\nEmail: " + email + "\nPassword: " + password ;  
        }

    }
}