using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFirst.Models;

namespace TaskFirst
{
    internal class Convertor
    {
        public List<OutputModel> ConvertToOutputModel(List<InputModel> inputList)
        {
            List<OutputModel> outputList = new List<OutputModel>();

            inputList.ForEach(e => e.Address = e.Address.Substring(0, e.Address.IndexOf(',')));
            
            outputList = inputList.GroupBy(x => x.Address)
                .Select(y => new OutputModel
                {
                    City = y.Key,
                    Services = y.GroupBy(g => g.Service).Select(h => new Service
                    {
                        Name = h.Key,
                        Payers = h.Select(p => new Payer
                        {
                            Name = p.FirstName + ' ' + p.LastName,
                            Payment = p.Payment,
                            Date = p.Date,
                            AccountNumber = p.AccountNumber
                        }).ToList(),
                        Total = 0
                    }).ToList(),
                    Total = 0
                }).ToList();
            outputList.ForEach(x => x.Services.ForEach(y => y.Payers.ForEach(z => y.Total += z.Payment)));
            outputList.ForEach(x => x.Services.ForEach(y => x.Total += y.Total));
            return outputList;
        }
    }
}
