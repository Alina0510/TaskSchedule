using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.DAL;

namespace TaskSchedule.BLL.Singletons
{
    public class SingletonContext
    {
        private static ApplicationContext _instance;

        private static readonly object _lock = new object();

        private SingletonContext()
        {
        }

        public static ApplicationContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ApplicationContext();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}
