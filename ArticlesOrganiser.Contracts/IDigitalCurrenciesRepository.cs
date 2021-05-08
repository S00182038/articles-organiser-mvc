using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArticlesOrganiser.Contracts
{
    public interface IDigitalCurrenciesRepository
    {
        Task<DigitalCurrency> All(string paginationToken = "");
    }
}
