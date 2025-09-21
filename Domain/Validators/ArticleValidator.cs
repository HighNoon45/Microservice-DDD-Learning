using Domain.Entities.ArticleAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator() 
        {
            RuleFor(x => x.Nr).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
