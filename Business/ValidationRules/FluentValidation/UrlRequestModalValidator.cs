using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
   public class UrlRequestModalValidator : AbstractValidator<UrlRequestModal>
   {
       public UrlRequestModalValidator()
       {
           RuleFor(urlModal => urlModal.LongUrl).Must(VerifyUrl);
       }

       private bool VerifyUrl(string url)
       {
           Uri uriResult;
           bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

           return result;
       }

    }
}
