using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GloboTicket.Mobile.Models;

namespace GloboTicket.Mobile.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
