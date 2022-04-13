using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IUrlModalService
    {
        Task<IDataResult<List<UrlModal>>> GetAllAsync();
        Task<IDataResult<UrlResponseModal>> GetByLongUrlAsync(string longUrl);
        Task<IDataResult<UrlResponseModal>> GetByShortUrlAsync(string shortUrl);
        Task<IResult> AddAsync(UrlRequestModal urlRequestModal);
        Task<IResult> AddCustomAsync(UrlRequestModal urlRequestModal);

    }
}
