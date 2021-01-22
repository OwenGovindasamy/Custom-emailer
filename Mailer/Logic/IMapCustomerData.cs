using System.Threading.Tasks;
using Mailer.Dto;
using Mailer.Models;

namespace Mailer.Logic
{
    public interface IMapCustomerData
    {
        Task<bool> SendEmailLogic(CustomerDto CustomerDto);
    }
}