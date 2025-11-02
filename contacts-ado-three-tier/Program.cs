using ContactsBusinessLayer;
using System;
using System.Data;
using System.Diagnostics.Contracts;

namespace ConsoleApp5
{
    internal class Program
    {
        static void TestFindContact(int ID)
        {

            clsContact contact = clsContact.Find(ID);
            if (contact != null)
            {
                Console.WriteLine(contact.FirstName + " " + contact.LastName);
                Console.WriteLine(contact.Email);
                Console.WriteLine(contact.Phone);
                Console.WriteLine(contact.Address);
                Console.WriteLine(contact.DateOfBirth);
                Console.WriteLine(contact.CountryID);
                Console.WriteLine(contact.ImagePath);
            }
            else
            { Console.WriteLine($"Contact {ID} not Found! "); }
        }

        static void TestAddNewContact()
        {
            clsContact Contact1=new clsContact();
            Contact1.FirstName = "Fadi";
            Contact1.LastName = "Maher";
            Contact1.Email = "A@a.com";
            Contact1.Phone = "010010";
            Contact1.Address = "address1";
            Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";

            if (Contact1.Save())
            {

                Console.WriteLine("Contact Added Successfully with id=" + Contact1.ID);
            }
          

        }
        static void Main(string[] args)
        {
           TestFindContact(31);  //Not found!

            TestAddNewContact();   

        }
    }
}
