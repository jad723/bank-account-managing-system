using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

namespace Bank{

    class Program{

        static void Main(string[] args){

            int choice;
            String? username, password, email, reEnteredPassword;   

            home_menu: do{

                HomeMenu();

                Console.Write("\nEnter the number of the option you want: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice){

                    case 0: 
                
                        Console.WriteLine("\nGoodbye!");
                        
                        break;

                    case 1:

                        Console.Write("\nEnter your username or \"cancel\" to cancel: ");
                        username = Console.ReadLine();


                        if (!username!.Equals("cancel", StringComparison.OrdinalIgnoreCase)) { 
                           
                            Console.Write("\nEnter your password  or \"cancel\" to cancel: ");
                            password = Console.ReadLine();

                            if (!password!.Equals("cancel", StringComparison.OrdinalIgnoreCase)) {

                                if (RightLogin(username, password) != null)
                                {

                                main: do
                                    {

                                        Account? account = RightLogin(username, password);

                                        MainMenu(account?.FullName);

                                        Console.Write("\nEnter the number of the option you want: ");
                                        choice = Convert.ToInt32(Console.ReadLine());


                                        switch (choice)
                                        {

                                            case 0:

                                                Console.WriteLine("\nGoodbye!");

                                                break;

                                            case 1: continue;

                                            case 2:

                                                Console.WriteLine(account?.toString());

                                                break;

                                            case 3:

                                                Console.WriteLine("\nYou have $" + account?.getBalance() + " in your account");

                                                break;

                                            case 4:

                                                Console.WriteLine("\nYou have have $" + account?.getBalance() + " in your account");
                                                Console.Write("\nEnter the amount you want to withdraw or enter 0 to cancel: ");
                                                int amountToWithdraw = Convert.ToInt32(Console.ReadLine());

                                                if (amountToWithdraw != 0)
                                                {
                                                    Console.Write("\nAre you sure you want to continue: ");
                                                    String? answer = Console.ReadLine();

                                                    if (answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)) account?.withdraw(amountToWithdraw);

                                                }
                                                break;

                                            case 5:

                                                Console.Write("\nEnter the amount you want to add or enter 0 to cancel: ");
                                                int amountToAdd = Convert.ToInt32(Console.ReadLine());

                                                if (amountToAdd != 0)
                                                {
                                                    Console.Write("\nAre you sure you want to continue: ");
                                                    String? answer = Console.ReadLine();

                                                    if (answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)) account?.add(amountToAdd);

                                                }
                                                break;

                                            case 6:

                                                String? target;

                                                do
                                                {

                                                    Console.Write("\nEnter the username of the account you want to transfer to or type \"cancel\" to cancel: ");
                                                    target = Console.ReadLine();

                                                    if (!target!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                                    {


                                                        if (Exists(target) != null)
                                                        {

                                                            Account? targetAccount = Exists(target);

                                                            Console.WriteLine("\nYou have have $" + account?.getBalance() + " in your account");

                                                            Console.Write("\nEnter the amount you want to transfer or enter 0 to cancel: ");
                                                            int amountToTransfer = Convert.ToInt32(Console.ReadLine());

                                                            if (amountToTransfer != 0)
                                                            {

                                                                Console.Write("\nAre you sure you want to continue: ");
                                                                String? answer = Console.ReadLine();

                                                                if (answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)) account?.transfer(targetAccount, amountToTransfer);

                                                            }
                                                        }

                                                        else Console.WriteLine("\nThe account does not exist, please try again");
                                                    }

                                                } while (Exists(target) == null && !target!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                                break;

                                            case 7:

                                                String? newPassword;
                                                String? oldPassword;

                                                do
                                                {
                                                    Console.Write("\nEnter your current password or \"cancel\" to cancel: ");
                                                    oldPassword = Console.ReadLine();

                                                    if (!oldPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                                    {

                                                        if (!account!.Password!.Equals(oldPassword)) Console.WriteLine("\nPassword does not match, please try again");

                                                    }
                                                } while (!account!.Password!.Equals(oldPassword) && !oldPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                                if (!oldPassword.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                                {


                                                    Console.Write("\nEnter your new password or \"cancel\" to cancel: ");
                                                    newPassword = Console.ReadLine();

                                                    if (!newPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                                    {

                                                        do
                                                        {

                                                            Console.Write("\nEnter your new password again or \"cancel\" to cancel: ");
                                                            reEnteredPassword = Console.ReadLine();

                                                            if (!reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                                            {

                                                                if (!newPassword!.Equals(reEnteredPassword)) Console.WriteLine("\nThe passwords doesn't match, please enter the password again.");

                                                            }


                                                        } while (!newPassword!.Equals(reEnteredPassword) && !reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                                        account.changePassword(newPassword);

                                                        goto home_menu;
                                                    }

                                                }

                                                goto main;

                                            case 8:

                                                Console.Write("\nAre you sure you want to delete your account: ");
                                                String? input = Console.ReadLine();

                                                if (input!.Equals("yes", StringComparison.OrdinalIgnoreCase))
                                                {

                                                    if (DeleteAccount(account!.Username))
                                                    {

                                                        Console.WriteLine("\nYour account has been deleted succesfully");

                                                        goto home_menu;

                                                    }

                                                    else Console.WriteLine("\nAccount deletion failed");

                                                }

                                                break;

                                            default:

                                                Console.WriteLine("\nInvalid Input");
                                                break;
                                        }

                                    } while (choice != 0 && choice != 1);
                                }

                                else Console.WriteLine("\nWrong username or password.");


                            }

                        }

                        break;

                    case 2:

                        String? newAccountPassword;
                        String? newUsername;


                        Console.Write("\nEnter your full name or \"cancel\" to cancel: ");
                        String? name = Console.ReadLine();

                        if(!name!.Equals("cancel", StringComparison.OrdinalIgnoreCase)) { 

                            do{

                                Console.Write("\nEnter your email or \"cancel\" to cancel: ");
                                email = Console.ReadLine();

                                if(EmailExists(email)) Console.WriteLine("\nAn account with that email already exists");

                            }while(EmailExists(email) && !email!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                            if (!email!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                            {

                                do
                                {

                                    Console.Write("\nEnter a username or \"cancel\" to cancel: ");
                                    newUsername = Console.ReadLine();

                                    if (!newUsername!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                    {

                                        if (UsernameExists(newUsername)) Console.WriteLine("\nAn account with that username already");

                                    }

                                } while (UsernameExists(newUsername) && !newUsername.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                if (!newUsername!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                {

                                    Console.Write("\nEnter a password or \"cancel\" to cancel: ");
                                    newAccountPassword = Console.ReadLine();

                                    if (!newAccountPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                    {

                                        do
                                        {

                                            Console.Write("\nEnter your password again or \"cancel\" to cancel: ");
                                            reEnteredPassword = Console.ReadLine();

                                            if (!reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                            {

                                                if (!newAccountPassword!.Equals(reEnteredPassword)) Console.WriteLine("\nThe passwords doesn't match, please enter the password again.");

                                            }

                                        } while (!newAccountPassword!.Equals(reEnteredPassword) && !reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                        if (!reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                                        {

                                            new Account(newUsername, newAccountPassword, name, email);

                                            Console.WriteLine("\nAccount created successfully");
                                        }
                                    }
                                }

                            }
                        }

                        break;

                    default: 

                        Console.WriteLine("\nInvalid input");
                        break;
                }

            }while(choice != 0);


        }

        public static void HomeMenu(){

            Console.WriteLine("\n\t\tWelcome, please choose what do you want to do");
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("1. Sign in");
            Console.WriteLine("2. Sign up");

        }

        public static Account? RightLogin(String? u, String? p){

            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = '" + u + "' and password = '" + p + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

             var result = cmd.ExecuteScalar();

            if (Convert.ToInt32(result) > 0)
            {

                string? username = "";
                string? password = "";
                string? fullname = "";
                string? email = "";
                int balance = 0;
                string query2 = "SELECT * FROM accounts WHERE username = '" + u + "'";

                MySqlCommand cmd2 = new MySqlCommand(query2, conn);

                MySqlDataReader reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
                    username = Convert.ToString(reader["username"]);
                    password = Convert.ToString(reader["password"]);
                    fullname = Convert.ToString(reader["fullname"]);
                    email = Convert.ToString(reader["email"]);
                    balance = Convert.ToInt32(reader["balance"]);

                }

                reader.Close();

                Account account = new Account(username, password, fullname, email, balance);
                conn.Close();

                return account;

            }

            conn.Close();

            return null;

        }

        public static void MainMenu(String? f){

            Console.WriteLine("\n\t\tWelcome back " + f);
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("1. Sign out");
            Console.WriteLine("2. View Account Details");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Add");
            Console.WriteLine("6. Make a Transfer");
            Console.WriteLine("7. Change Password");
            Console.WriteLine("8. Delete Account");

        }

        public static Account? Exists(String? u){

            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = '" + u + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            var result = cmd.ExecuteScalar();

            if (Convert.ToInt32(result) > 0)
            {

                string? username = "";
                string? password = "";
                string? fullname = "";
                string? email = "";
                int balance = 0;
                string query2 = "SELECT * FROM accounts WHERE username = '" + u + "'";

                MySqlCommand cmd2 = new MySqlCommand(query2, conn);

                MySqlDataReader reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
                    username = Convert.ToString(reader["username"]);
                    password = Convert.ToString(reader["password"]);
                    fullname = Convert.ToString(reader["fullname"]);
                    email = Convert.ToString(reader["email"]);
                    balance = Convert.ToInt32(reader["balance"]);

                }

                reader.Close();

                Account account = new Account(username, password, fullname, email, balance);
                conn.Close();

                return account;

            }

            conn.Close();

            return null;
        } 

        public static bool UsernameExists(String? u){

            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = '" + u + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            var result = cmd.ExecuteScalar();

            if (Convert.ToInt32(result) > 0)
            {
                conn.Close();
                return true;
            }

            conn.Close();

            return false;
        }

        public static bool EmailExists(String? e){

            string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

            MySqlConnection conn = new MySqlConnection(connstring);

            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE email = '" + e + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            var result = cmd.ExecuteScalar();

            if (Convert.ToInt32(result) > 0)
            {
                conn.Close();
                return true;
            }

            conn.Close();

            return false;
            
        }

        public static bool DeleteAccount(string? u)
        {
            bool success = false;

            try
            {
                string connstring = "SERVER=localhost; DATABASE=bank; UID=root; PASSWORD=;";

                MySqlConnection conn = new MySqlConnection(connstring);

                conn.Open();

                string query = "DELETE FROM accounts WHERE username = '" + u + "'";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();

                success = true;

            }catch(Exception e)
            {
                Console.WriteLine(e);
                success = false;
            }

            return success;

        }
    }
}