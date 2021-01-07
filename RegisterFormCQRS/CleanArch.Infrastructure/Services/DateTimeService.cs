using CleanArch.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
