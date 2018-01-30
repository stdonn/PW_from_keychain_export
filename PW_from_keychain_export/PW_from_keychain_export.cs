//
//  PW_from_keychain_export.cs
//
//  Author:
//       Stephan Donndorf <stephan@donndorf.info>
//
//  Copyright (c) 2018 Stephan Donndorf
//

using System;
using System.Globalization;
using System.Threading;

namespace PW_from_keychain_export
{
    class PW_from_keychain_export
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong number of arguments! Canceling...");
                Console.WriteLine("Usage: PW_from_keychain_export <export file> <output csv file>...");
                Environment.Exit(-1);
            }

            // change globalization information of this porgram (especially the file formats!)
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            try
            {
                new ProcessKeychain().processKeychainFile(args[0], args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurs during conversation!");
                Console.WriteLine(ex.Message);
                foreach (string key in ex.Data.Keys)
                    Console.WriteLine(key + " - " + ex.Data[key]);
            }
        }
    }
}
