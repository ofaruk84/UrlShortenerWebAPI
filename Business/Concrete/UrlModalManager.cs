using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Utilities.Hashing;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
   public class UrlModalManager : IUrlModalService
   {
       private readonly IUrlModalDal _urlModalDal;

       public UrlModalManager(IUrlModalDal urlModalDal)
       {
           _urlModalDal = urlModalDal;
       }


       public async Task<IDataResult<List<UrlModal>>> GetAllAsync()
       {
           var urls = await _urlModalDal.GetAllAsync();

           if (urls.Count > -1)
           {
               return new SuccessDataResult<List<UrlModal>>(urls, Messages.AllRecordsSuccessfullyFetchedFromDatabase);
           }

           return new ErrorDataResult<List<UrlModal>>(null, Messages.ThereAreNoRecordsInTheDatabase);
       }

       public async Task<IDataResult<UrlResponseModal>> GetByLongUrlAsync(string longUrl)
       {
           var url = await _urlModalDal.GetAsync(url => url.LongUrl.Equals(longUrl));

           if (url is null) return new ErrorDataResult<UrlResponseModal>(null, Messages.ThereAreNoRecordsInTheDatabase);

           var response = new UrlResponseModal
           {
               UrlModal = url
           };
           return new SuccessDataResult<UrlResponseModal>(response, Messages.AllRecordsSuccessfullyFetchedFromDatabase);

       }

       public async Task<IDataResult<UrlResponseModal>> GetByShortUrlAsync(string shortUrl)
       {
           var url = await _urlModalDal.GetAsync(url => url.ShortUrl==shortUrl);

           if (url == null) return new ErrorDataResult<UrlResponseModal>(null, Messages.ThereAreNoRecordsInTheDatabase);

           var response = new UrlResponseModal
            {
                UrlModal = url
            };
            return new SuccessDataResult<UrlResponseModal>(response, Messages.AllRecordsSuccessfullyFetchedFromDatabase);

       }

       public async Task<IResult> AddAsync(UrlRequestModal urlRequestModal)
       {
           var record =  await _urlModalDal.GetAsync(urlModal => urlModal.LongUrl == urlRequestModal.LongUrl);
           if (record is not null) return new ErrorResult(Messages.TheresARecordWithThisUrl);

           var validationResult = ValidationTool<UrlRequestModal>.Validate(new UrlRequestModalValidator(), urlRequestModal);

           if (!validationResult) return new ErrorResult(Messages.InvalidUrl);

           var shortUrl = UrlHasher.CreateShortUrl(urlRequestModal.LongUrl);
           var urlModal = new UrlModal
           {
               LongUrl = urlRequestModal.LongUrl,
               ShortUrl = shortUrl,
               CreatedDate = DateTime.Now
           };
            
           await _urlModalDal.AddAsync(urlModal);
           return new SuccessResult(Messages.RecordHasBeenSuccessfullyAdded);
       }

       public async Task<IResult> AddCustomAsync(UrlRequestModal urlRequestModal)
       {
           var record = await _urlModalDal.GetAsync(urlModal => urlModal.LongUrl == urlRequestModal.LongUrl);
           if (record is not null) return new ErrorResult(Messages.TheresARecordWithThisUrl);

            var res = ValidationTool<UrlRequestModal>.Validate(new UrlRequestModalValidator(), urlRequestModal);

           if (!res) return new ErrorResult(Messages.InvalidUrl);

           if (urlRequestModal.CustomName is null) return new ErrorResult(Messages.EmptyCustomName);
           
           var urlModal = new UrlModal
           {
               LongUrl = urlRequestModal.LongUrl,
               ShortUrl = CreateCustomUrl(urlRequestModal),
               CreatedDate = DateTime.Now
           };

           await _urlModalDal.AddAsync(urlModal);
           return new SuccessResult(Messages.RecordHasBeenSuccessfullyAdded);
        }


       private string CreateCustomUrl(UrlRequestModal urlRequestModal)
       {
           var splittedUrl = urlRequestModal.LongUrl.Split('.')[1];
           

           var shortUrl = "http://" + splittedUrl + "/" + urlRequestModal.CustomName;

           return shortUrl;
        }
   }
}
