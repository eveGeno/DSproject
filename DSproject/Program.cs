using DSproject;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Create engine
        var engine = new DatabaseEngine<Customer>();

        // Create indexes
        var uniqueIndex = new UniqueIndex<int, Customer>();
        var nonUniqueIndex = new NonUniqueIndex<string, Customer>();
        var rangeIndex = new RangeIndex<int, Customer>();

        // Add indices to engine
        engine.AddIndex(uniqueIndex);
        engine.AddIndex(nonUniqueIndex);
        engine.AddIndex(rangeIndex);

        // Add sample records
        var customer1 = new Customer(1, "Company A", "Arik", "Rotshild St 2", "TLV", "12345", "IL");
        var customer2 = new Customer(2, "Company B", "Bob", "456 Elm St", "NYC", "23456", "USA");
        var customer3 = new Customer(3, "Company C", "Yossi", "Jabotinski 34", "JLM", "34567", "IL");

        engine.AddRecord(customer1);
        engine.AddRecord(customer2);
        engine.AddRecord(customer3);

        // Test querying all records
        Console.WriteLine("All records:");
        foreach (var customer in engine.GetAllRecords())
        {
            Console.WriteLine(customer);
        }

        // Test record deletion
        Console.WriteLine("Removing customer2...");
        engine.RemoveRecord(customer2);
        Console.WriteLine("Customer2 removed.");

        // Checking that customer2 is deleted
        var removedCustomer = engine.FindRecordByUniqueIndex(uniqueIndex, 2); // Search by unique index
        if (removedCustomer == null)
        {
            Console.WriteLine("Customer2 has been successfully deleted.");
        }
        else
        {
            Console.WriteLine("Customer2 was not deleted.");
        }

        // Test adding a new customer
        var customer4 = new Customer(4, "Company D", "Barak", "242 Josephine St", "TOR", "98752", "CA");
        engine.AddRecord(customer4);
        Console.WriteLine("Added customer4.");

        // Verify customer4 is added
        var foundCustomer = engine.FindRecordByUniqueIndex(uniqueIndex, 4);
        if (foundCustomer != null)
        {
            Console.WriteLine("Customer4 found in records.");
        }
        else
        {
            Console.WriteLine("Customer4 was not found in records.");
        }

        //Testing adding a duplicate
        try
        {
            Console.WriteLine("Trying to add duplicate customer1...");
            engine.AddRecord(new Customer(1, "Company D", "David", "101 Maple St", "TLV", "13579", "IL"));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Expected exception: {ex.Message}");
        }

        // Test removing a customer that does not exist
        Console.WriteLine("Trying to remove non-existent customer...");
        engine.RemoveRecord(new Customer(5, "Company E", "Eli", "Unknown St", "Unknown", "00000", "ZZ"));
        Console.WriteLine("No error occurred when removing non-existent customer.");

        // Test finding customer after removal
        var nonExistentCustomer = engine.FindRecordByUniqueIndex(uniqueIndex, 2); // customer2 should not be found
        if (nonExistentCustomer == null)
        {
            Console.WriteLine("Successfully verified that customer2 does not exist.");
        }
        else
        {
            Console.WriteLine("Error: customer2 still exists.");
        }

        //From one company
        Console.WriteLine("All customers from Company D:");
        foreach (var customer in engine.GetRecordsByCompany("Company D"))
        {
            Console.WriteLine(customer);
        }

        //From one country
        Console.WriteLine("All customers from IL:");
        foreach (var customer in engine.GetRecordsByCountry("IL"))
        {
            Console.WriteLine(customer);
        }

        //Final check of all clients
        Console.WriteLine("Final list of clients:");
        foreach (var customer in engine.GetAllRecords())
        {
            Console.WriteLine(customer);
        }

    }
}
