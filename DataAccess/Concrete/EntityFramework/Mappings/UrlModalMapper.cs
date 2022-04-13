using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Mappings
{
    public class UrlModalMapper : IEntityTypeConfiguration<UrlModal>
    {
        public void Configure(EntityTypeBuilder<UrlModal> builder)
        {
            builder.HasKey(urlModal => urlModal.Id);
           
            builder.Property(urlModal => urlModal.LongUrl).HasColumnType("NVARCHAR(MAX)");
            builder.Property(urlModal => urlModal.ShortUrl).HasColumnType("NVARCHAR(MAX)");
            builder.Property(urlModal => urlModal.CreatedDate).HasColumnType("DATE");
        }
    }
}
