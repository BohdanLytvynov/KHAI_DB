// See https://aka.ms/new-console-template for more information

using Domain.Extensions;

Console.WriteLine("DB Helper!");

Console.WriteLine("Enter password for Hashing:");

string pass = Console.ReadLine();

string hash = SecurePasswordHasher.Hash(pass, 2000);

Console.WriteLine(hash);

Console.WriteLine(SecurePasswordHasher.Verify(pass, hash));

Console.WriteLine("Finish!");
