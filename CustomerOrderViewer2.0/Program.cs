namespace CustomerOrderViewer2._0
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Repositorys;

    class Program
    {
            private static string _connectionString =
                @"Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True";

            private static readonly CustomerOrdersCommand _customerOrdersCommand = new(_connectionString);
            private static readonly CustomersCommand _customersCommand = new(_connectionString);
            private static readonly ItemsCommand _itemsCommand = new(_connectionString);
            
        static void Main(string[] args)
        {
            try
            {
                var continueManaging = true;
                var userId = string.Empty;

                Console.WriteLine("What is your username?");
                userId = Console.ReadLine();

                do
                {
                    Console.WriteLine(
                        "1 - Show All | 2 - Upsert Customer Order | 3 - Delete Customer Order | 4 - Exit");
                    int option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        ShowAll();
                    }
                    else if (option == 2)
                    {
                        UpsertCustomerOrder(userId);
                    }
                    else if (option == 3)
                    {
                        DeleteCustomerOrder(userId);
                    }
                    else if (option == 4)
                    {
                        continueManaging = false;
                    }
                    else
                    {
                        Console.WriteLine("Option not found");
                    }

                } while (continueManaging == true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Someting went wrong: {0}", ex.Message);
            }

        }

        private static void ShowAll()
        {
            Console.WriteLine("{0}All Customer Orders: {0}", Environment.NewLine);
            DisplayCustomerOrders();

            Console.WriteLine("{0}All Customers: {0}", Environment.NewLine);
            DisplayCustomers();

            Console.WriteLine("{0}All Items: {0}", Environment.NewLine);
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayItems()
        {
            IList<ItemsModel> items = _itemsCommand.GetList();

            if (items.Any())
            {
                foreach (ItemsModel item in items)
                {
                    Console.WriteLine("{0}: Description: {1}, Price {2}", item.ItemId, item.Description, item.Price);
                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomersModel> customers = _customersCommand.GetList();

            if (customers.Any())
            {
                foreach (CustomersModel customer in customers)
                {
                    Console.WriteLine("{0}: First Name: {1}, Middle Name: {2}, Last Name: {3}, Age: {4}",
                        customer.CustomerId,
                        customer.Firstname,
                        customer.MiddleName ?? "N/A",
                        customer.LastName,
                        customer.Age);
                }
            }
        }

        private static void DisplayCustomerOrders()
        {
            IList<CustomerOrderDetailsModel> customerOrderDetails = _customerOrdersCommand.GetList();

            if (customerOrderDetails.Any())
            {
                foreach (CustomerOrderDetailsModel customerOrderDetail in customerOrderDetails)
                {
                    Console.WriteLine("{0}: Fullname: {1} {2} (Id: {3}) - purchased {4} for {5} (Id: {6})",
                        customerOrderDetail.CustomerOrderId,
                        customerOrderDetail.FirstName,
                        customerOrderDetail.LastName,
                        customerOrderDetail.CustomerId,
                        customerOrderDetail.Description,
                        customerOrderDetail.Price,
                        customerOrderDetail.ItemId);
                }
            }
        }

        private static void DeleteCustomerOrder(string userId)
        {
            Console.WriteLine("Enter CustomerOrderId:");
            int customerOrderId = Convert.ToInt32(Console.ReadLine());

            _customerOrdersCommand.Delete(customerOrderId, userId);
        }

        private static void UpsertCustomerOrder(string userId)
        {
            Console.WriteLine("Note: For updating insert existing CustomerOrderId, for new entries enter -1.");

            Console.WriteLine("Enter CustomerOrderId:");
            int newCustomerOrderId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CustomerId:");
            int newCustomerId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter ItemId:");
            int newItemId = Convert.ToInt32(Console.ReadLine());

            _customerOrdersCommand.Upsert(newCustomerOrderId, newCustomerId, newItemId, userId);

        }

    }
}
