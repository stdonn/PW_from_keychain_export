//
//  KeychainEntry.cs
//
//  Author:
//       Stephan Donndorf <stephan@donndorf.info>
//
//  Copyright (c) 2018 Stephan Donndorf
//

using System;
namespace PW_from_keychain_export
{
    public struct KeychainEntry
    {
        public string group
        {
            get; set;
        }

        public string title
        {
            get; set;
        }

        public string username
        {
            get; set;
        }

        public string password
        {
            get; set;
        }

        public string url
        {
            get; set;
        }

        public string notes
        {
            get; set;
        }

    }
}
