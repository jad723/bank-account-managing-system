using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank{

    class Account{

        private string? username, password, fullName, email;
        private int balance;
        private int counter;

        public Account(string? u, string? p, string? f, string? e){

            Username = u;
            Password = p;
            FullName = f;
            Email = e;
            balance = 0;;
            counter = 0;

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

        public int getCounter() { return counter; }

        public void withdraw(int amount) { 
            if(amount >= 0){
                if(amount<=balance){
                    if(counter < 10){
                    
                        balance -= amount;
                        counter++;
                        Console.WriteLine("\nWithdrew successfully, your new balance is $" + balance + " you have " + (10-counter) + " withdraws left for today");
                    }
                    else if(counter == 10){
                        Console.WriteLine("\nYou have reached the maximum amount of withdrawal for today.");
                    }
                }
            

                else Console.WriteLine("Not enough money");
            }

            else Console.WriteLine("\nWithdraw failed, please try again and enter a positive amount");
        }

        public void add(int amount) {
            
            if(amount >= 0) balance += amount;

            else Console.WriteLine("\nAdd failed, please try again and enter a positive amount");

        }

        public void transfer(Account? a, int amount) {
            if(amount >= 0){
                if(amount<=balance){
                    this.balance -= amount;

                    if(a !=null) a.balance += amount;

                    else Console.WriteLine("\nTransfer failed");
                }

                else Console.WriteLine("Not enough money");
            }

            else Console.WriteLine("\nTransfer failed, please try again and enter a positive amount");

        }
        
        public void changePassword(String? newPassword) {

           this.password = newPassword;
        }
        public String? toString() {

            return "\nName: " + fullName + "\nEmail: " + email + "\nPassword: " + password + "\n\nYou did " + counter + " withdraws today you have " + (10 - counter) + " left";  
        }

    }
}