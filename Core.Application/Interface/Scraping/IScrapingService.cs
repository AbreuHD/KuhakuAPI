using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interface.Scraping
{
    public interface IScrapingService
    {
        Task RunScrapingAsync();
    }
}
