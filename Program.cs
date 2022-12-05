using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Bank{

    class Program{

        static void Main(string[] args){

            int choice;
            String? username, password, email, reEnteredPassword;   
            Account a = new Account("jad", "064123", "Jad Maalouf", "jad@gmail.com");
            Account b = new Account("jana", "064123", "Jana Maalouf", "jana@gmail.com");
            List<Account> accounts = new List<Account>();

            accounts.Add(a);
            accounts.Add(b);

            home_menu: do{

                home_menu();

                Console.Write("\nEnter the number of the option you want: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice){

                    case 0: 
                
                        Console.WriteLine("\nGoodbye!");
                        
                        break;
                    case 1:

                        Console.Write("\nEnter your username: ");
                        username = Console.ReadLine();

                        Console.Write("\nEnter your password: ");
                        password = Console.ReadLine();

                        if(rightLogin(username, password, accounts) != null){
                            
                            main: do{
                                Account? account = rightLogin(username, password, accounts);
    
                                mainMenu(account?.FullName);

                                Console.Write("\nEnter the number of the option you want: ");
                                choice = Convert.ToInt32(Console.ReadLine());
    
                                
                                switch (choice){
    
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

                                        if(amountToWithdraw !=0 ){
                                            Console.Write("\nAre you sure you want to continue: ");
                                            String? answer = Console.ReadLine();

                                            if(answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)) account?.withdraw(amountToWithdraw);

                                        }
                                        break;
                                    case 5:
                                        
                                        Console.Write("\nEnter the amount you want to add or enter 0 to cancel: ");
                                        int amountToAdd = Convert.ToInt32(Console.ReadLine());

                                        if(amountToAdd !=0 ){
                                            Console.Write("\nAre you sure you want to continue: ");
                                            String? answer = Console.ReadLine();

                                            if(answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)){
                                                account?.add(amountToAdd);
                                                Console.WriteLine("\nAdded successfully, your new balance is $" + account?.getBalance());
                                            }
                                        }
                                        break;
                                    case 6:

                                        String? target;

                                        do{
                                            Console.Write("\nEnter the username of the account you want to transfer to or type \"cancel\" to cancel: ");
                                            target = Console.ReadLine();
    
                                            if(!target!.Equals("cancel", StringComparison.OrdinalIgnoreCase)){
                                                
                                                
                                                if(exists(target, accounts) != null){
            
                                                    Account? targetAccount = exists(target, accounts); 
        
                                                    Console.WriteLine("\nYou have have $" + account?.getBalance() + " in your account");
        
                                                    Console.Write("\nEnter the amount you want to transfer or enter 0 to cancel: ");
                                                    int amountToTransfer = Convert.ToInt32(Console.ReadLine());
        
                                                    if(amountToTransfer != 0 ){
        
                                                        Console.Write("\nAre you sure you want to continue: ");
                                                        String? answer = Console.ReadLine();
        
                                                        if(answer!.Equals("yes", StringComparison.OrdinalIgnoreCase)){
                                                            account?.transfer(targetAccount, amountToTransfer);
            
                                                            Console.WriteLine("\nTransfer successfully, your new balance is $" + account?.getBalance());
                                                        }
                                                    }
                                                }
            
                                                else Console.WriteLine("\nThe account does not exist, please try again");
                                            }
                                        }while(exists(target, accounts) == null && !target!.Equals("cancel", StringComparison.OrdinalIgnoreCase)); 

                                        break;
                                    case 7:

                                        String? newPassword;
                                        String? oldPassword;

                                        do{
                                            Console.Write("\nEnter your current password or \"cancel\" to cancel: ");
                                            oldPassword = Console.ReadLine();

                                            if(!oldPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase)){
                                                if(!account!.Password!.Equals(oldPassword)) Console.WriteLine("\nPassword does not match, please try again");
                                            }
                                        }while(!account!.Password!.Equals(oldPassword) && !oldPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase));

                                        if(!oldPassword.Equals("cancel", StringComparison.OrdinalIgnoreCase)){

                                            
                                            Console.Write("\nEnter your new password or \"cancel\" to cancel: ");
                                            newPassword = Console.ReadLine();

                                            if(!newPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase)){

                                                do{
        
                                                    Console.Write("\nEnter your new password again or \"cancel\" to cancel: ");
                                                    reEnteredPassword = Console.ReadLine();

                                                    if(!reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase)){

                                                        if(!newPassword!.Equals(reEnteredPassword)) Console.WriteLine("\nThe passwords doesn't match, please enter the password again."); 

                                                    }

        
                                                }while(!newPassword!.Equals(reEnteredPassword) && !reEnteredPassword!.Equals("cancel", StringComparison.OrdinalIgnoreCase));
        
                                                account.changePassword(newPassword);
            
                                                Console.WriteLine("\nPassword changed successfully, please sign in again");
        
                                                goto home_menu;
                                            }

                                        }

                                        goto main;
                                    default: 
                                        Console.WriteLine("\nInvalid Input");
                                        break;
                                }
                            }while(choice != 0 && choice != 1);

                        }

                        else Console.WriteLine("\nWrong username or password.");

                        break;

                    case 2:

                        String? newAccountPassword;
                        String? newUsername;


                        Console.Write("\nEnter your full name: ");
                        String? name = Console.ReadLine();

                        do{

                            Console.Write("\nEnter your email: ");
                            email = Console.ReadLine();

                            if(emailExists(email, accounts)) Console.WriteLine("\nAn account with that email already exists");

                        }while(emailExists(email, accounts));

                        do{

                            Console.Write("\nEnter a username: ");
                            newUsername = Console.ReadLine();

                            if(usernameExists(newUsername, accounts)) Console.WriteLine("\nAn account with that username already");

                        }while(usernameExists(newUsername, accounts));

                        Console.Write("\nEnter a password: ");
                        newAccountPassword = Console.ReadLine();
                        do{

                            Console.Write("\nEnter your password again: ");
                            reEnteredPassword = Console.ReadLine();
         
                            if(!newAccountPassword!.Equals(reEnteredPassword)) Console.WriteLine("\nThe passwords doesn't match, please enter the password again."); 

                        }while(!newAccountPassword!.Equals(reEnteredPassword));

                        accounts.Add(new Account(newUsername, newAccountPassword, name, email));

                        Console.WriteLine("\nAccount created successfully");

                        break;
                    default: 
                        Console.WriteLine("\nInvalid input");
                        break;
                }

            }while(choice != 0);


        }

        public static void home_menu(){
            Console.WriteLine("\n\t\tWelcome, please choose what do you want to do");
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("1. Sign in");
            Console.WriteLine("2. Sign up");
        }

        public static Account? rightLogin(String? u, String? p, List<Account> accounts){

            foreach(var account in accounts){
                
                
                if(account.Username != null){

                    if(account.Username.Equals(u, StringComparison.OrdinalIgnoreCase)){

                        if (account.Password != null){

                            if(account.Password.Equals(p)) return account;
                        
                        }

                    }
                }

            }

            return null;

        }

        public static void mainMenu(String? f){

            Console.WriteLine("\n\t\tWelcome back " + f);
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("1. Sign out");
            Console.WriteLine("2. View Account Details");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Add");
            Console.WriteLine("6. Make a Transfer");
            Console.WriteLine("7. Change Password");

        }

        public static Account? exists(String? u, List<Account> accounts){

            foreach(var account in accounts){

                if(account.Username != null){

                    if(account.Username.Equals(u, StringComparison.OrdinalIgnoreCase)) return account;
                }
            }

            return null;
        } 

        public static bool usernameExists(String? u, List<Account> accounts){

            foreach(var account in accounts){

                if(account.Username != null){

                    if(account.Username.Equals(u, StringComparison.OrdinalIgnoreCase)) return true;
                }
            }

            return false;
        }

        public static bool emailExists(String? e, List<Account> accounts){

            foreach(var account in accounts){

                if(account.Email != null){

                    if(account.Email.Equals(e, StringComparison.OrdinalIgnoreCase)) return true;
                }
            }

            return false;
            
        }
    }
}