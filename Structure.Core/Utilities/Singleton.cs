﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Core.Utilities
{
    public abstract class Singleton<T> where T : new()
    {
        static T _instance = new T();

        public Singleton()
        {
            if (_instance != null)
            {
                throw new Exception("Only 1 instance of the class can be created.");
            }
        }

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
