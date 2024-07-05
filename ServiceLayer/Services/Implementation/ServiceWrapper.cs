using DataLayer.DbContext;
using ServiceLayer.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly PianoContext context;

        public ServiceWrapper(PianoContext context)
        {
            this.context = context;
            system = new SystemService(context);
        }

        private ISystemService system;
        public ISystemService System
        {
            get
            {
                if (system == null)
                {
                    system = new SystemService(context);
                }
                return system;
            }
        }
    }
}
