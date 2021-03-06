using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    public class ConsoleService
    {
        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public int PromptForUserID(string action)
        {
            Console.WriteLine("");
            Console.Write("Enter ID of user you are " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return userId;
            }
        }

        public decimal PromptForAmount()
        {
            Console.Write("Enter amount:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return amount;
            }
        }

        public void DisplayUsers(List<UserInfo> users)
        {
            string header = "-------------------------------------------\n" +
                            "Users\n" +
                            "ID\t\tName\n" +
                            "-------------------------------------------";
            Console.WriteLine(header);
            foreach (UserInfo user in users)
            {
                Console.WriteLine($"{user.UserId}\t\t{user.Username}");
            }
            Console.WriteLine("---------");
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public void DisplayTransfers(List<API_Transfer> transfers, int currentUserId)
        {
            string header = "-------------------------------------------\n" +
                            "Transfers\n" +
                            "ID\t\tFrom/To\t\t\tAmount\n" +
                            "-------------------------------------------";
            Console.WriteLine(header);
            foreach (API_Transfer transfer in transfers)
            {
                string fromOrTo;
                string username;
                if (transfer.FromUser.UserId == currentUserId)
                {
                    fromOrTo = "To:";
                    username = transfer.ToUser.Username;
                }
                else
                {
                    fromOrTo = "From:";
                    username = transfer.FromUser.Username;
                }
                Console.WriteLine($"{transfer.TransferId}\t\t{fromOrTo}\t{username}\t\t{transfer.Amount:C2}");
            }
            Console.WriteLine("---------");
        }

        public void DisplayTransferDetails(API_Transfer transfer)
        {
            string header = "-------------------------------------------\n" +
                            "Transfer Details\n" +
                            "-------------------------------------------";
            Console.WriteLine(header);
            Console.WriteLine($"Id: {transfer.TransferId}");
            Console.WriteLine($"From: {transfer.FromUser.Username}");
            Console.WriteLine($"To: {transfer.ToUser.Username}");
            Console.WriteLine($"Type: {transfer.TransferType}");
            Console.WriteLine($"Status: {transfer.TransferStatus}");
            Console.WriteLine($"Amount: {transfer.Amount:C2}");
        }
    }
}
